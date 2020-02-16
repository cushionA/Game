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
    public float stepOnRate;
    MoveObject moveObj;


    PlayerActionManager move;

    //変数を宣言

    Animator anim;
    Rigidbody2D rb;
    string groundTag = "Ground";
    bool isGround = false;
    bool isGroundEnter, isGroundStay, isGroundExit;
    string moveFloorTag = "MoveFloor";
    bool isJump = false;
    bool isAttack = false;
    float jumpPos = 0.0f;
    float dashTime, jumpTime;
    float beforeAttack;
    float stepOnHeight;
    float judgePos;
    CapsuleCollider2D capcol;
    string enemyTag = "Enemy";
    bool isDown;



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




        float fireKey = Input.GetAxis("Fire1");


        if (fireKey > 0 && !move.isAttackDisabled()) //&& beforeAttack == 0.0f)
        {
            isAttack = true;
            horizontalkey = 0.0f;
            move.disableFinishAt = 0.0f;


        }

        else
        {
            isAttack = false;
        }
        //beforeAttack = fireKey;

        if (isAttack)
        {
            move.DisableAttack();

        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDown && !GManager.instance.isGameOver)
        {
            if (collision.collider.tag == enemyTag)
                    {
                        anim.Play("Sisterdown");
                        isDown = true;
                     
                        
                    }
                }
            
            //動く床
            else if (collision.collider.tag == moveFloorTag)
            {
                //踏みつけ判定になる高さ
                float stepOnHeight = (capcol.size.y * (stepOnRate / 100f));
                //踏みつけ判定のワールド座標
                float judgePos = transform.position.y - (capcol.size.y / 2f) + stepOnHeight;
                foreach (ContactPoint2D p in collision.contacts)
                {
                    //動く床に乗っている
                    if (p.point.y < judgePos)
                    {
                        moveObj = collision.gameObject.GetComponent<MoveObject>();
                    }
                }
            }
        }
    




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == groundTag || collision.tag == moveFloorTag)
        {
            isGroundEnter = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == groundTag || collision.tag == moveFloorTag)
        {
            isGroundStay = true;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == groundTag || collision.tag == moveFloorTag)
        {
            isGroundExit = true;
        }
    }

    public void Attack()
    {



    }




}



