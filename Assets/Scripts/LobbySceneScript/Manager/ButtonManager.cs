using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{   
    //���ΰ��� �� ��ȯ
    public void GoMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    // ���� ����
    public void GoExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
