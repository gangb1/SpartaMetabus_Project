using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResolutionManager : MonoBehaviour
{
    //프로젝트 전체에서 단 하나만 존재하게 끔 함
    private static ResolutionManager instance;          //싱글톤 패턴 구현을 위한 정적 인스턴스 변수

    //싱글톤 초기화 및 이벤트 등록
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);              //씬을 로드해도 오브젝트가 사라지지 않게 함
            SceneManager.sceneLoaded += OnSceneLoaded;      //씬이 로드될때마다 호출되는 이벤트에 OnSceneLoaded 함수 등록
        }
        else
        {
            Destroy(gameObject);                //중복 방지를 위해 자기 자신을 파괴함
        }
    }

    //씬이 로드되면 해당 씬의 이름을 넘기고 SetResolutionForCurrentScene메서드 호출(LoadSceneMode 매개변수를 사용하지 않으므로 _처리)
    private void OnSceneLoaded(Scene scene, LoadSceneMode _ )
    {
        SetResolutionForCurrentScene(scene.name);
    }

    //해상도 설정 메서드
    void SetResolutionForCurrentScene(string sceneName)
    {
        if (sceneName == "StartScene" || sceneName == "MainScene" || sceneName == "MiniGameScene(1)" || sceneName == "HiddenGameScene")
        {
            Screen.SetResolution(1280, 720, false);     //가로형(16:9)
        }
        else if(sceneName == "MiniGameScene(2)")
        {
            Screen.SetResolution(720,1280,false);        //세로형(9/16)
        }
        Debug.Log($"현재 해상도: {Screen.width} x {Screen.height}");         //현재 해상도 디버그 콘솔에 출력
    }

    //메모리 누수 방지 + 예기치 않은 중복 실행 방지
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;      //이벤트 해제
    }
}
