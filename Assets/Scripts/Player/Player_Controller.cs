using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [SerializeField]private Rigidbody2D rb;
    [SerializeField]private Animator anim;
    [SerializeField]private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject particleLeft; 
    [SerializeField] private GameObject particleRight; 
    private bool doubleJump = false;
    public float forceJump;
    public float speed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (GameManager.Instance.livePlayer > 0)
        {
            Jump();
            Running();   
        }
    }

    private void Running()
    {
        float input = Input.GetAxisRaw("Horizontal");
        if (input != 0)
        {
            transform.Translate( new Vector2(speed*input*Time.deltaTime,0f));
            Flip(input);
            if (!CheckGround.isGround)
            {
                anim.SetBool("Jump",true);
                anim.SetBool("Run",false);
                particleLeft.SetActive(false);
                particleRight.SetActive(false);
            }
            else
            {
                anim.SetBool("Run",true);
                if (input < 0)
                {
                    particleLeft.SetActive(false);
                    particleRight.SetActive(true);
                }
                else if (input > 0)
                {
                    particleLeft.SetActive(true);
                    particleRight.SetActive(false);
                }
            }
        }
        else
        {
            anim.SetBool("Run",false);
            particleLeft.SetActive(false);
            particleRight.SetActive(false);
        }
    }

    private bool isJump = false;

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (CheckGround.isGround)
            {
                AudioManager.Instance.PlaySFX(AudioManager.Instance.jump);
                rb.velocity = Vector2.up*forceJump;
                isJump = false;
            }
            if (!CheckGround.isGround && !isJump)
            {
                doubleJump = true;
            }
            if (doubleJump)
            {
                AudioManager.Instance.PlaySFX(AudioManager.Instance.jump);
                rb.velocity = Vector2.up*forceJump;
                anim.SetBool("DoubleJump",true);
                doubleJump = false;
                isJump = true;
            }
        }
        if (!CheckGround.isGround)
        {
            speed = 2f;
            anim.SetBool("Jump",true);
        }

        if (CheckGround.isGround)
        {
            speed = 5f;
            anim.SetBool("Jump",false);
            anim.SetBool("DoubleJump",false);
            isJump = false;
        }
    }

    private void Flip(float toward)
    {
        if ((toward > 0 && spriteRenderer.flipX) || (toward < 0 && !spriteRenderer.flipX))
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }
}