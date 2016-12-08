using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    //プレイヤーの状態を表示する
    int SnowBall = 3;
    public int Health=100;
    int MaxHealth = 100;
    [SerializeField]
    Image HealthImage;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    //ダメージを受ける処理=================
    public void Damage(int dame)
    {
        Health = Health - dame;
    }
    //=====================================
    //ライフを現在の体力どおりに表示する処理******
    void DrawHealth(int health)
    {
        HealthImage.fillAmount = health / MaxHealth;
    }
    //********************************************
}
