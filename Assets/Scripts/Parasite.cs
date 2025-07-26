using UnityEngine;

public class Parasite : MonoBehaviour
{
    Transform player;
    Health health;
    Rigidbody2D rb;

    public float distanceToActivate;
    public float speed;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindFirstObjectByType<PlayerMovement>().transform;
        health = GetComponent<Health>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!health.dead)
        {

            Vector2 path = player.position - transform.position;
            if (path.magnitude < distanceToActivate)
            {
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
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !health.dead)
        {
            collision.gameObject.GetComponent<Health>().doDamage(1);
        }
    }



}
