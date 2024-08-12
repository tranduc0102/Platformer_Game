using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject particleLeft;
    [SerializeField] private GameObject particleRight;
    private bool doubleJump = false;
    public float forceJump;
    public float speed;

    private bool isGravityInverted = false; // Biến để kiểm tra trạng thái trọng lực

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (GameManager.Instance.livePlayer > 0)
        {   Jump();
            Running();
            transGravity();
        }
    }

    private void Running()
    {
        float input = 0;
        if (Input.GetKey("a") || Input.GetKey("left"))
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            input = -1;
        }
        else if(Input.GetKey("d") || Input.GetKey("right"))
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            input = 1;
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            input = 0;
        }
        if(input!=0){
            Flip(input);

            if (!CheckGround.isGround)
            {
                anim.SetBool("Jump", true);
                anim.SetBool("Run", false);
                particleLeft.SetActive(false);
                particleRight.SetActive(false);
            }
            else
            {
                anim.SetBool("Run", true);
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
            anim.SetBool("Run", false);
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
                rb.velocity = Vector2.up * forceJump * Mathf.Sign(rb.gravityScale);
                isJump = false;
            }
            else if (!CheckGround.isGround && !isJump)
            {
                doubleJump = true;
            }

            if (doubleJump)
            {
                AudioManager.Instance.PlaySFX(AudioManager.Instance.jump);
                rb.velocity = Vector2.up * forceJump * Mathf.Sign(rb.gravityScale);
                anim.SetBool("DoubleJump", true);
                doubleJump = false;
                isJump = true;
            }
        }
        if (!CheckGround.isGround)
        {
            speed = 2f;
            anim.SetBool("Jump", true);
        }
        else
        {
            speed = 5f;
            anim.SetBool("Jump", false);
            anim.SetBool("DoubleJump", false);
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

    private void transGravity()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            rb.gravityScale *= -1;
            isGravityInverted = !isGravityInverted; // Cập nhật trạng thái trọng lực
            Rotation();
        }
    }

    private bool trans = false;

    private void Rotation()
    {
        if (trans == false)
        {
            transform.eulerAngles = new Vector3(0, 0, 180);
            spriteRenderer.flipX = true;
        }
        else
        {
            transform.eulerAngles = Vector3.zero;
            spriteRenderer.flipX = false;
        }

        trans = !trans;
    }
}
