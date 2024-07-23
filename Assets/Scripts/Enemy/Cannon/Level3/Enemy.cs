using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform playerTransform;
    public bool isAdvent = false;
    
    

    // Update is called once per frame
    private void Update()
    {
        if (playerTransform.GetComponent<MoveShip>().isMove)
        {
            AdventEnemy();
        }
    }

    void AdventEnemy()
    {
        if (playerTransform.position.x >= 80f && !isAdvent)
        {
            isAdvent = true;
            Transform[] childTransforms = gameObject.GetComponentsInChildren<Transform>(true);

            // Duyệt qua tất cả các Transform
            foreach (var childTransform in childTransforms)
            {
                // Bỏ qua đối tượng cha
                if (childTransform == gameObject.transform) continue;

                // Kích hoạt đối tượng con
                childTransform.gameObject.SetActive(true);
            }
        }
        
        if (isAdvent && transform.position.x - playerTransform.position.x <= 14.8f)
        {
            // Move the enemy forward
            transform.Translate(new Vector2(4f * Time.deltaTime, 0f));
        }
        else if (!isAdvent)
        {
            // Move the enemy forward if not in advent state
            transform.Translate(new Vector2(4f * Time.deltaTime, 0f));
        }
    }
}