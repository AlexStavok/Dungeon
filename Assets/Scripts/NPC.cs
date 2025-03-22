using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueSO dialogueSO;

    public void Interact()
    {
        DialogueManager.Instance.StartDialogue(dialogueSO);
    }
}
