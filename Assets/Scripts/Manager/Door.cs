using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Sprite openDoorSprite;
    [SerializeField] private Sprite closeDoorSprite;
    [SerializeField] private bool isOpen = false;

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
    }
}
