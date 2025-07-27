using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) 
            SceneManager.LoadSceneAsync("Level " + SceneManage.level);

        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadSceneAsync("Level Select");
        


    }

    
}
