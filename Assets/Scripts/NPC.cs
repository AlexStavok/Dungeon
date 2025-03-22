using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private DialogueSO dialogueSO;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InputManager.Instance.OnPlayerInteract += InputManager_OnPlayerInteract;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InputManager.Instance.OnPlayerInteract -= InputManager_OnPlayerInteract;
        }
    }
    private void InputManager_OnPlayerInteract(object sender, System.EventArgs e)
    {
        DialogueManager.Instance.StartDialogue(dialogueSO);
    }
}
