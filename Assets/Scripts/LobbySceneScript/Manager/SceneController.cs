using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void goMiniGameScene()
    {
        SceneManager.LoadScene("MiniGameScene(1)");
    }
    
    public void goMiniGameScene2()
    {
        SceneManager.LoadScene("MiniGameScene(2)");
    }
}
