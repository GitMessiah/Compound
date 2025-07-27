using UnityEngine;

public class Parasite : MonoBehaviour
{
    Transform player;
    Health health;
    Rigidbody2D rb;
    Animator animator;

    public float distanceToActivate;
    public float speed;
    bool played = false;
    float timer = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindFirstObjectByType<PlayerMovement>().transform;
        health = GetComponent<Health>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!health.dead)
        {

            Vector2 path = player.position - transform.position;
            if (path.magnitude < distanceToActivate)
            {
                timer += Time.deltaTime;
                if (timer > 5f && Random.Range(0, 100) > 80)
                {
                    SoundManager.PlaySound(SoundType.PARASITEPASSIVE, 0.5f);
                    timer = 0f;
                }
                if (player.position.x - transform.position.x > 0)
                {

                    rb.AddForce(new Vector2(speed, 0));
                    //rb.linearVelocityX = speed;
                }
                if (player.position.x - transform.position.x < 0)
                {
                    rb.AddForce(new Vector2(-speed, 0));
                    //rb.linearVelocityX = -speed;
                }

            }

        }
        else
        {
            if(!played) SoundManager.PlaySound(SoundType.PARASITEDEATH, 0.2f); played = true;
            animator.Play("Parasite_Death");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !health.dead)
        {
            collision.gameObject.GetComponent<Health>().doDamage(1);
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }



}
