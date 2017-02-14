using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class debugText : MonoBehaviour {
    Player PL_;
	// Use this for initialization
	void Start () {
        PL_ = GameObject.Find("yukinnko3").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.GetComponent<Text>().text = "残弾 : " + PL_.SnowBall;
	}
}
