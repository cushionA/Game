using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPoint : MonoBehaviour
{
    [Header("ステージコントローラー")] public StageCtrl ctrl;

    private string playerTag = "Player";
    Collider2D col;

    private void Start()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        if (collision.tag == playerTag)
        {
            col.enabled = false;
            if (GManager.instance != null && ctrl != null)
            {
                GManager.instance.SubHeartNum();
               
                if (!GManager.instance.isGameOver)
                {
                    ctrl.PlayerSetContinuePoint();
                    Invoke("ColRecover", 2.0f);
                    Debug.Log("コライダー消滅");

                }

            }
            else
            {
                Debug.Log("設定が足りません");
            }
        }
    }
    void ColRecover ()
    {
        col.enabled = true;
 
    }
}
