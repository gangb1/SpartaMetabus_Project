using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneStartUI : BaseUI
{
    Button startBtn;
    Button exitBtn;

    protected override UIState GetUIState()
    {
        return UIState.Start;
    }

    public override void Init(UImanager uiMamager)
    {
        base.Init(uiMamager);

        startBtn = transform.Find("StartBtn").GetComponent<Button>();
        exitBtn = transform.Find("ExitBtn").GetComponent <Button>();

        startBtn.onClick.AddListener(OnClickMainGameButton);
        exitBtn.onClick.AddListener(OnClickExitButton);
    }

    void OnClickMainGameButton()
    {
        uiManager.OnClickMainGame();
    }

    void OnClickExitButton()
    {
        uiManager.OncClickExit();
    }
    
}
