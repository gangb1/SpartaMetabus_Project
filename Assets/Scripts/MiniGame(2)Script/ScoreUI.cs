using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : BaseUI
{
    TextMeshProUGUI scoreTxt;
    TextMeshProUGUI comboTxt;
    TextMeshProUGUI bestScoreTxt;
    TextMeshProUGUI bestComboTxt;

    Button startBtn;
    Button exitBtn;

    protected override UIState GetUIState()
    {
        return UIState.Score;
    }

    public override void Init(UImanager uiManager)
    {
        base.Init(uiManager);

        scoreTxt = transform.Find("ScoreTxt").GetComponent<TextMeshProUGUI>();
        comboTxt = transform.Find("ComboTxt").GetComponent <TextMeshProUGUI>();
        bestScoreTxt = transform.Find("BestScoreTxt").GetComponent<TextMeshProUGUI>();
        bestComboTxt = transform.Find("BestComboTxt").GetComponent<TextMeshProUGUI>();

        startBtn = transform.Find("StartBtn").GetComponent<Button>();
        exitBtn = transform.Find("ExitBtn").GetComponent<Button>();

        startBtn.onClick.AddListener(OnClickStartBtn);
        exitBtn.onClick.AddListener(OnClickExitBtn);
    }

    public void SetUI(int score,int combo, int bestScore, int bestCombo)
    {
        scoreTxt.text = score.ToString();
        comboTxt.text = combo.ToString();
        bestScoreTxt.text = bestScore.ToString();
        bestComboTxt.text = bestCombo.ToString();

    }
    void OnClickStartBtn()
    {
        uiManager.OnClickStart();
    }
    void OnClickExitBtn()
    {
        uiManager.OncClickExit();
    }

}
