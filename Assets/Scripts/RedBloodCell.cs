using UnityEngine;

public class RedBloodCell : MonoBehaviour
{


    public float movementTime = 3f;
    public float movementTimer = 3f;

    public float force = 100f;

    public Rigidbody2D rb;

    public Health health;

    public Animator animator;

    public SpriteRenderer spriteRenderer;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movementTimer = Random.Range(1f, 3f);
        health = GetComponent<Health>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {


        

        if (!health.dead)
        {

            if (rb.linearVelocityX < 0)
                spriteRenderer.flipX = true;

            if (rb.linearVelocityX > 0)
                spriteRenderer.flipX = false;

            if (rb.linearVelocityX <= 0.01 && rb.linearVelocityX >= -0.01)
            {
                animator.Play("Nothing");
            }
            else
            {
                animator.Play("Red_Blood_Cell_Move");
            }
        }
           

        if (!health.dead && movementTimer <= 0)
        {
            float randomValue = Random.Range(-1f, 1f);
            rb.AddForce(new Vector2(randomValue * force, 0), ForceMode2D.Impulse);
            movementTimer = movementTime; 
        } else if (health.dead)
        {
            animator.Play("Red_Blood_Cell_Die");
            Destroy(this.gameObject.GetComponent<BoxCollider2D>());
            Destroy(this.gameObject.GetComponent<Rigidbody2D>());

        }



        movementTimer -= Time.deltaTime;

     
    }
}
