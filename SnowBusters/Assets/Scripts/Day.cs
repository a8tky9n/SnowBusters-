using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day : MonoBehaviour {
    public bool night;
    Quaternion StartAngle;
    void Start()
    {
        StartAngle = gameObject.transform.rotation;
        //StartCoroutine("RotateLight");
    }
    void Update()
    {
        gameObject.transform.Rotate(0, Time.deltaTime*3, 0);
        if (gameObject.transform.eulerAngles.x >= 180)
        {
            night = true;

        }
        if (night && gameObject.transform.eulerAngles.x >= 0 && gameObject.transform.eulerAngles.x <= 180)
        {
            night = false;
            gameObject.transform.rotation = StartAngle;
        }
    }
    IEnumerator RotateLight()
    {
        //Debug.Log("現在の角度"+gameObject.transform.eulerAngles.x);
        yield return new WaitForSeconds(1f);
        //gameObject.transform.Rotate(0, 0.6f, 0);
        gameObject.transform.Rotate(0, 10f, 0);//デバッグ用 
        if (gameObject.transform.eulerAngles.x>=180)
        {
            night = true;
            
        }
        if (night&&gameObject.transform.eulerAngles.x >= 0&& gameObject.transform.eulerAngles.x <= 180)
        {
            night = false;
            gameObject.transform.rotation = StartAngle;
        }
        StartCoroutine("RotateLight");
    }
}
