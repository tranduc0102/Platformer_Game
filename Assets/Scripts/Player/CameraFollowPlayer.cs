using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform playerTransform; 
    [SerializeField] private float followSpeed = 2f;    
    [SerializeField] private Vector3 offset;            

    // Limitations for camera movement
    [SerializeField] private Vector2 minLimits;         
    [SerializeField] private Vector2 maxLimits;

    private void Awake()
    {
        if (playerTransform == null)
        {
            playerTransform = GameObject.Find("Player").transform;   
        }
    }

    private void Start()
    {
        Vector3 dis = new Vector2(0, -2);
        offset = transform.position - playerTransform.position+dis;
    }

    private void LateUpdate()
    {
        Follow();
    }

    void Follow()
    {
        Vector3 targetPosition = playerTransform.position + offset;
        
        targetPosition.x = Mathf.Clamp(targetPosition.x, minLimits.x, maxLimits.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minLimits.y, maxLimits.y);
        
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
}