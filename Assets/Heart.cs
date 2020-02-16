using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    public GameObject hp_object = null;

    private Text heartText;
    private int oldHeartNum;

    void Start()
    {
        heartText = hp_object.GetComponent<Text>();
        if (heartText != null && GManager.instance != null)
        {
            heartText.text = "HP ×  " + GManager.instance.heartNum.ToString();
        }
    }

    void Update()
    {
        if (heartText != null && GManager.instance != null)
        {
            if (oldHeartNum != GManager.instance.heartNum)
            {
                heartText.text = "HP ×  " + GManager.instance.heartNum.ToString();
                oldHeartNum = GManager.instance.heartNum;
            }
        }
    }
}