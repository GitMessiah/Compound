using UnityEngine;

public class HealthUI : MonoBehaviour
{

    public GameObject heart1;
    Animator animator1;

    public GameObject heart2;
    Animator animator2;

    public GameObject heart3;
    Animator animator3;

    public Health playerHealth;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        playerHealth = FindFirstObjectByType<PlayerMovement>().GetComponent<Health>();

        animator1 = heart1.GetComponent<Animator>();
        animator2 = heart2.GetComponent<Animator>();
        animator3 = heart3.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth.health < 3)
        {
            animator3.Play("Heart_Empty");
        } else
        {
            animator3.Play("Heart_Idle");
        }

        if (playerHealth.health < 2)
        {
            animator2.Play("Heart_Empty");
        } else
        {
            animator2.Play("Heart_Idle");
        }

        if (playerHealth.health < 1)
        {
            animator1.Play("Heart_Empty");
        }
        else
        {
            animator1.Play("Heart_Idle");
        }

        
    }
}
