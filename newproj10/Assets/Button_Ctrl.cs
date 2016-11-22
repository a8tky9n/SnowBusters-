using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Button_Ctrl : MonoBehaviour
{
    [SerializeField]
    Button Start_;
    [SerializeField]
    Image lifeBar_;
    // Use this for initialization
    void Start()
    {
        Start_.Select();
        lifeBar_.fillAmount = 0f;
    }
    public void OnButtonPressed(GameObject button)
    {
        Debug.Log(button.name);
        button.transform.SetAsLastSibling();
        //lifeBar_.fillAmount = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        lifeBar_.color=new Color(1f, lifeBar_.fillAmount,lifeBar_.fillAmount,1);
        lifeBar_.fillAmount += 0.01f;
    }
}
