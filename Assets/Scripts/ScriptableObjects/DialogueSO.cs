using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogueSO", menuName = "ScriptableObjects/DialogueSO")]
[System.Serializable]
public class DialogueSO : ScriptableObject
{
    [TextArea(3,5)] public string[] sentances;
}