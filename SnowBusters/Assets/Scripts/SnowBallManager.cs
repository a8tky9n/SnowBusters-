using UnityEngine;
using System.Collections;

public class SnowBallManager : MonoBehaviour {
    [SerializeField]
    int dmg;
	// Use this for initialization
	void Start () {
        Destroy(gameObject,5f);
	}
    void OnTriggerEnter(Collider col)
    {
        //Debug.Log(col.name);
        if (col.tag == "Enemy")
        {
            col.gameObject.GetComponent<Enemy>().DMG(dmg);
        }
        Destroy(gameObject);
    }
}
