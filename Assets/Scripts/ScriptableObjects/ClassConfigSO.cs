using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewClassConfig", menuName = "Game/Class Config")]
public class ClassConfigSO : ScriptableObject
{
    //Config for class

    [Header("StartAttributes")]
    public float startStrength;
    public float startAgility;
    public float startIntelligence;

    [Header("StartCharacteristics")]
    public float moveSpeed;

    [Header("AttributesIncrement")]
    public float strengthIncrement;
    public float agilityIncrement;
    public float intelligenceIncrement;
}