using System.Collections;
using UnityEngine;

public class Enemy_Start : MonoBehaviour
{
    [SerializeField] private AI_Enemy _enemy;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject obj;
    public float attackForce = 8f;
    private bool canAttack = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _enemy = GetComponent<AI_Enemy>(); 
    }

    private void Update()
    {
        if (canAttack)
        {
            Attack();
        }
    }

    // Hàm này được gọi từ animation event khi Idle animation kết thúc
    public void OnIdleAnimationEnd()
    {
        canAttack = true;
    }
    public void OnSpeedAnimationEnd()
    {
        float direction = transform.localScale.x > 0 ? -1 : 1;
        _enemy.speed = direction * 2f;
        
    }

    void Attack()
    {
        canAttack = false;
        float direction = transform.localScale.x > 0 ? -1 : 1;
        _enemy.speed = direction * attackForce;
        rb.velocity = new Vector2(_enemy.speed+direction, rb.velocity.y);
    }
    
}