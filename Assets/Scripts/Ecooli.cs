using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Ecooli : MonoBehaviour
{

    Transform player;
    Health health;
    Animator animator;
    Animator firepointAnimator;
    SpriteRenderer sprite;

    public Rigidbody2D rb;
    public GameObject firepoint;
    public SpriteRenderer gunSprite;

    public float speed;
    public float attackDistanceFromEcooli;
    public float runX = 5f;
    public float runY = 3f;

    public float reloadTime = 1;
    public float reloadTimer;

    public bool sunglassesOn = true;
    public bool sunglassesFallingOff = false;
    public bool onlyFallOnce = false;

    public float distanceToActivate = 25;
    float timer;
    bool played = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        reloadTimer = reloadTime;

        player = FindFirstObjectByType<PlayerMovement>().transform;
        health = GetComponent<Health>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        firepointAnimator = firepoint.GetComponent<Animator>();
        gunSprite = firepoint.GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!health.dead && !sunglassesFallingOff)
        {
            

            float distanceX = player.position.x - transform.position.x;
            float distanceY = player.position.y - transform.position.y;

            if (Mathf.Abs(distanceX) < runX && Mathf.Abs(distanceY) < runY)
            {
                if (distanceX > 0)
                {
                    rb.linearVelocityX = -speed;
                }
                if (distanceX < 0)
                {
                    rb.linearVelocityX = speed;
                }
            }



            Vector3 lookDir = player.position - transform.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            lookDir.Normalize();
            firepoint.transform.position = transform.position + lookDir * attackDistanceFromEcooli;
            firepoint.transform.rotation = Quaternion.Euler(0, 0, angle);


            if (angle > -90 && angle < 90)
            {
                gunSprite.flipY = false;
            }
            else
            {
                gunSprite.flipY = true;
            }

            if (reloadTimer < 0)
            {

                firepoint.GetComponent<Gun>().ShootAnimation();
                reloadTimer = reloadTime;

            }

            if ((player.transform.position - transform.position).magnitude < distanceToActivate)
            {
                reloadTimer -= Time.deltaTime;
            }

            if (health.health <= 20 && !onlyFallOnce)
                {
                    sunglassesOn = false;
                    sunglassesFallingOff = true;
                    onlyFallOnce = true;
                }

        } 

       

        Animate();

    }


 

    public void Animate()
    {

        

        if (!health.dead)
        {

            if (transform.position.x < player.transform.position.x)
            {
                sprite.flipX = true;
            }

            if (transform.position.x > player.transform.position.x)
            {
                sprite.flipX = false;
            }

            if (sunglassesOn && !sunglassesFallingOff)
            {

                if (Mathf.Abs(rb.linearVelocityX) > 0.01)
                {
                    animator.Play("Sunglasses_Run");
                }
                else
                {
                    animator.Play("Sunglass_Idle");
                }

            }
            else
            {

                if (sunglassesFallingOff)
                {
                    animator.Play("Sunglasses_Fall");

                }
                else
                {
                    if (Mathf.Abs(rb.linearVelocityX) > 0.01)
                    {
                        animator.Play("NoSunglasses_Run");
                    }
                    else
                    {
                        animator.Play("NoSunglass_Idle");
                    }
                }


            }
        } else
        {
            if(!played) SoundManager.PlaySound(SoundType.ECOOLIDEATH, 0.2f); played = true;
            animator.Play("Ecooli_Death");
        }
        
    }

    public void SunglassesFellOff()
    {
        sunglassesFallingOff = false;
        reloadTime = reloadTime / 2;
        reloadTimer = reloadTime;
    }

    public void Allign()
    {

        if (transform.position.x < player.transform.position.x)
        {
            transform.position += new Vector3(-2, -0.5f, 0);
        }

        if (transform.position.x > player.transform.position.x)
        {
            transform.position += new Vector3(2, -0.5f, 0);
        }

        if (health.dead)
        {
            Destroy(this.gameObject.GetComponent<BoxCollider2D>());
            Destroy(this.gameObject.GetComponent<Rigidbody2D>());
            Destroy(firepoint);

        }
    }

    

}
