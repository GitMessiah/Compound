using UnityEngine;

public class Health : MonoBehaviour
{

    public float health = 100;
    public bool dead = false;
    public bool invincible = false;

    float invincibleTimer;
    public float invincibleTime = 0.3f;

    SpriteRenderer sprite;

    public void doDamage(float damage)
    {
        if (!invincible)
        {
            health -= damage;
            SoundManager.PlaySound(SoundType.HURT, 0.1f);
            invincible = true;
        }
            
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        invincibleTimer = invincibleTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -20) health -= 9999;
        if (invincible)
        {
            invincibleTimer -= Time.deltaTime;
            sprite.color = Color.red;
        } else
        {
            sprite.color = Color.white;
        }

        if (invincibleTimer < 0)
        {
            invincibleTimer = invincibleTime;
            invincible = false;
        }

        if (health <= 0)
        {
            dead = true;
        }   
    }
}
