using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{

    public string scene;

    public bool loadSceneOnEscape = false;

    public static int level = 1;



    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (loadSceneOnEscape && Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadSceneAsync(scene); 
        }

     
    }

    public void OnPlay()
    {
        SceneManager.LoadSceneAsync(scene);
        Debug.Log(scene);
    }

    
 
    public static void LoadLevel(int levelSelected)
    {
        level = levelSelected;
        SceneManager.LoadSceneAsync("Level " + levelSelected);
    }

}
