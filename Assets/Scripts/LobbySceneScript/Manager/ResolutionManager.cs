using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResolutionManager : MonoBehaviour
{
    private static ResolutionManager instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode )
    {
        SetResolutionForCurrentScene(scene.name);
    }

    void SetResolutionForCurrentScene(string sceneName)
    {
        if (sceneName == "StartScene" || sceneName == "MainScene" || sceneName == "MiniGameScene(1)" || sceneName == "HiddenGameScene")
        {
            Screen.SetResolution(1280, 720, false);
        }
        else if(sceneName == "MiniGameScene(2)")
        {
            Screen.SetResolution(720,1280,false);
        }
        Debug.Log($"현재 해상도: {Screen.width} x {Screen.height}");
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;  
    }
}
