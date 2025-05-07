using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NpcManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI PlainBestScoreTxt;
    [SerializeField] private TextMeshProUGUI StackBestScoreTxt;
    [SerializeField] private Canvas canvas;

    protected AnimationHandler animationHandler;
    private int PlainBestScore;
    private int StackBestScore;

    private bool isActive;

    private void Awake()
    {
        animationHandler = GetComponent<AnimationHandler>();


    }
    public void ShowLeaderBoard()
    {
        
        if(isActive)
        {
            CloseLeaderBoard();
        }
        else
        {
            PlainBestScore = PlayerPrefs.GetInt("HighScore");
            StackBestScore = PlayerPrefs.GetInt("BestScore");

            PlainBestScoreTxt.text = PlainBestScore.ToString();
            StackBestScoreTxt.text = StackBestScore.ToString();

            canvas.gameObject.SetActive(true);
            animationHandler.Active();
            isActive = true;
        }
    }

    public void CloseLeaderBoard()
    {
        canvas.gameObject.SetActive(false);
        animationHandler.DeActive();
        isActive = false;
    }





}
