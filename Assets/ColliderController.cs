using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour
{
    public  GameObject attack;  //1で作成したColliderをインスペクタから入れておく。

    Collider2D col;


    void Start()
    {
        col = attack.GetComponent<Collider2D>();
        //最初はColliderをオフにしておく
        col.enabled = false;
    }

    //AnimationEventなどから呼ぶ
    public void PunchOn()
    {
      
        //Colliderオン
        col.enabled = true;
    }

    //AnimationEventなどから呼ぶ
    public void PunchOff()
    {
        //Colliderオフ
        col.enabled = false;
        Debug.Log("aaa");
    }
}