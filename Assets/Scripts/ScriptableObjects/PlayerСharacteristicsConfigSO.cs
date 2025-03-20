using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewPlayerСharacteristicsConfig", menuName = "ScriptableObjects/PlayerСharacteristicsConfig")]
public class PlayerСharacteristicsConfigSO : ScriptableObject
{
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

    [Header("Characteristics")]
    public float health;
    public float effectsResistance;
    public float blockingDamage;

    public float stamina;
    public float attackSpeed;
    public float armor;
    
    public float mana;
    public float magicResistance;
    public float magicPower;
}
