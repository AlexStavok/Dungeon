using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [SerializeField] private DialogueUI dialogueUI;

    private DialogueSO dialogueSO;
    private int currentSentanceIndex;
    private bool talking = false;

    private void Awake()
    {
        Instance = this;
    }
    public void StartDialogue(DialogueSO dialogueSOToSet)
    {
        dialogueSO = dialogueSOToSet;
        currentSentanceIndex = 0;
        dialogueUI.Show();
        talking = true;
        dialogueUI.TypeSentance(dialogueSO.sentances[currentSentanceIndex]);
    }
    private void NextLine()
    {
        if (currentSentanceIndex < dialogueSO.sentances.Length - 1)
        {
            currentSentanceIndex++;
            dialogueUI.TypeSentance(dialogueSO.sentances[currentSentanceIndex]);
        }
        else
        {
            dialogueUI.Hide();
            talking = true;
        }
    }
    private void Update()
    {
        if (!talking)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            if (dialogueUI.IsLineTyped())
            {
                NextLine();
            }
            else
            {
                dialogueUI.ShowFullText();
            }
        }
    }
}
