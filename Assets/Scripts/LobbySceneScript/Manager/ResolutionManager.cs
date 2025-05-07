using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResolutionManager : MonoBehaviour
{
    //������Ʈ ��ü���� �� �ϳ��� �����ϰ� �� ��
    private static ResolutionManager instance;          //�̱��� ���� ������ ���� ���� �ν��Ͻ� ����

    //�̱��� �ʱ�ȭ �� �̺�Ʈ ���
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);              //���� �ε��ص� ������Ʈ�� ������� �ʰ� ��
            SceneManager.sceneLoaded += OnSceneLoaded;      //���� �ε�ɶ����� ȣ��Ǵ� �̺�Ʈ�� OnSceneLoaded �Լ� ���
        }
        else
        {
            Destroy(gameObject);                //�ߺ� ������ ���� �ڱ� �ڽ��� �ı���
        }
    }

    //���� �ε�Ǹ� �ش� ���� �̸��� �ѱ�� SetResolutionForCurrentScene�޼��� ȣ��(LoadSceneMode �Ű������� ������� �����Ƿ� _ó��)
    private void OnSceneLoaded(Scene scene, LoadSceneMode _ )
    {
        SetResolutionForCurrentScene(scene.name);
    }

    //�ػ� ���� �޼���
    void SetResolutionForCurrentScene(string sceneName)
    {
        if (sceneName == "StartScene" || sceneName == "MainScene" || sceneName == "MiniGameScene(1)" || sceneName == "HiddenGameScene")
        {
            Screen.SetResolution(1280, 720, false);     //������(16:9)
        }
        else if(sceneName == "MiniGameScene(2)")
        {
            Screen.SetResolution(720,1280,false);        //������(9/16)
        }
        Debug.Log($"���� �ػ�: {Screen.width} x {Screen.height}");         //���� �ػ� ����� �ֿܼ� ���
    }

    //�޸� ���� ���� + ����ġ ���� �ߺ� ���� ����
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;      //�̺�Ʈ ����
    }
}
