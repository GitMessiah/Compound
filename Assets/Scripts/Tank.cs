using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Tank : MonoBehaviour
{


    Transform player;
    Health health;
    Rigidbody2D rb;
    SpriteRenderer sprite;
    Animator animator;

    public GameObject bomb;
    public Transform firepoint;

    public float distanceToActivate = 25f;
    public float distanceRun = 5f;
    public float speed = 1f;
    public float bulletSpeed = 5f;
    public float distanceFromTank = 1f;
    public float distanceFromTankY = 0.5f;

    public float reloadTime = 1;
    float reloadTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


        player = FindFirstObjectByType<PlayerMovement>().transform;
        health = GetComponent<Health>();
        rb = GetComponent<Rigidbody2D>();
        reloadTimer = reloadTime;
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {
        if (!health.dead)
        {
            float distance = player.position.x - transform.position.x;

            Vector2 path = player.position - transform.position;

            reloadTimer -= Time.deltaTime;

            if (reloadTimer < 0 && path.magnitude < distanceToActivate)
            {
                reloadTimer = reloadTime;

                if (distance > 0)
                {
                    firepoint.position = new Vector2(transform.position.x + distanceFromTank, transform.position.y + distanceFromTankY);
                    GameObject g = Instantiate(bomb, firepoint.position, firepoint.rotation);
                    g.GetComponent<Rigidbody2D>().AddForce(new Vector2(bulletSpeed, 0));
                } else
                {
                    firepoint.position = new Vector2(transform.position.x - distanceFromTank, transform.position.y + distanceFromTankY);
                    GameObject g = Instantiate(bomb, firepoint.position, firepoint.rotation);
                    g.GetComponent<Rigidbody2D>().AddForce(new Vector2(-bulletSpeed, 0));
                }
            }


            if (path.magnitude < distanceRun) 
            {
                if (distance > 0)
                {
                    sprite.flipX = false;
                    rb.AddForce(new Vector2(-speed, 0));
                }
                if (distance < 0)
                {

                    sprite.flipX = true;
                    rb.AddForce(new Vector2(speed, 0));
             
                }


            } else if (path.magnitude < distanceRun * 2)
            {
                if (distance > 0)
                {
                    sprite.flipX = false;
                } else if (distance < 0) {
                    sprite.flipX = true;
                }
            } else if (path.magnitude < distanceToActivate)
            {

                if (distance > 0)
                {
                    sprite.flipX = false;
                    rb.AddForce(new Vector2(speed, 0));
                }
                if (distance < 0)
                {
                    sprite.flipX = true;
                    rb.AddForce(new Vector2(-speed, 0));
                }

            }

        }
        else
        {
            animator.Play("Tank_Death");
        }



    }

    public void Die()
    {
        Destroy(gameObject);
    }

}
