using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpspeed;
    Rigidbody2D rb;
    Animator animator;
    float moveHorizontal;
    bool jumpflag,doublejumpflag,standflag;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        jumpflag = false;
        doublejumpflag = false;
        standflag = false;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        animator.SetBool("standflag", standflag);
        animator.SetBool("doublejumpflag", doublejumpflag);
        animator.SetFloat("jumpspeed",rb.velocity.y);
    }

    /*//移动函数
    void Movement()
    {
        moveHorizontal = Input.GetAxis("Horizontal");//对应键盘上的A键和D键 或←键和→键，返回的是递增的浮点数
        moveHorizontal = Input.GetAxisRaw("Horizontal");//返回的是-1，0，1整数

        //Debug.Log("水平："+ moveHorizontal+",垂直："+ moveVertical);

        rb.velocity = new Vector2(moveHorizontal * speed, rb.velocity.y);//对刚体组件的速度属性乘上一个标量speed

        if (moveHorizontal != 0&&standflag==true)
        {
            Debug.Log("run");
            animator.Play("Player01_run Animation");
        }
        else if(standflag==true)
        {
            Debug.Log("idle");
            animator.Play("Player01_idle Animation");
        }

        if (Input.GetKeyDown(KeyCode.W) && doublejumpflag == true && jumpflag == false)
        {
            Debug.Log("doublejump");
            animator.Play("Player01_doubleJump Animation");
            rb.velocity = new Vector2(rb.velocity.x, jumpspeed);
            doublejumpflag = false;
        }

        if (Input.GetKeyDown(KeyCode.W)&&jumpflag==true)
        {
            Debug.Log("jump");
            animator.Play("Player01_jump Animation");
            rb.velocity = new Vector2(rb.velocity.x, jumpspeed);
            jumpflag = false;
            doublejumpflag = true;
        }

        if (rb.velocity.y <= 0&&standflag==false)
        {
            Debug.Log("fall");
            animator.Play("Player01_fall Animation");
        }
    }
    *///不通过unity动画系统进行动画切换

    void Movement()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveHorizontal * speed, rb.velocity.y);
        animator.SetFloat("speed",Mathf.Abs(rb.velocity.x));

        if (Input.GetKeyDown(KeyCode.W) && doublejumpflag == true && jumpflag == false)
        {
            animator.Play("Player01_doubleJump Animation");
            rb.velocity = new Vector2(rb.velocity.x, jumpspeed);
            doublejumpflag = false;
        }

        if (Input.GetKeyDown(KeyCode.W) && jumpflag == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpspeed);
            jumpflag = false;
            doublejumpflag = true;
        }

        Veer();

    }

    /*
    void Veer()
    {
        if (moveHorizontal > 0)
        {
            transform.rotation = Quaternion.Euler(0,0,0);//控制旋转Player来控制面向
        }
        if (moveHorizontal < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);//旋转Player
        }
    }
    */
    void Veer()
    {
        if (moveHorizontal > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);//控制缩放Player来控制面向
        }
        if (moveHorizontal < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);//控制缩放Player来控制面向
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)//用碰撞点的法线来判断所碰撞的面，用一个正方体来举例，首先拿到碰撞点的法线对象
        {
            if (contact.normal.y == 1)//然后判断 取到的normal对象的值
            {
                if (collision.gameObject.tag == "Platform")
                {
                    jumpflag = true;
                    doublejumpflag = false;
                    standflag = true;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Trap" || collision.gameObject.tag == "TrapMod")
        {
            animator.Play("Player01_hit Animation");
            GameCtr.instance.GameOverFlag = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Boundary" )
        {
            GameCtr.instance.GameOverFlag = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
                if (collision.gameObject.tag == "Platform")
                {
                    standflag = false;
                }      
    }
}
