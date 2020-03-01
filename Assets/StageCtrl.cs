using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCtrl : MonoBehaviour
{
    [Header("プレイヤーゲームオブジェクト")] public GameObject playerObj;
    [Header("コンティニュー位置")] public GameObject[] continuePoint;
    [Header("ゲームオーバー")] public GameObject GameOverObj;
    private SisterMove p;

    void Start()
    {
        if (playerObj != null && continuePoint != null && continuePoint.Length > 0)
        {
            playerObj.transform.position = continuePoint[0].transform.position;
            p = playerObj.GetComponent<SisterMove>();
            if (p == null)
            {
                Debug.Log("プレイヤーが設定されていません");
                Destroy(this);
            }
        }
        else//← これはif (playerObj != null～のループに対応、その反対。 
        {
            Debug.Log("ステージコントローラーの設定が足りていません");
            Destroy(this);
        }
    }


    private void Update()
    {
        if (GManager.instance.isGameOver)
        {
            GameOverObj.SetActive(true);
         

        }
        else
        {
            GameOverObj.SetActive(false);

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
