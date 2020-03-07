using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageCtrl : MonoBehaviour
{
    [Header("プレイヤーゲームオブジェクト")] public GameObject playerObj;
    [Header("コンティニュー位置")] public GameObject[] continuePoint;
    [Header("ゲームオーバー")] public GameObject GameOverObj;
    [Header("ゲームオーバー")] public GameObject ClearObj;
    private SisterMove p;
    private bool doGameOver = false; //New!
    private bool startFade = false; //New!

    void Start()
    {
        if (playerObj != null && continuePoint != null && continuePoint.Length > 0 && GameOverObj != null) //New!
        {
            GameOverObj.SetActive(false); //New!
            ClearObj.SetActive(false);
            playerObj.transform.position = continuePoint[0].transform.position;
            p = playerObj.GetComponent<SisterMove>();
            if (p == null)
            {
                Debug.Log("プレイヤーが設定されていません");
                Destroy(this);
            }
        }
        else
        {
            Debug.Log("ステージコントローラーの設定が足りていません");
            Destroy(this);
        }
    }


    private void Update()
    {
        //ゲームオーバー New
        if (GManager.instance.isGameOver)
        {
            GameOverObj.SetActive(true);
           
        }
        //プレイヤーがダメージを受けた
        else if (GManager.instance.isClear)
        {
            //表示
            ClearObj.SetActive(true);

        }

    }



    /// <summary>
    /// プレイヤーをコンティニューポイントへ移動する
    /// </summary>
    public void PlayerSetContinuePoint()
    {
        playerObj.transform.position = continuePoint[GManager.instance.continueNum].transform.position;
        p.ContinuePlayer();
    }
}
   