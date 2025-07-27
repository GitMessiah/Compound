using Unity.VisualScripting;
using UnityEngine;

public class Bacteria : MonoBehaviour
{
    Transform player;
    Health health;

    public Rigidbody2D rb;
    public float speed = 1;
    public float distanceToActivate = 25;

    float timer = 0f;
    bool played = false;
    public Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindFirstObjectByType<PlayerMovement>().transform;
        health = GetComponent<Health>();
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
                path.Normalize();
                rb.AddForce(path * speed);
                timer += Time.deltaTime;
                if (timer > 5f && Random.Range(0, 100) > 80)
                {
                    SoundManager.PlaySound(SoundType.BACTERIAPASSIVE, 0.5f);
                    timer = 0f;
                }
            }

        }
        else
        {
            if(!played) SoundManager.PlaySound(SoundType.BACTERIADEATH, 0.2f); played = true;
            animator.Play("Bacteria_Death");
        }
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !health.dead)
        {
            collision.gameObject.GetComponent<Health>().doDamage(1);
        }
    }
}
