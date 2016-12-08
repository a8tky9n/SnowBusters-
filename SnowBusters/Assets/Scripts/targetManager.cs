using UnityEngine;
using System.Collections;

public class targetManager : MonoBehaviour {
    LineRenderer LR;
    
	void Start () {
        LR = gameObject.GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {
            gameObject.transform.Translate(Input.GetAxisRaw("Mouse X")/3f, 0, Input.GetAxisRaw("Mouse Y")/3f);
            LR.enabled = true;
            LR.SetPosition(0, gameObject.transform.root.transform.position);

            LR.SetPosition(4, gameObject.transform.position);
        }else
        {
            LR.enabled = false;
        }
    }
}
