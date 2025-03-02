using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewCharacteristicsStatScaling", menuName = "Game/Characteristics Stat Scaling")]
public class CharacteristicsStatScaling : ScriptableObject
{
    // How many characteristics and stats a player will get for each attribute

    [Header("Attributes -> Characteristics")]

    [Header("strength")]
    public float physicalPower = 0f;
    public float resistance = 0f;

    [Header("agility")]
    public float endurance = 0f;
    public float quickness = 0f;

    [Header("intelligence")]
    public float magicalPower = 0f;
    public float wisdom = 0f;

    [Header("Characteristics -> Stats")]

    [Header("physicalPower")]
    public float physicalPowerToPhysicalDamage = 0f;
    public float physicalPowerToHealth = 0f;

    [Header("resistance")]
    public float resistanceToPhysicalProtection = 0f;
    public float resistanceToHealth = 0f;

    [Header("endurance")]
    public float enduranceToStamina = 0f;
    public float enduranceToMoveSpeed = 0f;

    [Header("quickness")]
    public float quicknessToAttackSpeed = 0f;
    public float quicknessToStamina = 0f;

    [Header("magicalPower")]
    public float magicalPowerToMagicalDamage = 0f;
    public float magicalPowerToMana = 0f;

    [Header("wisdom")]
    public float wisdomToMagicalProtection = 0f;
    public float wisdomToMana = 0f;
}