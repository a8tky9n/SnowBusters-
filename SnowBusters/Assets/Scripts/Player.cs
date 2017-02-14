using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //プレイヤーの状態を表示する
    //雪だまの数
    public int SnowBall = 3;
    //持てる雪だまの最大数
    int MaxSnowBallCount = 20;
    //体力
    public int Health = 100;
    //体力の最大値
    int MaxHealth = 100;
    //しゃがんでいるかどうかのフラグ
    public bool Crouching_ = false;
    //コルーチンが走っているかどうかのフラグ
    bool IsRunning = false;
    [SerializeField]
    Image HealthImage;
    [SerializeField]
    GameObject RollingSnowBall;
    [SerializeField]
    Image[] balls = new Image[12];
    void Update()
    {
        Crouching_ = Input.GetKey(KeyCode.C);
        RollingSnowBall.SetActive(Crouching_);
        if (Crouching_ && Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            StartCoroutine("MakeSnowBall");
        }
        else
        {
            StopCoroutine("MakeSnowBall");
            IsRunning = false;
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            RollingSnowBall.GetComponent<RollingSnowBall>().IntiTransform();
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
            if (SnowBall <= MaxSnowBallCount)
                SnowBall += num;
        }
        else
        {
            if (SnowBall > 0)
                SnowBall += num;
        }
        switch (SnowBall)
        {
            case 0:
                balls[0].enabled = false;
                balls[1].enabled = false;
                balls[2].enabled = false;
                balls[3].enabled = false;
                balls[4].enabled = false;
                balls[5].enabled = false;
                balls[6].enabled = false;
                balls[7].enabled = false;
                balls[8].enabled = false;
                balls[9].enabled = false;
                balls[10].enabled = false;
                balls[11].enabled = false;
                break;
            case 1:
                balls[0].enabled = false;
                balls[1].enabled = false;
                balls[2].enabled = false;
                balls[3].enabled = false;
                balls[4].enabled = false;
                balls[5].enabled = false;
                balls[6].enabled = false;
                balls[7].enabled = false;
                balls[8].enabled = false;
                balls[9].enabled = false;
                balls[10].enabled = false;
                balls[11].enabled = true;
                break;
            case 2:
                balls[0].enabled = false;
                balls[1].enabled = false;
                balls[2].enabled = false;
                balls[3].enabled = false;
                balls[4].enabled = false;
                balls[5].enabled = false;
                balls[6].enabled = false;
                balls[7].enabled = false;
                balls[8].enabled = false;
                balls[9].enabled = false;
                balls[10].enabled = true;
                balls[11].enabled = true;
                break;
            case 3:

                balls[0].enabled = false;
                balls[1].enabled = false;
                balls[2].enabled = false;
                balls[3].enabled = false;
                balls[4].enabled = false;
                balls[5].enabled = false;
                balls[6].enabled = false;
                balls[7].enabled = false;
                balls[8].enabled = false;
                balls[9].enabled = true;
                balls[10].enabled = true;
                balls[11].enabled = true;
                break;
            case 4:
                balls[0].enabled = false;
                balls[1].enabled = false;
                balls[2].enabled = false;
                balls[3].enabled = false;
                balls[4].enabled = false;
                balls[5].enabled = false;
                balls[6].enabled = false;
                balls[7].enabled = false;
                balls[8].enabled = true;
                balls[9].enabled = true;
                balls[10].enabled = true;
                balls[11].enabled = true;
                break;
            case 5:
                balls[0].enabled = false;
                balls[1].enabled = false;
                balls[2].enabled = false;
                balls[3].enabled = false;
                balls[4].enabled = false;
                balls[5].enabled = false;
                balls[6].enabled = false;
                balls[7].enabled = true;
                balls[8].enabled = true;
                balls[9].enabled = true;
                balls[10].enabled = true;
                balls[11].enabled = true;
                break;
            case 6:
                balls[0].enabled = false;
                balls[1].enabled = false;
                balls[2].enabled = false;
                balls[3].enabled = false;
                balls[4].enabled = false;
                balls[5].enabled = false;
                balls[6].enabled = true;
                balls[7].enabled = true;
                balls[8].enabled = true;
                balls[9].enabled = true;
                balls[10].enabled = true;
                balls[11].enabled = true;
                break;
            case 7:
                balls[0].enabled = false;
                balls[1].enabled = false;
                balls[2].enabled = false;
                balls[3].enabled = false;
                balls[4].enabled = false;
                balls[5].enabled = true;
                balls[6].enabled = true;
                balls[7].enabled = true;
                balls[8].enabled = true;
                balls[9].enabled = true;
                balls[10].enabled = false;
                balls[11].enabled = true;
                break;
            case 8:
                balls[0].enabled = false;
                balls[1].enabled = false;
                balls[2].enabled = false;
                balls[3].enabled = false;
                balls[4].enabled = true;
                balls[5].enabled = true;
                balls[6].enabled = true;
                balls[7].enabled = true;
                balls[8].enabled = true;
                balls[9].enabled = true;
                balls[10].enabled = true;
                balls[11].enabled = true;
                break;
            case 9:
                balls[0].enabled = false;
                balls[1].enabled = false;
                balls[2].enabled = false;
                balls[3].enabled = true;
                balls[4].enabled = true;
                balls[5].enabled = true;
                balls[6].enabled = true;
                balls[7].enabled = true;
                balls[8].enabled = true;
                balls[9].enabled = true;
                balls[10].enabled = true;
                balls[11].enabled = true;
                break;
            case 10:
                balls[0].enabled = false;
                balls[1].enabled = true;
                balls[2].enabled = false;
                balls[3].enabled = false;
                balls[4].enabled = false;
                balls[5].enabled = false;
                balls[6].enabled = false;
                balls[7].enabled = false;
                balls[8].enabled = false;
                balls[9].enabled = false;
                balls[10].enabled = false;
                balls[11].enabled = false;
                break;
            case 11:
                balls[0].enabled = false;
                balls[1].enabled = true;
                balls[2].enabled = false;
                balls[3].enabled = false;
                balls[4].enabled = false;
                balls[5].enabled = false;
                balls[6].enabled = false;
                balls[7].enabled = false;
                balls[8].enabled = false;
                balls[9].enabled = false;
                balls[10].enabled = false;
                balls[11].enabled = true;
                break;
            case 12:
                balls[0].enabled = false;
                balls[1].enabled = true;
                balls[2].enabled = false;
                balls[3].enabled = false;
                balls[4].enabled = false;
                balls[5].enabled = false;
                balls[6].enabled = false;
                balls[7].enabled = false;
                balls[8].enabled = false;
                balls[9].enabled = false;
                balls[10].enabled = true;
                balls[11].enabled = true;
                break;
            case 13:
                balls[0].enabled = false;
                balls[1].enabled = true;
                balls[2].enabled = false;
                balls[3].enabled = false;
                balls[4].enabled = false;
                balls[5].enabled = false;
                balls[6].enabled = false;
                balls[7].enabled = false;
                balls[8].enabled = false;
                balls[9].enabled = true;
                balls[10].enabled = true;
                balls[11].enabled = true;
                break;
            case 14:
                balls[0].enabled = false;
                balls[1].enabled = true;
                balls[2].enabled = false;
                balls[3].enabled = false;
                balls[4].enabled = false;
                balls[5].enabled = false;
                balls[6].enabled = false;
                balls[7].enabled = false;
                balls[8].enabled = true;
                balls[9].enabled = true;
                balls[10].enabled = true;
                balls[11].enabled = true;
                break;
            case 15:
                balls[0].enabled = false;
                balls[1].enabled = true;
                balls[2].enabled = false;
                balls[3].enabled = false;
                balls[4].enabled = false;
                balls[5].enabled = false;
                balls[6].enabled = false;
                balls[7].enabled = true;
                balls[8].enabled = true;
                balls[9].enabled = true;
                balls[10].enabled = true;
                balls[11].enabled = true;
                break;
            case 16:
                balls[0].enabled = false;
                balls[1].enabled = true;
                balls[2].enabled = false;
                balls[3].enabled = false;
                balls[4].enabled = false;
                balls[5].enabled = false;
                balls[6].enabled = true;
                balls[7].enabled = true;
                balls[8].enabled = true;
                balls[9].enabled = true;
                balls[10].enabled = true;
                balls[11].enabled = true;
                break;
            case 17:
                balls[0].enabled = false;
                balls[1].enabled = true;
                balls[2].enabled = false;
                balls[3].enabled = false;
                balls[4].enabled = false;
                balls[5].enabled = true;
                balls[6].enabled = true;
                balls[7].enabled = true;
                balls[8].enabled = true;
                balls[9].enabled = true;
                balls[10].enabled = false;
                balls[11].enabled = true;
                break;
            case 18:
                balls[0].enabled = false;
                balls[1].enabled = true;
                balls[2].enabled = false;
                balls[3].enabled = false;
                balls[4].enabled = true;
                balls[5].enabled = true;
                balls[6].enabled = true;
                balls[7].enabled = true;
                balls[8].enabled = true;
                balls[9].enabled = true;
                balls[10].enabled = true;
                balls[11].enabled = true;
                break;
            case 19:
                balls[0].enabled = false;
                balls[1].enabled = true;
                balls[2].enabled = false;
                balls[3].enabled = true;
                balls[4].enabled = true;
                balls[5].enabled = true;
                balls[6].enabled = true;
                balls[7].enabled = true;
                balls[8].enabled = true;
                balls[9].enabled = true;
                balls[10].enabled = true;
                balls[11].enabled = true;
                break;
            case 20:
                balls[0].enabled = true;
                balls[1].enabled = true;
                balls[2].enabled = false;
                balls[3].enabled = false;
                balls[4].enabled = false;
                balls[5].enabled = false;
                balls[6].enabled = false;
                balls[7].enabled = false;
                balls[8].enabled = false;
                balls[9].enabled = false;
                balls[10].enabled = false;
                balls[11].enabled = false;
                break;
            default:
                break;

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
