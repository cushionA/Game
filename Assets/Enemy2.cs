using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    #region//インスペクターで設定する
    [Header("移動速度")] public float speed;
    [Header("重力")] public float gravity;
    [Header("画面外でも行動する")] public bool nonVisibleAct;
    [HideInInspector]public bool move;
    [HideInInspector] public bool left = false;
    #endregion

    #region//プライベート変数
    private Rigidbody2D rb = null;
    private SpriteRenderer sr = null;
    private Animator anim = null;
    private BoxCollider2D col = null;
    private bool isDead = false;
    private float deadTimer = 0.0f;
    float playerPos;
    string attackTag = "Attack";
  
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();       
        col = GetComponent<BoxCollider2D>();
    }



    void FixedUpdate()
    {


        if (move)
        {


        

            int xVector = -1;
            if (left)
            {

                xVector = 1;
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            rb.velocity = new Vector2(xVector * speed, -gravity);
            anim.SetBool("walk", true);

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
}
