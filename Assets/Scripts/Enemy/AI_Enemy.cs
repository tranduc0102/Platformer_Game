using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Enemy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private Collider2D arrageAttack;
    private int playerCollisionCount = 0;
    public float speed;
    public bool flip = false;
    
    
    
    public bool CanRun
    {
        get
        {
            return anim.GetBool("CanRun");
        }
        set
        {
            anim.SetBool("CanRun",value);
        }
    }

    public void AfterAttack()
    {
        anim.SetBool("Attack",false);
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        arrageAttack = transform.Find("ArrageAttack").GetComponent<Collider2D>();
    }
    private void Update()
    {
        Running();
    }

    private void Running()
    {
        if (CanRun)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            Flip();
        }
    }

    private void Flip()
    {
        if (flip)
        {
            float x = transform.localScale.x;
            transform.localScale = new Vector2(-x, transform.localScale.y);
            speed = -speed;
            flip = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (arrageAttack &&other.gameObject.CompareTag("Player"))
        {
            playerCollisionCount++;
            anim.SetBool("Attack", true);
            CanRun = false;
        }
    }
    
    
    private  void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            flip = true;
        }
        if (arrageAttack && other.gameObject.CompareTag("Player"))
        {
            playerCollisionCount--;
            if (playerCollisionCount <= 0)
            {
                anim.SetBool("Attack", false);
                CanRun = true;
                playerCollisionCount = 0; 
            }
        }
    }
}