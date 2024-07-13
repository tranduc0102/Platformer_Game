using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObject : MonoBehaviour
{
   public Transform enemy;
   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.gameObject.CompareTag("Player"))
      {
         other.gameObject.GetComponent<Animator>().SetTrigger("Hit");
         Debug.Log("thang nay da bi mat mau");
         other.gameObject.GetComponent<PlayerRespawn>().ReturnCheckPoint();
      }
   }
}
