using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;

public class Attack : MonoBehaviour
{
   private Rigidbody2D rb;
   private Animator animator;

   public float attackCooldown = 2f;  // 攻擊冷卻時間，單位為秒
   private float cooldownTimer = 0f;  // 冷卻時間計時器
   public bool isAttacking = false;  // 是否正在攻擊
   

    public const int Key_Up = 0;    
    public const int Key_Down = 1;    
    public const int Key_Left = 2;    
    public const int Key_Right = 3;    
    public const int Key_Attack = 4; 
       
    public const int Frame_Count = 100;     
    public const int Sample_Size = 2;   
    public const int Sample_Count = 2;    
    
    
    int[,]Sample =
    {
        //上(W)+攻擊(J)
        { Key_Up ,Key_Attack },
        //下(S)+攻擊(J)
        { Key_Down,Key_Attack },
    };
      
    int  currentkeyCode =0;     
    bool startFrame = false;         
    int  currentFrame = 0;     
    List<int> playerSample;        
    bool isSuccess= false;          

    void Start()
    { 
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerSample  = new List<int>();   
    }

    
    void Update()
    {
        UpdateKey();

        if (Input.anyKeyDown)
        {
            if (isSuccess)
            {
                isSuccess = false;
                Reset();
            }

            if (!startFrame)
            {
                startFrame = true;
            }
            playerSample.Add(currentkeyCode);
            int size = playerSample.Count;
            if(size == Sample_Count)
            {
                for(int i = 0; i< Sample_Size; i++)    
                {    
                    int SuccessCount = 0;    
                    for(int j = 0; j< Sample_Count; j++)    
                    {    
                        int temp = playerSample[j];    
                            if(temp== Sample[i,j])
                            {    
                            SuccessCount++;    
                            }    
                    }
                    
                    if(SuccessCount ==Sample_Count)    
                    {    
                        isSuccess = true;    
                        break;    
                    }
                }
            }
        }
        if (startFrame)
        {
            currentFrame++;
        }
        if(currentFrame >= Frame_Count)
        {
            if (!isSuccess)
            {
                Reset();
            }
        }
      if (isAttacking)
        {
            // 如果正在攻擊，則啟動冷卻時間計時器
            cooldownTimer += Time.deltaTime;

            if (cooldownTimer >= attackCooldown)
            {
                // 冷卻時間結束，允許再次攻擊
                isAttacking = false;
                cooldownTimer = 0f;
            }
        }
      /* if (isSuccess && !isAttacking)
        {  
          animator.SetTrigger("IsTopCut");
          isAttacking = true;
        }
        
        else if (isSuccess && !isAttacking)
        {
          animator.SetTrigger("IsDownCut");
          isAttacking = true;
        }
        else
        {
            isSuccess = false;
        }
          */
      if (isSuccess && !isAttacking)
        {
            Debug.Log("123");
        }
    }
    void Reset ()    
     {    
        currentFrame = 0;    
        startFrame = false;    
        playerSample.Clear();    
     }   

    void UpdateKey()    
    {    
          
        if (Input.GetKeyDown (KeyCode.W))    
        {    
            currentkeyCode = Key_Up;            
        }    
        if (Input.GetKeyDown (KeyCode.S))    
        {    
            currentkeyCode = Key_Down;    
        }    
        if (Input.GetKeyDown (KeyCode.A))    
        {    
            currentkeyCode = Key_Left;    
        }    
        if (Input.GetKeyDown (KeyCode.D))    
        {    
            currentkeyCode = Key_Right;    
        }    
        if (Input.GetKeyDown (KeyCode.J))    
        {    
            currentkeyCode = Key_Attack;    
        }    
    }    
}
