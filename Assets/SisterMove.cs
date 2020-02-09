using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SisterMove : MonoBehaviour
{
    //インスペクタで設定
    public float speed;
    public float gravity;
    public float jumpSpeed;
    public float jumpHeight;
    public AnimationCurve dashCurve;
    public AnimationCurve jumpCurve;
    public GameObject sister;
    PlayerActionManager move;

    //変数を宣言

    Animator anim;
    Rigidbody2D rb;
    string groundTag = "Ground";
    bool isGround = false;
    bool isGroundEnter, isGroundStay, isGroundExit;
    bool isJump = false;
    bool isAttack = false;
    float jumpPos = 0.0f;
    float dashTime, jumpTime;
    float beforeAttack;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        move = sister.GetComponent<PlayerActionManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        anim.SetBool("jump", isJump);
        anim.SetBool("ground", isGround);
        anim.SetBool("attack", isAttack);

        if (isGroundEnter || isGroundStay)
        {
            isGround = true;
        }
        else if (isGroundExit)
        {
            isGround = false;
        }

        float horizontalkey = Input.GetAxis("Horizontal");
        float xSpeed = 0.0f;
        float ySpeed = -gravity;
        if (horizontalkey > 0)
        {
            anim.SetBool("run", true);
            transform.localScale = new Vector3(1, 1, 1);
            xSpeed = speed;
            dashTime += Time.deltaTime;
        }
        else if (horizontalkey < 0)
        {
            anim.SetBool("run", true);
            transform.localScale = new Vector3(-1, 1, 1);
            xSpeed = -speed;
            dashTime += Time.deltaTime;
        }
        else
        {
            anim.SetBool("run", false);
            xSpeed = 0.0f;
            dashTime = 0.0f;
        }

        float verticalkey = Input.GetAxis("Vertical");

        if (isGround)
        {
            if (verticalkey > 0)
            {
                ySpeed = jumpSpeed;
                jumpPos = transform.position.y;//ジャンプ位置記録
                isJump = true;
            }
            else
            {
                isJump = false;
            }
        }
        else if (isJump)
        {
            if (verticalkey > 0 && jumpPos + jumpHeight > transform.position.y)
            {
                ySpeed = jumpSpeed;
                jumpTime += Time.deltaTime;
            }
            else
            {
                isJump = false;
            }
            jumpTime = 0.0f;
        }

        dashTime *= dashCurve.Evaluate(dashTime);
        if (isJump)
        {
            ySpeed *= jumpCurve.Evaluate(jumpTime);
        }

        rb.velocity = new Vector2(xSpeed, ySpeed);

        isGroundEnter = false;
        isGroundStay = false;
        isGroundExit = false;

        //アタックを入れるとこ


        Attack();
       



    }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == groundTag)
            {
                isGroundEnter = true;
            }
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.tag == groundTag)
            {
                isGroundStay = true;
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == groundTag)
            {
                isGroundExit = true;
            }
        }

    public void Attack()
    {

        float horizontalkey = Input.GetAxis("Horizontal");
        float fireKey = Input.GetAxis("Fire1");


        if (fireKey > 0 && !move.isJumpDisabled()) //&& beforeAttack == 0.0f)
        {
            isAttack = true;
            horizontalkey = 0.0f;

        }

        else
        {
            isAttack = false;
        }
        //beforeAttack = fireKey;
        move.disableJump();
    }
   

}

    

