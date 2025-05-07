using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{   
    //메인게임 씬 전환
    public void GoMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    // 게임 종료
    public void GoExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
