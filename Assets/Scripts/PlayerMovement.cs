using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public float shotForce = 10f;
    public float speed = 5f;
    public float jumpForce = 10f;
    public float acceleration = 0.3f;
    public float attackDistanceFromPlayer = 1f;
    public float damage = 10; //MAX DAMAGE

    public float reloadSpeed = 100f;
    public float reloadSpeedTimer;

    public float charge = 0;
    public float maxCharge = 3;

    public Transform chargeBar;
    public float barX;

    public Rigidbody2D rb;
    public Camera cam;
    public Health dipHealth;

    public SpriteRenderer sprite;
    public SpriteRenderer bowSprite;

    public bool die = false;

    public bool grounded = false;

    float movementX;
    Vector2 mousePos;


    public Transform firepoint;
    public GameObject bullet;

    public Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        barX = chargeBar.localScale.x;
        chargeBar.localScale = new Vector3(0, chargeBar.localScale.y);

        reloadSpeedTimer = reloadSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
        reloadSpeedTimer -= Time.deltaTime;

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        movementX = Input.GetAxisRaw("Horizontal");

        if (!dipHealth.dead)
        {

            if (Input.GetMouseButton(0) && reloadSpeedTimer <= 0)
            {

                if (charge < maxCharge)
                {
                    charge += Time.deltaTime;

                } else
                {
                    charge = maxCharge;
                }

                chargeBar.localScale = new Vector3((charge / maxCharge) * barX, chargeBar.localScale.y);
                
            } else if (!Input.GetMouseButton(0) && charge > 0) {

                Shoot();
                charge = 0;
                chargeBar.localScale = new Vector3(0, chargeBar.localScale.y);
                reloadSpeedTimer = reloadSpeed;

            }

            //Jump
            if (Input.GetKeyDown(KeyCode.Space) && grounded)
            {
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                grounded = false;
            }

        }

        if (!dipHealth.dead)
        {


            float velX = rb.linearVelocityX;

            if (velX < speed && velX > -speed)
            {
                rb.linearVelocityX += movementX * acceleration;
            }

            if (velX > 0 && movementX <= 0)
            {
                rb.linearVelocityX -= acceleration;
            }

            if (velX < 0 && movementX >= 0)
            {
                rb.linearVelocityX += acceleration;
            }


            //rb.AddForce(movement * speed * Time.fixedDeltaTime, ForceMode2D.Impulse);

            Vector3 lookDir = mousePos - rb.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            lookDir.Normalize();
            firepoint.position = transform.position + lookDir * attackDistanceFromPlayer;
            firepoint.rotation = Quaternion.Euler(0, 0, angle);

            if (angle > -90 && angle < 90)
            {
                bowSprite.flipY = false;
            }
            else
            {
                bowSprite.flipY = true;
            }

        }


        Animations();


    }

    void Animations()
    {

        if (dipHealth.dead && !die)
        {
            animator.Play("Dip_Death", -1, 0f);
            die = true;
        }
        else if (!dipHealth.dead)
        {

            if (movementX > 0)
            {
                sprite.flipX = false;
            }
            if (movementX < 0)
            {
                sprite.flipX = true;
            }
            
            if (movementX != 0)
            {
                animator.Play("Dip_Run");

            }
            else
            {
                animator.Play("Dip_Idle");
            }

        }


    }

    void Shoot()
    {
        GameObject g = Instantiate(bullet, firepoint.position, firepoint.rotation);

        g.GetComponent<Rigidbody2D>().AddForce(firepoint.right * shotForce * charge);
        g.GetComponent<Arrow>().damage = charge * damage;
        Debug.Log("Damage: " + (charge * damage));

    }

    void FixedUpdate()
    {
        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !dipHealth.dead)
        {
            grounded = true;
            if (rb.linearVelocityX > 0)
            {
                rb.linearVelocityX = speed;
            }
            if (rb.linearVelocityX < 0)
            {
                rb.linearVelocityX = -speed;
            }

        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !dipHealth.dead)
        {
            grounded = true;
        }
    }

    public void AfterDeath()
    {
        SceneManager.LoadSceneAsync("Death Screen");
    }

}
