using UnityEngine;

public class JumpDamage : MonoBehaviour
{
    public Animator anim;
    public float jumpForce;
    public int lifes;
    public float time = 0f;
    private bool check = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
       
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if ( other.transform.CompareTag("Player") && !check)
        {
            if (other.transform.position.y > transform.position.y)
            {
                other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up*jumpForce;
                LosseLifeAndHit();
                AudioManager.Instance.PlaySFX(AudioManager.Instance.hitEnemy);
                CheckLife();
            }
        }
    }

    public void LosseLifeAndHit()
    {
        anim.SetTrigger("Hit");
        lifes--;
    }

    public void CheckLife()
    {
        if (lifes == 0)
        {
            anim.SetBool("Dead", true);
            gameObject.layer = 1;
            
            foreach (Transform child in transform)
            {
                if(child.name != "Ground")
                    child.gameObject.SetActive(false);
            }
        }
    }
}