using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    [SerializeField]
    GameObject enemy;
    [SerializeField]
    Day day_;
	void Start()
    {
        StartCoroutine("Spawn");
    }
    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(5f);
        if (day_.night)
        {
            Instantiate(enemy, gameObject.transform.position, Quaternion.identity);
        }
        StartCoroutine("Spawn");
    }
}
