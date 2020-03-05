using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour
{
    public GameObject stm;
    StageCtrl stg;
    // Start is called before the first frame update
    void Start()
    {
        stg = stm.GetComponent<StageCtrl>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RetryGame()
    {
        GManager.instance.isGameOver = false;
        GManager.instance.isClear = false;
        GManager.instance.heartNum = GManager.instance.defaultHeartNum;
        GManager.instance.continueNum = 0;
        stg.PlayerSetContinuePoint();
        //GManagerの数値を初期化するのはシーン切り替えでもインスタンスが破棄されないから。Static。
        SceneManager.LoadScene("Stage");
    }


}
