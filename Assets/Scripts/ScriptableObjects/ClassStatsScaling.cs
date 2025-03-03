using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewClassStatScaling", menuName = "Game/Class Stat Scaling")]
public class ClassStatsScaling : ScriptableObject
{
    // How many attributes for each level,characteristics for each attribute, stats for each characteristic

    [Header("StartStats")]
    public float startHealth = 0f;
    public float startMana = 0f;
    public float startStamina = 0f;
    public float startMoveSpeed = 0f;
    public float startAttackSpeed = 0f;
    public float startPhysicalDamage = 0f;
    public float startMagicalDamage = 0f;

    [Header("Level -> Attributes")]

    public float strength = 0f;
    public float agility = 0f;
    public float intelligence = 0f;

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
    public float enduranceToMoveSpeed = 0f;
    public float enduranceToStamina = 0f;

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