using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager_ : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width / 4-150, Screen.height - 300, 300, 150), "RESTART"))
        {
            SceneManager.LoadScene("Main");
        }
        if (GUI.Button(new Rect(Screen.width*3 / 4-150, Screen.height - 300, 300, 150), "EXIT"))
        {
            Application.Quit();
        }
    }
}
