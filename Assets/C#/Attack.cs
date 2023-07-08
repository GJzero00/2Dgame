using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;

public class Attack : MonoBehaviour
{
   private Rigidbody2D rb;
   private Animator animator;

   public float attackCooldown = 2f;  // �����N�o�ɶ��A��쬰��
   private float cooldownTimer = 0f;  // �N�o�ɶ��p�ɾ�
   public bool isAttacking = false;  // �O�_���b����
   

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
        //�W(W)+����(J)
        { Key_Up ,Key_Attack },
        //�U(S)+����(J)
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
            // �p�G���b�����A�h�ҰʧN�o�ɶ��p�ɾ�
            cooldownTimer += Time.deltaTime;

            if (cooldownTimer >= attackCooldown)
            {
                // �N�o�ɶ������A���\�A������
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
