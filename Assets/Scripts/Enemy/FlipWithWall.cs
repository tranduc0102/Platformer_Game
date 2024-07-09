using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipWithWall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<AI_Enemy>().flip = true;
        }

        if (other.CompareTag("Untagged"))
        {
            
        }
    }
}
