using UnityEngine;
using System.Collections;

public class ThrowSnowBall : MonoBehaviour {
    //ターゲットの座標
    public Transform Target;
    //仰角
    [Range(0,90)]
    public float firingAngle = 45.0f;
    //重力加速度
    public float gravity = 9.8f;
    //投げる物体
    public GameObject Projectile;
    //自分の座標
    private Transform myTransform;

    void Awake()
    {
        myTransform = gameObject.transform;
    }

    void Start()
    {
       // StartCoroutine(SimulateProjectile());
    }
    void Update()
    {
        if(Input.GetMouseButtonUp(0)){
            StartCoroutine(SimulateProjectile());
        }
        if(Input.GetAxisRaw("Mouse ScrollWheel") != 0){
            if (firingAngle > 5 && firingAngle < 90)
            {
                firingAngle += Input.GetAxisRaw("Mouse ScrollWheel")*5f;
            }
            else
            {
                if (firingAngle <= 5)
                {
                    firingAngle = 5.1f;
                }else if (firingAngle >= 90)
                {
                    firingAngle = 89.9f;
                }
            }
        }
    }

    IEnumerator SimulateProjectile()
    {
        //yield return new WaitForSeconds(1.5f);
        GameObject proj;
        proj = Instantiate(Projectile) as GameObject;
        //投げるオブジェクトを自分の座標+αに持ってくる
        proj.transform.position= myTransform.position + new Vector3(0, 0.0f, 0);

        //ターゲットとの距離を計算する
        float target_Distance = Vector3.Distance(proj.transform.position, Target.position);

        // 指定した仰角でどれだけの速度があればターゲットに衝突するか計算する.
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        // X軸とY軸に速度を分解する
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        // 滞空時間を計算する
        float flightDuration = target_Distance / Vx;

        // Rotate projectile to face the target.
        proj.transform.rotation = Quaternion.LookRotation(Target.position - proj.transform.position);

        float elapse_time = 0;

        while (elapse_time < flightDuration)
        {
            proj.transform.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

            elapse_time += Time.deltaTime;

            yield return null;
        }
    }
}
