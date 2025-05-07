using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionTrigger : MonoBehaviour
{

    [SerializeField] private float interactDistance = 1.5f;
    [SerializeField] private string message = "Press [E]";

    [SerializeField] private UnityEvent onInteract;

    [Header("Score On/Off")]
    [SerializeField] private bool showScore = false;
    [SerializeField] private string scoreKey = "HighScore";


    private Transform player;
    private bool playerInRange = false;
    private UIController ui;

    private void Start()
    {
        ui = UIController.instance;
    }
    private void Update()
    {
        if(playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            float distance = Vector2.Distance(transform.position, player.position);
            if(distance <= interactDistance)
            {
                onInteract.Invoke();
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

            string finalMessage = message;

            if(showScore && !string.IsNullOrEmpty(scoreKey))
            {
                int score = PlayerPrefs.GetInt(scoreKey);
                finalMessage += $"\nBestScore: {score}";
            }
            ui.ShowText(finalMessage,showScore);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
            playerInRange = false;
            player = null;
        if(ui != null)
        ui.HideText();
    }


}
