using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResolutionManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void SetResolutionForCurrentScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "StartScene" || sceneName == "MainScene" || sceneName == "MiniGameScene(1)")
        {
            Screen.SetResolution(1280, 720, false);
        }
        else if(sceneName == "MiniGameScene(2)")
        {
            Screen.SetResolution(720,1280,false);
        }
    }
}
