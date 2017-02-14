using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingSnowBall : MonoBehaviour {
    Vector3 intiScale;
    Vector3 intiLPosition;

    [SerializeField]
    GameObject Field_;
    [SerializeField]
    GameObject Player_;
	// Use this for initialization
	void Start () {
        intiScale = gameObject.transform.localScale;
        intiLPosition = gameObject.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
        if (gameObject.activeSelf)
        {
            if (gameObject.transform.lossyScale.x<1f) {
                if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
                {
                    gameObject.transform.localScale += new Vector3(0.04f, 0.04f, 0.04f);
                    gameObject.transform.localPosition += new Vector3(0f, 0.021f, 0.021f);
                }
            }
        }
	}
    public void IntiTransform()
    {
        gameObject.transform.localScale = intiScale;
        gameObject.transform.localPosition = intiLPosition;
    }
    public void OnDisable()
    {
        if (gameObject.transform.lossyScale.x > 1.0)
        {
            Field_.GetComponent<FieldManager>().MakeWall((int)gameObject.transform.position.x, (int)gameObject.transform.position.z);
        }
        else
        {
            Player_.GetComponent<Player>().SnowBallProcess((int)(gameObject.transform.lossyScale.x * 10));
        }
    }
}
