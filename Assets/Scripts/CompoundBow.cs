using UnityEngine;

public class CompoundBow : MonoBehaviour
{

    Animator animator;
    public PlayerMovement player;

    bool charging = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        if (player.charge > 0)
        {
            charging = true;
            animator.Play("CompoundBowCharge");
        }   
        if (player.charge == 0 && charging)
        {
            charging = false;
            animator.Play("CompoundBowDischarge");
        }
       
    }
}
