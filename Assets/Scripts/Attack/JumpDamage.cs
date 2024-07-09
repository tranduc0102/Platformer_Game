using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpDamage : MonoBehaviour
{
   public Animator anim;
   public GameObject destroyObject;
   public float jumpForce;
   public int lifes;
   public float time = 0f;
   private bool check = false;

   private void Awake()
   {
      destroyObject = GameObject.Find("ArrageAttack");
   }

   private void FixedUpdate()
   {
      if(check)
         time -= Time.deltaTime;
      if (time <= 0)
      {
         check = false;
      }
   }

   private void OnCollisionEnter2D(Collision2D other)
   {
      if ( other.transform.CompareTag("Player") && !check)
      {
         other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up*jumpForce;
         LosseLifeAndHit();
         CheckLife();
         time = 0.25f;
         check = true;
      }
   }

   public void LosseLifeAndHit()
   {
      lifes--;
      anim.SetTrigger("Hit");
   }

   public void CheckLife()
   {
      if (lifes == 0)
      {
         anim.SetBool("Dead",true);
         destroyObject.SetActive(false);
         gameObject.layer = 7;
      }
   }
}
