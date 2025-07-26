using UnityEngine;

public class Bomb : MonoBehaviour
{

    Transform player;
    Rigidbody2D rb;
    public float speed = 1f;

    [SerializeField]
    private float timer = 0;

    public float despawnTime = 5f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindFirstObjectByType<PlayerMovement>().transform;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (timer > despawnTime)
        {
            Destroy(this.gameObject);
        }

        Vector2 path = player.position - transform.position;
        rb.AddForce(path * speed);
        timer += Time.deltaTime;

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health>().doDamage(1);
            Destroy(gameObject);
        }
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }

}
