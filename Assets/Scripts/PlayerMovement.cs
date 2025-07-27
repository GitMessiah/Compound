using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    public float x;
    public float shotForce = 10f;
    public float speed = 5f;
    public float jumpForce = 10f;
    public float acceleration = 0.3f;
    public float attackDistanceFromPlayer = 1f;
    public float damage = 10; //MAX DAMAGE

    public float reloadSpeed = 100f;
    public float reloadSpeedTimer;

    public float charge = 0;
    public float maxCharge = 1;

    public Slider chargeBar;
    public float barX;

    public Rigidbody2D rb;
    public Camera cam;
    public Health dipHealth;

    public SpriteRenderer sprite;
    public SpriteRenderer bowSprite;

    public bool die = false;

    public bool grounded = false;
    public bool bowCharged = false;
    public bool bowCharging = false;

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
        reloadSpeedTimer = reloadSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        GroundedCheck();
        reloadSpeedTimer -= Time.deltaTime;

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        movementX = Input.GetAxisRaw("Horizontal");
        
        if (!dipHealth.dead)
        {
            

            if (Input.GetMouseButton(0) && reloadSpeedTimer <= 0)
            {

                if (charge < maxCharge)
                {
                    if (!bowCharging)
                    {
                        SoundManager.PlaySound(SoundType.BOWCHARGE, 0.25f);
                        bowCharging = true;
                    }
                    charge += Time.deltaTime;

                }
                else
                {
                    if (!bowCharged)
                    {
                        SoundManager.PlaySound(SoundType.BOWFULL, 0.5f);
                        bowCharged = true;
                    }
                    charge = maxCharge;
                }

                chargeBar.value = charge;
                chargeBar.maxValue = maxCharge;
                
            }
            else if (!Input.GetMouseButton(0) && charge > 0)
            {

                Shoot();
                bowCharged = false;
                bowCharging = false;
                charge = 0;
                chargeBar.value = 0;
                reloadSpeedTimer = reloadSpeed;

            }

            //Jump
            if (Input.GetKeyDown(KeyCode.Space) && grounded)
            {
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                grounded = false;
                SoundManager.PlaySound(SoundType.JUMP, 0.1f);
            }

        }

        if (!dipHealth.dead)
        {


            float velX = rb.linearVelocityX;

            if (velX <= speed && velX > -speed)
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
            SoundManager.PlaySound(SoundType.PLAYERDEATH);
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
        SoundManager.PlaySound(SoundType.BOWSHOT, 0.5f);
        g.GetComponent<Rigidbody2D>().AddForce(firepoint.right * shotForce * charge);
        g.GetComponent<Arrow>().damage = charge * damage;
        g.GetComponent<Arrow>().knockback *= charge * 0.66f;
        Debug.Log("Damage: " + (charge * damage));

    }

    void FixedUpdate()
    {

    } 

    void GroundedCheck()
    {
        Vector2 pos = transform.position + Vector3.down * (GetComponent<Collider2D>().bounds.extents.y + 0.05f);
        grounded = Physics2D.OverlapCircle(pos, x, LayerMask.GetMask("wall"));
    } 

    public void AfterDeath()
    {
        SceneManager.LoadSceneAsync("Death Screen");
    }

}
