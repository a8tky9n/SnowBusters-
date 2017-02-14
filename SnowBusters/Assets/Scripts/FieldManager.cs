using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    //フィールドの広さは42*42(x,y)
    int[,] FeildStatu = new int[43,43];//0～42
    [SerializeField]GameObject SnowWall_;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void MakeWall(int x, int y)
    {
        Debug.Log(x+" , "+y+"→"+(x + 21) + " , " + (y + 21));
        if (FeildStatu[x+21, y+21] == 0)
        {
            GameObject.Instantiate(SnowWall_, new Vector3(x, 0, y), Quaternion.identity);
            FeildStatu[x+21, y+21] = 1;
        }
    }
}
