using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Attack attack;

    public float moveSpeed;
    public float jumpForce = 5f;
    public float Rush_time ;
    public float rush_F = 20f;
    public bool isJumping = false;
    public bool isRush_F = false;
    public bool isRush_B = false;
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
        
        // role jump
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            anim.SetBool("Jump",true);
            rb.AddForce(new Vector2(rb.velocity.x, jumpForce));
            isJumping = true;
        }
        
    }
   
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Reset jump state when character hits the ground
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
        //turn over
        if ((Mathf.Abs(PlayerTwo_x.position.x) > Mathf.Abs(transform.position.x)))
        {
            transform.localScale = new Vector3(2, 2, 2);
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.D))
            {
                anim.SetBool("CanRush_F",true);
                rb.velocity = new Vector2(moveX * rush_F, rb.velocity.y);
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.A))
            {
                anim.SetBool("CanRush_B",true);
                StartCoroutine(Rush_F());
            }
            else
            {
                anim.SetBool("CanRush_F",false);
                anim.SetBool("CanRush_B",false);
            }
            
            
        }
        else 
        {
            transform.localScale = new Vector3(-2, 2, 2);
            if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.D))
            {
                anim.SetBool("CanRush_B",true);
                StartCoroutine(Rush_B());
            }
            else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.A))
            {
                anim.SetBool("CanRush_F",true);
                rb.velocity = new Vector2(moveX * rush_F, rb.velocity.y);
            }
            else
            {
                anim.SetBool("CanRush_F",false);
                anim.SetBool("CanRush_B",false);
            }
        }
    }
   
   IEnumerator Rush_F() 
    {
        yield return new WaitForSeconds(Rush_time);
        transform.position = new Vector2(transform.position.x-2 ,transform.position.y);
    }
     IEnumerator Rush_B() 
    {
        yield return new WaitForSeconds(Rush_time);
        transform.position = new Vector2(transform.position.x+2 ,transform.position.y);
    }
}

