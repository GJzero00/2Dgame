using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Attack attack;

    public float moveSpeed;
    public float jumpForce = 5f;
    public float rush = 20f;
    public bool isJumping = false;
    public bool isRush = false;
    private Transform PlayerTwo_x;
    private GameObject PlayerTwo;

    private Rigidbody2D rb;
    private Animator anim;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        PlayerTwo= GameObject.Find("playerTwo");
        PlayerTwo_x = PlayerTwo.GetComponent<Transform>();
    }

    void Update()
    {
       
        Move();
        
        // 角色跳躍
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            anim.SetBool("Jump",true);
            rb.AddForce(new Vector2(rb.velocity.x, jumpForce));
            isJumping = true;
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 角色碰撞地面時重置跳躍狀態
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            anim.SetBool("Jump",false);
        }
    }

    //move
    public void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);
        anim.SetFloat("Blend", Mathf.Abs(moveX));
        //翻轉
        if ((Mathf.Abs(PlayerTwo_x.position.x) > Mathf.Abs(transform.position.x)))
        {
          
            transform.localScale = new Vector3(2, 2, 2);
        }
        else 
        {
            transform.localScale = new Vector3(-2, 2, 2);
        }
    }
   
}
