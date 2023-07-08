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
        
        // ������D
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            anim.SetBool("Jump",true);
            rb.AddForce(new Vector2(rb.velocity.x, jumpForce));
            isJumping = true;
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // ����I���a���ɭ��m���D���A
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
        //½��
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
