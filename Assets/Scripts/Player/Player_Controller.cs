using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [SerializeField]private Rigidbody2D rb;
    [SerializeField]private Animator anim;
    public bool doubleJump = false;
    public float forceJump;
    public float speed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Jump();
        Running();
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
            }
            else
            {
                anim.SetBool("Run",true);
            }
        }
        else
        {
            anim.SetBool("Run",false);
        }
    }

    private bool isJump = false;

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (CheckGround.isGround)
            {
                rb.velocity = new Vector2(rb.velocity.x,forceJump);
                isJump = false;
            }
            if (!CheckGround.isGround && !isJump)
            {
                doubleJump = true;
            }
            if (doubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x,forceJump);
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
        float x = transform.localScale.x;
        if ((toward > 0 && x < 0) || (toward < 0 && x > 0))
        {
            transform.localScale = new Vector2(-x, transform.localScale.y);
        }
    }
}
