using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Door : MonoBehaviour
{
    [SerializeField] private Sprite openDoorSprite;     //���� ������ �� ������ ��������Ʈ �̹���
    [SerializeField] private Sprite closeDoorSprite;    //���� ������ �� ������ ��������Ʈ �̹���
    [SerializeField] private bool isOpen = false;       //�ʱ� ���¿��� ���� �ִ��� ����
    [SerializeField] private Transform player;          //�÷��̾� ��ġ ����

    private BoxCollider2D boxCollider;                  //�浹 ó��(���� �����϶��� �浹 Ȱ��ȭ)
    private SpriteRenderer spriteRenderer;              //�� ��������Ʈ�� ǥ��


    //������Ʈ ���� ����
    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();                
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

    }

    // Start is called before the first frame update
    void Start()
    {
        //�÷��̾� null ���(�ν����Ϳ� �Ҵ� �ȵǸ� ���)
        if (player == null)
        {
            Debug.LogWarning("Player�� �Ҵ���� �ʾ� �ڵ����� �±׷� �˻��մϴ�.");
            player = GameObject.FindWithTag("Player")?.transform;
        }

        SetDoorState(isOpen);           //isOpen�� ���� �� ���� ����
        UpdateSortingOrder();           //���� ���� ����
    }

    //public void Interact()
    //{
    //    ToggleDoor();
    //}

    //�� ����/�ݱ� ���
    public void ToggleDoor()
    {
        isOpen = !isOpen;           //���� ���� ����
        SetDoorState(isOpen);       //����� ���¸� �������� �� ���� �ݿ�
    }

    //�� ���� ����
    private void SetDoorState(bool open)
    {   
        //���� ������ �� ���� ��������Ʈ�� ���� �ݶ��̴��� ��
        boxCollider.enabled = !open;
        spriteRenderer.sprite = open ? openDoorSprite: closeDoorSprite;

        //���� ������ sortingOrder 0 ����(order in layer�� ���� ���� ��)
        if(open)
        {
            spriteRenderer.sortingOrder = 0;
        }
        else
        {
            UpdateSortingOrder();       //���� �� �÷��̾�� ��ġ �� �� ���� ���� ����
        }
    }

    private void UpdateSortingOrder()
    {

        if (player == null) return;
        //���� �÷��̾�� �Ʒ��� ������ �÷��̾� �ڷ�, ���� ������ ������ ��
        spriteRenderer.sortingOrder = transform.position.y < player.position.y ? 200:0;
    }

}
