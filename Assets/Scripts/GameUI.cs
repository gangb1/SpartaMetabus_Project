using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : BaseUI
{
    TextMeshProUGUI scoreTxt;
    TextMeshProUGUI comboTxt;
    TextMeshProUGUI maxcomboTxt;

    protected override UIState GetUIState()
    {
        return UIState.Game;
    }

    public override void Init(UImanager uiManager)
    {
        base.Init(uiManager);

        scoreTxt = transform.Find("ScoreTxt").GetComponent<TextMeshProUGUI>();
        comboTxt = transform.Find("ComboTxt").GetComponent<TextMeshProUGUI>();
        maxcomboTxt = transform.Find("MaxComboTxt").GetComponent<TextMeshProUGUI>();



    }
    public void SetUI(int score, int combo, int maxcombo)
    {
        scoreTxt.text = score.ToString();
        comboTxt.text = combo.ToString();
        maxcomboTxt.text = maxcombo.ToString();
    }
}
