using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clear : MonoBehaviour
{

    GameObject[] enemy;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log("感知");
        if (enemy.Length == 0)
        {
            GManager.instance.isClear = true;
        }
        
    }
}
