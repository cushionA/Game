﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{
    public static GManager instance = null;//GM型のinstanceという名のインスタンスに無を入れる
    public int continueNum = 0;
    public int heartNum = 5;
    [HideInInspector]public bool isGameOver;



    
    private void Awake()
    {
        //これによりオープニングとかステージ2とかのほかのシーンに行ってもオブジェクトが破棄されない。
        if (instance == null)
        {
            instance = this;
            //↑最初にこのクラスのインスタンスを作っておく
            DontDestroyOnLoad(this.gameObject);
            //シーン移動によりオブジェクトを破壊しないメソッド↑
        }
        //インスタンスに何か入っている＝これ以外のものを破壊
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}