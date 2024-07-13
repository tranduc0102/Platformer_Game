using System;
using System.Collections;
using UnityEngine;

public class Fan : MonoBehaviour
{
    public Transform player;
    public float pushForce = 15f; 
    public float maxDistance = 10f;
    private Rigidbody2D playerRb;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
    }

    void Start()
    {
        if (player != null)
        {
            playerRb = player.GetComponent<Rigidbody2D>();
        }
        StartCoroutine(FanActivation());
    }

    IEnumerator FanActivation()
    {
        while (true)
        {
            if (Mathf.Approximately(transform.eulerAngles.z, 90))
            {
                ApplyForceToPlayer1();
            }
            else if (Mathf.Approximately(transform.eulerAngles.z, 0))
            {
                ApplyForceToPlayer2();
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    void ApplyForceToPlayer1()
    {
        if (player.transform.position.y > transform.localScale.x+1 || player.transform.position.x >transform.position.x)
        {
            return;
        }
        Vector2 direction = (player.position - transform.position).normalized;
        float distance = Vector2.Distance(player.position, transform.position);
        if (distance <= maxDistance)
        {
            float forceMagnitude = pushForce * (maxDistance - distance) / maxDistance;
            playerRb.AddForce(direction * forceMagnitude, ForceMode2D.Impulse);
        }
    }
    void ApplyForceToPlayer2()
    {
        if (player.transform.position.y < transform.position.y)
        {
            Debug.Log(player.transform.position.x - transform.position.x);
            return;
        }

        if ((player.transform.position.x - transform.position.x <= 0.8f && player.transform.position.x - transform.position.x >= -0.25f))
        {
            Vector2 direction = Vector2.up; // Hướng lên trên
            float distance = Vector2.Distance(player.position, transform.position);
            if (distance <= maxDistance)
            {
                float forceMagnitude = pushForce * (maxDistance - distance) / maxDistance;
                playerRb.AddForce(direction * forceMagnitude, ForceMode2D.Impulse);
            }
        }

        
    }
    
}