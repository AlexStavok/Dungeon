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

    [Header("Ñharacteristics")]
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
    private float currentMoveSpeed;

    [Header("Level")]
    [SerializeField] private float targetExperience;
    [SerializeField] private float currentExperience;
    [SerializeField] private int level;
    [SerializeField] private int characteristicPoints;
    [SerializeField] private int skillPoints;


    [Header("Other")]
    [SerializeField] private PlayerÑharacteristicsConfigSO ñharacteristicsConfigSO;


    public event EventHandler OnExperienceChanged;
    public event EventHandler OnHealthChanged;
    public event EventHandler OnManaChanged;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        SetStartAttributes();
        CalculateCharacteristics();
    }

    private void SetStartAttributes()
    {
        if (ñharacteristicsConfigSO == null)
            return;

        strength = ñharacteristicsConfigSO.startStrength;
        agility = ñharacteristicsConfigSO.startAgility;
        intelligence = ñharacteristicsConfigSO.startIntelligence;

        baseMoveSpeed = ñharacteristicsConfigSO.moveSpeed;
        currentMoveSpeed = baseMoveSpeed;

        level = 1;
        targetExperience = ñharacteristicsConfigSO.startTargetExperience;
        currentExperience = 0f;
        OnExperienceChanged?.Invoke(this, EventArgs.Empty);
    }
    private void CalculateCharacteristics()
    {
        if (ñharacteristicsConfigSO == null)
            return;
        maxHealth = strength * ñharacteristicsConfigSO.health;
        effectsResistance = strength * ñharacteristicsConfigSO.effectsResistance;
        blockingDamage = strength * ñharacteristicsConfigSO.blockingDamage;

        attackSpeed = agility * ñharacteristicsConfigSO.attackSpeed;
        armor = agility * ñharacteristicsConfigSO.armor;

        maxMana = intelligence * ñharacteristicsConfigSO.mana;
        magicResistance = intelligence * ñharacteristicsConfigSO.magicResistance;
        magicPower = intelligence * ñharacteristicsConfigSO.magicPower;

        health = maxHealth;
        mana = maxMana;

        OnHealthChanged?.Invoke(this, EventArgs.Empty);
        OnManaChanged?.Invoke(this, EventArgs.Empty);
    }

    public void GetExperience(float experience)
    {
        currentExperience += experience;

        OnExperienceChanged?.Invoke(this, EventArgs.Empty);

        CheckExperience();
    }

    private void CheckExperience()
    {
        if (currentExperience >= targetExperience)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        currentExperience -= targetExperience;
        targetExperience = targetExperience + ñharacteristicsConfigSO.targetExperienceIncrement;

        level++;
        skillPoints++;
        characteristicPoints++;

        strength += ñharacteristicsConfigSO.strengthIncrement;
        agility += ñharacteristicsConfigSO.agilityIncrement;
        intelligence += ñharacteristicsConfigSO.intelligenceIncrement;

        OnExperienceChanged?.Invoke(this, EventArgs.Empty);

        CalculateCharacteristics();

        CheckExperience();
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
                health -= (float)Math.Round(damage.damageAmount * (1 - (magicResistance / 100)), 1);
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
    public float GetTargetExperience()
    {
        return targetExperience;
    }
    public float GetCurrentExperience()
    {
        return currentExperience;
    }
    public int GetLevel()
    {
        return level;
    }
    public int GetSkillPoints()
    {
        return skillPoints;
    }
    public int GetCharacteristicPoints()
    {
        return characteristicPoints;
    }
    public void SubtractSkillPoints()
    {
        skillPoints--;
    }
    public float GetMagicPower()
    {
        return magicPower;
    }
    public float GetAttackSpeed()
    {
        return attackSpeed / 100;
    }

    public void Save(ref PlayerStatsData data)
    {
        data.position = transform.position;
        data.health = health;
        data.mana = mana;
        data.targetExperience = targetExperience;
        data.currentExperience = currentExperience;
        data.level = level;
        data.skillPoints = skillPoints;
    }
    public void Load(PlayerStatsData data)
    {
        transform.position = data.position;
        health = data.health;
        mana = data.mana;
        targetExperience = data.targetExperience;
        currentExperience = data.currentExperience;
        level = data.level;
        skillPoints = data.skillPoints;

        SettingsAfterLoad();
    }

    private void SettingsAfterLoad()
    {
        strength = ñharacteristicsConfigSO.startStrength + ((level - 1) * ñharacteristicsConfigSO.strengthIncrement);
        agility = ñharacteristicsConfigSO.startAgility + ((level - 1) * ñharacteristicsConfigSO.agilityIncrement);
        intelligence = ñharacteristicsConfigSO.startIntelligence + ((level - 1) * ñharacteristicsConfigSO.intelligenceIncrement);

        maxHealth = strength * ñharacteristicsConfigSO.health;
        effectsResistance = strength * ñharacteristicsConfigSO.effectsResistance;
        blockingDamage = strength * ñharacteristicsConfigSO.blockingDamage;

        attackSpeed = agility * ñharacteristicsConfigSO.attackSpeed;
        armor = agility * ñharacteristicsConfigSO.armor;

        maxMana = intelligence * ñharacteristicsConfigSO.mana;
        magicResistance = intelligence * ñharacteristicsConfigSO.magicResistance;
        magicPower = intelligence * ñharacteristicsConfigSO.magicPower;



        OnExperienceChanged?.Invoke(this, EventArgs.Empty);
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
        OnManaChanged?.Invoke(this, EventArgs.Empty);
    }
}

[System.Serializable]
public struct PlayerStatsData
{
    public Vector3 position;

    public float health;
    public float mana;
    public float targetExperience;
    public float currentExperience;
    public int level;
    public int skillPoints;
}
