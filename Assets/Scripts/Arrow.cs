using UnityEngine;

public class Arrow : MonoBehaviour
{


    public float damage = 10;
    public float knockback = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);

        } else if (collision.CompareTag("Friendly") || collision.CompareTag("Enemy"))
        {

            if (collision.CompareTag("Friendly"))
            {
                Health playerHealth = FindFirstObjectByType<PlayerMovement>().GetComponent<Health>();
                playerHealth.doDamage(1);
            }

            collision.GetComponent<Health>().doDamage(damage);
            Vector2 angle = collision.transform.position - transform.position;
            angle.Normalize();
            collision.GetComponent<Rigidbody2D>().AddForce(angle * knockback, ForceMode2D.Impulse);
            Destroy(this.gameObject);
        }
           
    }

}
