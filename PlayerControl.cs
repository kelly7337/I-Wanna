using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed = 2;
    public float jumpSpeed1;
    public float jumpSpeed2;

    public GameWindow gameWindow;

    private int jumpCount = 2;
    private Vector3 playerScale;
    private Rigidbody2D playerRigidBody;
    private Animator PlayerAni;
    private CapsuleCollider2D playerFeet;
    public void InitPlayer()
    {
        playerScale = transform.localScale;
        playerRigidBody = GetComponent<Rigidbody2D>();
        PlayerAni = GetComponent<Animator>();
        playerFeet = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Jump();
        Fall();
        IfOnLand();
    }

    private void Run()
    {
        if(Input.GetKey(KeyCode.RightArrow)) { 
            transform.localScale = playerScale;
            playerRigidBody.velocity = new Vector2(speed, playerRigidBody.velocity.y);
            PlayerAni.SetBool("IfRun", true);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.localScale = new Vector3(-playerScale.x, playerScale.y, playerScale.z);
            playerRigidBody.velocity = new Vector2(-speed, playerRigidBody.velocity.y);
            PlayerAni.SetBool("IfRun", true);
        }
        else
        {
            playerRigidBody.velocity = new Vector2(0, playerRigidBody.velocity.y);
            PlayerAni.SetBool("IfRun", false);
        }
    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && jumpCount != 0) {
            PlayerAni.SetBool("IfJump", true);
            PlayerAni.SetBool("IfFall", false);
            PlayerAni.SetBool("IfIdle", false);
            if (jumpCount == 2) {
                playerRigidBody.velocity = Vector2.up * jumpSpeed1;
            }
            else if (jumpCount == 1)
            {
                playerRigidBody.velocity = Vector2.up * jumpSpeed2;
                jumpCount--;
            }
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            if(playerRigidBody.velocity.y > 3f)
            {
                playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, 3f);
            }
        }
    }

    private void Fall()
    {
        if(playerRigidBody.velocity.y <= 0f)
        {
            PlayerAni.SetBool("IfFall", true);
            PlayerAni.SetBool("IfJump", false);
            PlayerAni.SetBool("IfIdle", false);
        }
        if(playerRigidBody.velocity.y < -8f)
        {
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, -8f);
        }
    }

    private void IfOnLand()
    {
        if (playerFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            jumpCount = 2;
            if (PlayerAni.GetBool("IfFall") is true)
            {
                PlayerAni.SetBool("IfIdle", true);
                PlayerAni.SetBool("IfFall", false);
            }
        }
        else
        {
            if(jumpCount == 2)
            {
                jumpCount--;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Spike"))
        {
            gameWindow.GameOver();
        }
        else if (collision.transform.CompareTag("NextLevelSign"))
        {
            //gameWindow.NextLevel();
            gameWindow.SetWindowState(false);
            
        }
    }
}
