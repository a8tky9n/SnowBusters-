using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    [SerializeField]
    AudioClip[] Scream_ = new AudioClip[4];
    AudioSource Aud;
    int health;
    [SerializeField]
    Image[] batu = new Image[5];

    void Start()
    {
        for(int i = 0; i < 5; i++)
        {
            batu[i].enabled = false;
        }
        health = 5;
        Aud = gameObject.GetComponent<AudioSource>();
    }
    void OnTriggerEnter(Collider col)
    {
        Debug.Log(col.tag);
        if (col.tag == "Enemy")
        {
            Aud.PlayOneShot(Scream_[Random.Range(0, 3)]);
            Destroy(col.gameObject);
            health -= 1;
            batu[health].enabled = true;
            if (health == 0)
            {
                StartCoroutine("SceneMove");
            }
        }
    }
    IEnumerator SceneMove()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("End");
    }
}
