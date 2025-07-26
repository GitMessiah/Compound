using UnityEngine;

public class Bullet : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health>().doDamage(1);
            Destroy(gameObject);
        }

    }

}
