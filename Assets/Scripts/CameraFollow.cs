using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject player;
    private Vector3 playerTransform;

    void Start()
    {

        player = FindFirstObjectByType<PlayerMovement>().gameObject;
    }
    void Update()
    {
        if (player != null)
        {
            FollowPlayer();
        }
    }
    void FollowPlayer()
    {
        playerTransform = player.transform.position;
        transform.position = new Vector3(playerTransform.x, playerTransform.y, -10);
    }
}
