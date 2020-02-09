using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionManager : MonoBehaviour
{
    public GameObject sister;
    SisterMove act;

    // 無効化する時間(秒)
    static  readonly float disableTime = 2.0f;

    // 無効化終了時刻
   public float disableFinishAt = 0.0f;


    private void Start()
    {

    act = sister.GetComponent<SisterMove>();

    }

   

    // 無効化終了時刻を設定
   public void disableJump()
    {
       
            disableFinishAt = Time.time + disableTime;
            Debug.Log("判定します");
        
    }

    // 無効化中か判定
   public bool isJumpDisabled()
    {
        
        return Time.time < disableFinishAt;
      
    }

   
}
