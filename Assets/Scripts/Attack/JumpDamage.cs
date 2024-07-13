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
            if (other.transform.position.y > transform.position.y)
            {
                other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up*jumpForce;
                LosseLifeAndHit();
                CheckLife();
                time = 0.25f;
                check = true;
            }
        }
    }

    public void LosseLifeAndHit()
    {
        lifes--;
        anim.SetTrigger("Hit");
    }

    public void CheckLife()
    {
        if (lifes <= 0)
        {
            anim.SetBool("Dead", true);
            gameObject.layer = 1;

            // Tắt tất cả các object con
            foreach (Transform child in transform)
            {
                if(child.name != "Ground")
                    child.gameObject.SetActive(false);
            }
        }
    }
}