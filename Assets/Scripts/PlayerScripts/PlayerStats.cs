using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    [Header("Attributes")]
    [SerializeField] private float strength;
    [SerializeField] private float agility;
    [SerializeField] private float intelligence;

    [SerializeField] private float strengfth;
    [SerializeField] private float agilityf;
    [SerializeField] private float intellifgence;

    [Header("�haracteristics")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float health;
    [SerializeField] private float maxMana;
    [SerializeField] private float mana;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float magicPower;
    [SerializeField] private float blockingDamage;
    [SerializeField] private float armor;
    [SerializeField] private float magicResistance;
    [SerializeField] private float effectsResistance;

    [Space(5)]
    [SerializeField] private float baseMoveSpeed;
    [SerializeField] private float runSpeedMultiplier;
    [SerializeField] private float staminaDrainRate;
    [SerializeField] private float staminaRegenRate;
    private float currentMoveSpeed;

    [Header("Level")]
    [SerializeField] private int level;
    [SerializeField] private int characteristicPoints;
    [SerializeField] private int skillPoints;


    [Header("Other")]
    [SerializeField] private Player�haracteristicsConfigSO �haracteristicsConfigSO;


    public event EventHandler OnHealthChanged;
    public event EventHandler OnManaChanged;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        SetStartAttributes();
        CalculateCharacteristics();
    }

    private void SetStartAttributes()
    {
        if (�haracteristicsConfigSO == null)
            return;
        strength = �haracteristicsConfigSO.startStrength;
        agility = �haracteristicsConfigSO.startAgility;
        intelligence = �haracteristicsConfigSO.startIntelligence;

        baseMoveSpeed = �haracteristicsConfigSO.moveSpeed;
        currentMoveSpeed = baseMoveSpeed;
        level = 1;
    }
    private void CalculateCharacteristics()
    {
        if (�haracteristicsConfigSO == null)
            return;
        maxHealth = strength * �haracteristicsConfigSO.health;
        effectsResistance = strength * �haracteristicsConfigSO.effectsResistance;
        blockingDamage = strength * �haracteristicsConfigSO.blockingDamage;

        attackSpeed = agility * �haracteristicsConfigSO.attackSpeed;
        armor = agility * �haracteristicsConfigSO.armor;

        maxMana = intelligence * �haracteristicsConfigSO.mana;
        magicResistance = intelligence * �haracteristicsConfigSO.magicResistance;
        magicPower = intelligence * �haracteristicsConfigSO.magicPower;

        health = maxHealth;
        mana = maxMana;

        OnHealthChanged?.Invoke(this, EventArgs.Empty);
        OnManaChanged?.Invoke(this, EventArgs.Empty);
    }

    public void LevelUp()
    {
        level++;

        strength += �haracteristicsConfigSO.strengthIncrement;
        agility += �haracteristicsConfigSO.agilityIncrement;
        intelligence += �haracteristicsConfigSO.intelligenceIncrement;

        CalculateCharacteristics();
    }
    public void TakeDamage(Damage damage)
    {
        switch (damage.damageType)
        {
            case Damage.DamageType.Physical:
                float finalDamage = Math.Max(0, (damage.damageAmount - blockingDamage) * (1 - (armor / 100)));
                health -= (float)Math.Round(finalDamage, 1);
                health = (float)Math.Round(health, 1);
                break;
            case Damage.DamageType.Magical:
                health -= (float)Math.Round(damage.damageAmount * (1 - magicResistance), 1);
                health = (float)Math.Round(health, 1);
                break;
        }
        DeathHandler();
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
    }
    private void DeathHandler()
    {
        if (health <= 0)
        {
            Player.Instance.DeathHandler();
        }
    }
    public float GetCurrentMoveSpeed()
    {
        return currentMoveSpeed;
    }
    public float GetHealth()
    {
        return health;
    }
    public float GetMaxHealth()
    {
        return maxHealth;
    }
    public void AddHeal(float amount)
    {
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
    }
    public float GetMana()
    {
        return mana;
    }
    public float GetMaxMana()
    {
        return maxMana;
    }
    public void AddMana(float amount)
    {
        mana += amount;
        if (mana > maxMana)
        {
            mana = maxMana;
        }
        OnManaChanged?.Invoke(this, EventArgs.Empty);

    }
    public void SubtractMana(float manaToSubtract)
    {
        mana -= manaToSubtract;
        if (mana < 0)
        {
            mana = 0;
        }
        OnManaChanged?.Invoke(this, EventArgs.Empty);
    }
    public bool HasEnoughMana(float checkMana)
    {
        return mana >= checkMana;
    }
}
