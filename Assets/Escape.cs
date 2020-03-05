using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escape : MonoBehaviour
{
    // Inspector
    [SerializeField] private GameObject playerObject;
    SisterMove sis;

    private void Start()
    {
        sis = playerObject.GetComponent<SisterMove>();
    }

    // ゲームオブジェクトのレイヤーを取得する
    public void GetLayer()
    {
        Debug.Log(playerObject.layer);
    }

    // ゲームオブジェクトのレイヤーを変更する
    public void SetLayer(int layerNumber)
    {
        playerObject.layer = layerNumber;
    }

    private void Update()
    {
        if(sis.isContinue){
            GetLayer();
            SetLayer(8);
   

        }
        else
        {
            SetLayer(0);

        }
        
    }

}
