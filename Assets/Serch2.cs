using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Serch2 : MonoBehaviour
{


    public GameObject enemy;

    Enemy2 en;
    string groundTag = "Ground";


    // Start is called before the first frame update
    void Start()
    {

        en = enemy.GetComponent<Enemy2>();
    }

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == groundTag)
        {
            if (en.left)
            {
                en.left = false;

            }
            else
            {
                en.left = true;


            }


        }
    }
}
