using UnityEngine;
using System.Collections;

public class ballController : MonoBehaviour {
    
	void Update () {
        gameObject.transform.Translate(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical"));
	}
}
