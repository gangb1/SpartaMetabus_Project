using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Door : MonoBehaviour
{
    [SerializeField] private Sprite openDoorSprite;
    [SerializeField] private Sprite closeDoorSprite;
    [SerializeField] private bool isOpen = false;
    [SerializeField] private Transform player;

    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

    }

    // Start is called before the first frame update
    void Start()
    {
        SetDoorState(isOpen);
        UpdateSortingOrder();
    }

    public void Interact()
    {
        ToggleDoor();
    }

    public void ToggleDoor()
    {
        isOpen = !isOpen;
        SetDoorState(isOpen);
    }

    private void SetDoorState(bool open)
    {
        boxCollider.enabled = !open;
        spriteRenderer.sprite = open ? openDoorSprite: closeDoorSprite;

        if(open)
        {
            spriteRenderer.sortingOrder = 0;
        }
        else
        {
            UpdateSortingOrder();
        }
    }

    private void UpdateSortingOrder()
    {
        if(player == null)
        {
            player = GameObject.FindWithTag("Player")?.transform;
        }

        if (player == null) return;

        spriteRenderer.sortingOrder = transform.position.y < player.position.y ? 200:0;
    }

}
