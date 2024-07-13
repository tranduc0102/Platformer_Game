using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAnimator : MonoBehaviour
{
   public List<RuntimeAnimatorController> listAnimator;
   public Animator anim;

   private void Awake()
   {
      anim = GetComponent<Animator>();
   }

   private void Start()
   { 
      anim.runtimeAnimatorController = listAnimator[GameManager.Instance.idAnimator];
   }

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.E))
      {
         Change();
      }
   }

   void Change()
   {
      if (GameManager.Instance.idAnimator < listAnimator.Count - 1)
      {
         GameManager.Instance.idAnimator++;
      }
      else
      {
         GameManager.Instance.idAnimator = 0;
      }
      anim.runtimeAnimatorController = listAnimator[GameManager.Instance.idAnimator];
   }
}
