using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    //�Ϲ����� �˸� �޽����� �����ִ� �г�
    [Header("default tooltip Panel")]
    [SerializeField] private GameObject defaultPanel;
    [SerializeField] private TextMeshProUGUI defualtText;

    //���� ���� �޽��� �г�
    [Header("Button tooltip Panel")]
    [SerializeField] private GameObject ScorePanel;
    [SerializeField] private TextMeshProUGUI ScoreText;

    //�̱��� �ν��Ͻ�
    public static UIController instance { get; private set; }

    //�̱��� ����
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //�޽��� ǥ�� �Լ�
    public void ShowText(string message, bool useScorePanel = false)
    {
        HideText();     //��� UI ���� ����

        if(useScorePanel)           //useScorePanel�� true�̸� scorePanel�� ǥ��
        {
            ScorePanel.SetActive(true);             
            ScoreText.text = message;
        }
        else                        //false�̸� defaultPanel�� ǥ��
        {
            defaultPanel.SetActive(true);
            defualtText.text = message;
        }
    }

    //��� �ؽ�Ʈ �����
    public void HideText()
    {
        defaultPanel.SetActive(false);
        ScorePanel.SetActive(false);
        if (defaultPanel != null) defaultPanel.SetActive(false);
        if (ScorePanel != null) ScorePanel.SetActive(false);
    }

    //ui�� �����ִ��� ���� Ȯ��
    public bool IsActive()
    {
        return defaultPanel.activeSelf || ScorePanel.activeSelf;
    }
}
