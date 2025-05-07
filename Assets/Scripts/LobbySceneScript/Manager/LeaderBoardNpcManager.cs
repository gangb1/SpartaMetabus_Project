using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderBoardNpcManager : MonoBehaviour
{
    //���� �ؽ�Ʈ�� ǥ���ϴ� UI �ؽ�Ʈ �ʵ�
    [SerializeField] private TextMeshProUGUI PlainBestScoreTxt;         
    [SerializeField] private TextMeshProUGUI StackBestScoreTxt;
    //���� UI ��ü�� ���δ� CANVAS
    [SerializeField] private Canvas canvas;

   //�ִϸ��̼� ���� Ŭ���� 
    protected AnimationHandler animationHandler;
    //���� ����� �ְ� ������ �ε� �Ŀ� �����ص� ������
    private int PlainBestScore;
    private int StackBestScore;
    //�������尡 �����ִ��� Ȯ�ο� bool��
    private bool isActive;

    private void Awake()
    {
        //������Ʈ ����
        animationHandler = GetComponent<AnimationHandler>();


    }
    public void ShowLeaderBoard()
    {
        //�̹� ���������� �ݱ�
        if(isActive)
        {
            CloseLeaderBoard();
        }
        //�������� ������
        else
        {   //�ְ����� �ҷ���
            PlainBestScore = PlayerPrefs.GetInt("HighScore");
            StackBestScore = PlayerPrefs.GetInt("BestScore");

            //�ؽ�Ʈ ui�� ���� ǥ��
            PlainBestScoreTxt.text = PlainBestScore.ToString();
            StackBestScoreTxt.text = StackBestScore.ToString();

            //canvas Ȱ��ȭ
            canvas.gameObject.SetActive(true);
            animationHandler.Active();      //�ִϸ��̼� ���
            isActive = true;                //���� �Ұ� true ����
        }
    }

    //�������� �ݱ�
    public void CloseLeaderBoard()
    {
        canvas.gameObject.SetActive(false);         //canvas ��Ȱ��ȭ
        animationHandler.DeActive();                //�ִϸ��̼� ����(������ �� �ݴ� �ִϸ��̼�)
        isActive = false;                           //���� �Ұ� ����
    }





}
