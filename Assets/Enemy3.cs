using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{

    #region//インスペクターで設定する
    [Header("移動速度")] public float speed;
    [Header("重力")] public float gravity;
    [Header("画面外でも行動する")] public bool nonVisibleAct;
    public GameObject player;

    #endregion

    #region//プライベート変数
    private Rigidbody2D rb = null;
    private SpriteRenderer sr = null;
    private Animator anim = null;
    bool right = false;
    private BoxCollider2D col = null;
    private bool isDead = false;
    private float deadTimer = 0.0f;
    Transform rbs;
    float playerPos;
    string attackTag = "Attack";
    int xVector;
    float chargeTime = 0.0f;
    bool isCor;
    float actTime = 0.0f;
  
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindWithTag("Player");こうすればプレイアブルが変わっても。

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rbs = player.GetComponent<Transform>();
        col = GetComponent<BoxCollider2D>();
    }



    void FixedUpdate()
    {
        //デフォルトの立ちモーションに遷移。
        if (!isCor)//このフラグが立っている間は方向転換をできない
        {
           
            playerPos = rbs.position.x;

            if (playerPos - transform.position.x > 0)
            {
                right = true;
                Debug.Log("照準");
            }
            else
            {
                right = false;
            }
            //↑プレイヤーがどちらにいるか判断。
        }
       

        if (sr.isVisible)
        {
           
        　　　　//画面に入っている間動作
            actTime += Time.fixedDeltaTime;

            //時間を数えて、画面に入った三秒後に起動。また、突進後のクールタイムも兼ねる。
            if (right && actTime > 3.0f)
            {
                //プレイヤーが右側、かつクールタイム消化。
                Debug.Log("移動開始");
                transform.localScale = new Vector3(1, 1, 1);
                xVector = 1;
                Debug.Log("加算");
                if (chargeTime < 3.0f)
                {
                    //突進の制限時間三秒である以内は方向転換を禁じて進み続ける。
                    anim.SetBool("walk", true);
                    isCor = true;//方向転換を禁止。


                    rb.velocity = new Vector2(xVector * speed, -gravity);
                    chargeTime += Time.fixedDeltaTime;
                }
               

                //プレイヤーの向きにアニメーションを反転させ、引数を入れてコルーチン開始。
            }

            else if(!right && actTime > 3.0f)
            { //プレイヤーが左側、かつクールタイム消化。
                transform.localScale = new Vector3(-1, 1, 1);
                xVector = -1;
              
                if (chargeTime < 3.0f)
                {
                    //突進の制限時間三秒である以内は方向転換を禁じて進み続ける。
                    anim.SetBool("walk", true);
                    Debug.Log("移動開始");
                    isCor = true;//方向転換を禁止。

                    rb.velocity = new Vector2(xVector * speed, -gravity);
                    chargeTime += Time.fixedDeltaTime;

                }
                //プレイヤーの向きにアニメーションを反転させ、引数を入れてコルーチン開始。
            }
            if (chargeTime >= 3.0f)
            {//突進の制限時間を超えたら初期状態に戻し、方向転換を可能に。
                anim.SetBool("walk", false);
                isCor = false;
                actTime = 0.0f;
                chargeTime = 0.0f;
               
            }
        }


            /* else
             {

                 if (!isDead)
                 {
                     anim.Play("dead");
                     rb.velocity = new Vector2(0, -gravity);
                     isDead = true;
                     col.enabled = false;
                 }*/
            if (isDead)
            {
                transform.Rotate(new Vector3(0, 0, 5));
                if (deadTimer > 3.0f)
                //死んだフレームから何秒？
                {
                    Destroy(this.gameObject);
                }
                else
                {
                    deadTimer += Time.deltaTime;
                }
            }
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == attackTag)
        {

            if (!isDead)
            {
                anim.Play("dead");
                rb.velocity = new Vector2(0, -gravity);
                isDead = true;
                col.enabled = false;
            }


        }

    }

    IEnumerator Charge(int xVector)
    {

        isCor = true;
        //yield return new WaitForSeconds(3.0f);
        Debug.Log("コルーチン開始");
        //三秒待って処理開始。
        anim.SetBool("walk", true);
        while (chargeTime > 3.0f)
        {

            rb.velocity = new Vector2(xVector * speed, -gravity);
            Debug.Log("移動");
            chargeTime += Time.deltaTime;

        }
            //アニメーションを移動に遷移させ、さらに引数の方向へ設定したスピードで動かす。

            //二秒間突進して終了
        
            Debug.Log("コルーチン終了");
        chargeTime = 0.0f;
        isCor = false;
               yield break;
    }

}