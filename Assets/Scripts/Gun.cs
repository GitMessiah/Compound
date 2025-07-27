using UnityEngine;

public class Gun : MonoBehaviour
{

    public GameObject bullet;
    public float bulletSpeed = 5;
    Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShootAnimation()
    {
        animator.Play("Fire_Gun");
    }

    public void Shoot()
    {
        GameObject g = Instantiate(bullet, transform.position, transform.rotation);
        SoundManager.PlaySound(SoundType.ECOOLISHOOT, 0.2f);
        g.GetComponent<Rigidbody2D>().AddForce(transform.right * bulletSpeed, ForceMode2D.Impulse);
    }
}
