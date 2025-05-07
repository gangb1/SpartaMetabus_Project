using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionTrigger : MonoBehaviour
{

    [SerializeField] private float interactDistance = 1.5f;             //��ȣ�ۿ� �Ÿ� ���� ��
    [SerializeField] private string message = "Press [E]";              //ui�� ǥ���� �⺻ �ȳ� �޽���

    [SerializeField] private UnityEvent onInteract;                     //��ư�� ������ �� ������ UnityEvent(�� ����, �� ��ȯ ��)

    //���� ǥ�� ��� �ѱ�/���� �ɼ�
    [Header("Score On/Off")]
    [SerializeField] private bool showScore = false;
    [SerializeField] private string scoreKey = "HighScore";         //playerPrefs���� ����� Ű���� inspector���� ���� ����


    private Transform player;               //�÷��̾� ������Ʈ ��ġ ����
    private bool playerInRange = false;     //���� Ʈ���� ���� �ȿ� �ִ��� ����
    private UIController ui;                //uiǥ�ø� ���� ��Ʈ�ѷ� ����

    private void Start()
    {
        ui = UIController.instance;             //uicontroller �̱��� �ν��Ͻ� ����
    }
    private void Update()
    {
        //�÷��̾ ���� �ȿ� �ְ� EŰ�� ������ �� 
        if(playerInRange && Input.GetKeyDown(KeyCode.E))        
        {

            float distance = Vector2.Distance(transform.position, player.position);         //Ʈ���� ������Ʈ�� �÷��̾ �󸶳� ������ �ִ��� ���
            //��ȣ�ۿ� �Ÿ����� �����Ÿ��� �� ������
            if(distance <= interactDistance)
            {
                onInteract.Invoke();            //�̺�Ʈ ����
                ui.HideText();                  //ui�ؽ�Ʈ ����
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Ʈ���ſ� �±װ� player�� ������Ʈ�� ������ ����
        if(collision.CompareTag("Player"))
        {
            player = collision.transform;             //��ȣ�ۿ� �Ÿ� ����� ���� player ��ġ ����
            playerInRange = true;                   //��ȣ�ۿ� ��� ����

            string finalMessage = message;          //�ȳ� �޽��� ����

            //showScore�� true�̰� scoreKey�� null/�� ���ڿ��� �ƴ϶��
            if(showScore && !string.IsNullOrEmpty(scoreKey))
            {
                //playerPrefs���� �������� ������ �޽����� �߰�
                int score = PlayerPrefs.GetInt(scoreKey);
                finalMessage += $"\nBestScore: {score}";
            }
            ui.ShowText(finalMessage,showScore);        //ui��Ʈ�ѷ��� ���� �޽��� ǥ��
        }
    }

    //�÷��̾ Ʈ���� �������� ������ ��
    private void OnTriggerExit2D(Collider2D other)
    {
            playerInRange = false;  //��ȣ�ۿ� ��� ���� ����
            player = null;      //�÷��̾� ���� ����
        if(ui != null)      
        ui.HideText();      //�޽��� ����
    }


}
