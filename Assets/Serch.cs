using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Serch : MonoBehaviour
{


    public GameObject enemy;



    Enemy2 en;
    string playerTag = "Player";
 

    // Start is called before the first frame update
    void Start()
    {
        en = enemy.GetComponent <Enemy2> ();
        
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == playerTag)
        {
            en.move = true;

        }

    }
}
