using UnityEngine;
using System.Collections;

public class targetManager : MonoBehaviour
{
    LineRenderer LR;
    [SerializeField]
    GameObject RP; //ReleasePoint
    [SerializeField]
    GameObject Player;
    void Start()
    {
        LR = gameObject.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            gameObject.transform.parent = null;
            //仰角
            float Angle = RP.GetComponent<ThrowSnowBall>().firingAngle;
            //ターゲットとの距離
            float dist = Vector3.Distance(gameObject.transform.position, RP.transform.position);
            //初速度
            float projectile_Velocity = dist / (Mathf.Sin(2 * Angle * Mathf.Deg2Rad) / 9.8f);
            //Y軸方向の初速度
            float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(Angle * Mathf.Deg2Rad);
            //最大到達点
            float h = Mathf.Tan(Mathf.Deg2Rad * Angle) * dist / 2;
            //ターゲットとの中点の座標
            Vector3 Center = (RP.transform.position + gameObject.transform.position) / 2;
            //直前の座標をとっておく
            Vector3 beforePosition = gameObject.transform.position;
            //マウスを移動することで落下地点を変更できる
            //マウスのX軸方向の移動はプレイヤーに対して回転する動き
            gameObject.transform.RotateAround(Player.transform.position, Vector3.up, Input.GetAxisRaw("Mouse X"));
            //マウスのY軸方向の移動はプレイヤーとの距離が変化する動き
            if (Mathf.Abs(Player.transform.position.z-gameObject.transform.position.z) >= 1)
            {
                gameObject.transform.Translate(0, 0, Input.GetAxisRaw("Mouse Y")/3);
            }
            else
            {
                if (Player.transform.position.z - gameObject.transform.position.z > 0)
                {
                    gameObject.transform.position = new Vector3(gameObject.transform.localPosition.x, 0, Player.transform.position.z + 1f);
                }
                else
                {
                    gameObject.transform.position = new Vector3(gameObject.transform.localPosition.x, 0, Player.transform.position.z - 1f);
                }
            }

            //Debug.Log("Mouse X: " + Input.GetAxisRaw("Mouse X") + ",Mouse Y: " + Input.GetAxisRaw("Mouse Y"));
            //落下地点の移動を制限する
            gameObject.transform.position = new Vector3(Mathf.Clamp(transform.position.x, -21.5f, 21.5f), gameObject.transform.position.y, Mathf.Clamp(transform.position.z, -21.5f, 21.5f));
            Player.transform.LookAt(gameObject.transform, Vector3.up);
            //Debug.Log(((PT * PT) + (PdT * PdT) - (TdT * TdT)) / 2 * PT * PdT);
            //Debug.Log(theta + " " + PT + " " + PdT + " " + TdT * TdT);
            //弾道描画
            LR.enabled = true;
            LR.SetPosition(0, RP.transform.position);
            LR.SetPosition(1, new Vector3((RP.transform.position.x + Center.x) / 2, 3 * Vy * Vy / (8 * 8.9f) + RP.transform.position.y, (RP.transform.position.z + Center.z) / 2));
            LR.SetPosition(2, new Vector3(Center.x, Vy * Vy / (2 * 9.8f) + RP.transform.position.y, Center.z));
            LR.SetPosition(3, new Vector3((transform.position.x + Center.x) / 2, 3 * Vy * Vy / (8 * 8.9f) + RP.transform.position.y, (transform.position.z + Center.z) / 2));
            LR.SetPosition(4, gameObject.transform.position);
            //Debug.Log("Global:" + (RP.transform.position.z + gameObject.transform.position.z)/2);
            // Debug.Log("Local:" + (RP.transform.localPosition.x + gameObject.transform.localPosition.x)/2);
        }
        else
        {
            //弾道を消す
            LR.enabled = false;
            gameObject.transform.parent = Player.transform;
        }
    }

    Vector3 yZero(Vector3 Vec3)
    {
        Vec3 = new Vector3(Vec3.x, 0, Vec3.z);
        return Vec3;
    }
}
