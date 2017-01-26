using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    //プレイヤーの状態を表示する
    //雪だまの数
    public int SnowBall = 3;
    //持てる雪だまの最大数
    int MaxSnowBallCount = 5;
    //体力
    public int Health=100;
    //体力の最大値
    int MaxHealth = 100;
    //しゃがんでいるかどうかのフラグ
    public bool Crouching = false;
    //コルーチンが走っているかどうかのフラグ
    bool IsRunning = false;
    [SerializeField]
    Image HealthImage;

	void Update () {
        Crouching = Input.GetKey(KeyCode.C);

        if (Crouching&&Input.GetAxisRaw("Horizontal")==0&&Input.GetAxisRaw("Vertical")==0)
        {
            StartCoroutine("MakeSnowBall");
        }
        else
        {
            StopCoroutine("MakeSnowBall");
        }
	}

    //ダメージを受ける処理=================
    public void Damage(int dame)
    {
        Health = Health - dame;
    }
    //=====================================
    
    //ライフを現在の体力どおりに表示する処理******
    private void DrawHealth(int health)
    {
        HealthImage.fillAmount = health / MaxHealth;
    }
    //********************************************

    //残弾の数を変更処理@@@@@@@@@@@@@@@@@@@@@@@@
    public void SnowBallProcess(int num)
    {
        if (num >= 1)
        {
            if (SnowBall<=MaxSnowBallCount)
                SnowBall += num;
        }else
        {
            if (SnowBall > 0)
                SnowBall += num;
        }
    }
    //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

    //所持できる雪だまの最大数を変更する処理======
    public void MaxSnowBallCountChange(int num)
    {
        MaxSnowBallCount += num;
    }
    //============================================

    //雪だまを作る処理(コルーチン)****************
    IEnumerator MakeSnowBall()
    {
        if (IsRunning)
        {
            yield break;
        }
        else
        {
            IsRunning = true;
            yield return new WaitForSeconds(2f);
            SnowBallProcess(1);
            IsRunning = false;
        }
    }
    //********************************************

}
