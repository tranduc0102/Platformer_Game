using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _animator.SetBool("On",true);
            AudioManager.Instance.PlaySFX(AudioManager.Instance.checkPoint);
            other.gameObject.GetComponent<PlayerRespawn>().PointCheckPoint(transform.position.x,transform.position.y);
        }
    }
}
