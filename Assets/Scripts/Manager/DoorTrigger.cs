using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private Door door;
    [SerializeField] private float interactDistance = 1.5f;
    [SerializeField] private string message = "Press [E]";

    private Transform player;
    private bool playerInRange = false;
    private UIController ui;

    private void Start()
    {
        ui = FindObjectOfType<UIController>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            float distance = Vector2.Distance(transform.position, player.position);
            if(distance <= interactDistance)
            {
                door.ToggleDoor();
                ui.HideText();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            player = collision.transform;
            playerInRange = true;
            ui.ShowText(message);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = false;
            player = null;
            ui.HideText();
        }
    }


}
