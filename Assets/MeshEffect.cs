using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshEffect : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player;


    private SkinnedMeshRenderer sr = null;
    SisterMove act;

    void Start()
    {
        sr = GetComponent<SkinnedMeshRenderer>();
        act = player.GetComponent<SisterMove>();
    }

    private void Update()
    {//updateはレンダリングの前に呼ばれるので画面表示の前に差し込める。
        if (act.isContinue)
        {
            Debug.Log("点滅");
            //明滅　ついている時に戻る
            if (act.blinkTime > 0.2f)
            {
                sr.enabled = true;
                act.blinkTime = 0.0f;
            }
            //明滅　消えているとき
            else if (act.blinkTime > 0.1f)
            {
                sr.enabled = false;
            }
            //明滅　ついているとき
            else
            {
                sr.enabled = true;
            }
            //------------------------------------------------------
            //ここで区切る。一秒経ってないうちはデルタタイムする。

            //1秒たったら明滅終わり
            if (act.continueTime > 1.0f)
            {
               
               
                sr.enabled = true;
            }
           
        }
    }
}
