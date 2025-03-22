using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class DialogueUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private float textSpeed;

    private string sentance;

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void TypeSentance(string sentanceToType)
    {
        sentance = sentanceToType;
        textComponent.text = string.Empty;
        StartCoroutine(TypeLine());
    }

    private IEnumerator TypeLine()
    {
        foreach (char c in sentance.ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void ShowFullText()
    {
        StopAllCoroutines();
        textComponent.text = sentance;
    }

    public bool IsLineTyped()
    {
        return textComponent.text == sentance;
    }
}