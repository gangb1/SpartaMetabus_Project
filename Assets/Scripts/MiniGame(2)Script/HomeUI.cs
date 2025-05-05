using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeUI :  BaseUI
{
    Button startBtn;
    Button exitBtn;

    protected override UIState GetUIState()
    {
        return UIState.Home;
    }

    public override void Init(UImanager uiManager)
    {
        base.Init(uiManager);                   //BaseUI.uiManager 설정
        
        //버튼 찾아서 연결
        startBtn = transform.Find("StartBtn").GetComponent<Button>();
        exitBtn = transform.Find("ExitBtn").GetComponent <Button>();

        //버튼에 이벤트 연결
        startBtn.onClick.AddListener(OnClickStartButton);
        exitBtn.onClick.AddListener(OnClickExitButton);
    }

    void OnClickStartButton()
    {
        uiManager.OnClickStart();
    }
    void OnClickExitButton()
    {
        uiManager.OncClickExit();
    }
}
