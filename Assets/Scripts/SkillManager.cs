using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using TMPro;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance;

    [SerializeField] private Transform skillSpawnPosition;


    [SerializeField] private MagicSwordSO magicSwordSO;
    [SerializeField] public int magicSwordLevel = 0;
    [SerializeField] private SwordRainSO swordRainSO;
    [SerializeField] public int swordRainLevel = 0;
    [SerializeField] private SwordCageSO swordCageSO;
    [SerializeField] public int swordCageLevel = 0;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        InputManager.Instance.OnUsingMagicSwordSkill += InputManager_OnUsingMagicSwordSkill;
        InputManager.Instance.OnUsingSwordRainSkill += InputManager_OnUsingSwordRainSkill;
        InputManager.Instance.OnUsingSwordCageSkill += InputManager_OnUsingSwordCageSkill;
    }

    private void InputManager_OnUsingMagicSwordSkill(object sender, System.EventArgs e)
    {
        if(magicSwordLevel > 0)
        {
            if (PlayerStats.Instance.HasEnoughMana(magicSwordSO.levels[magicSwordLevel - 1].manaCost))
            {
                PlayerStats.Instance.SubtractMana(magicSwordSO.levels[magicSwordLevel - 1].manaCost);

                MagicSword sword = Instantiate(magicSwordSO.skillPrefab, skillSpawnPosition.position, Quaternion.identity);
                Transform targetPosition = PlayerCombat.Instance.GetTargetEnemy();

                if (targetPosition != null)
                {
                    sword.Initialize(targetPosition.position, magicSwordSO.levels[magicSwordLevel - 1], PlayerStats.Instance.GetMagicPower());
                }
                else
                {
                    sword.Initialize((Vector2)skillSpawnPosition.position + PlayerMovement.Instance.GetLookDirection(), magicSwordSO.levels[magicSwordLevel - 1], PlayerStats.Instance.GetMagicPower());
                }
            }
        }
    }
    private void InputManager_OnUsingSwordRainSkill(object sender, System.EventArgs e)
    {
        if(swordRainLevel > 0)
        {
            if (PlayerStats.Instance.HasEnoughMana(swordRainSO.levels[magicSwordLevel - 1].manaCost))
            {
                PlayerStats.Instance.SubtractMana(swordRainSO.levels[magicSwordLevel - 1].manaCost);

                SwordRain swordRain = Instantiate(swordRainSO.skillPrefab, skillSpawnPosition.position, Quaternion.identity);
                Transform targetPosition = PlayerCombat.Instance.GetTargetEnemy();

                if (targetPosition != null)
                {
                    swordRain.GetComponent<SwordRain>().Initialize(targetPosition.position, swordRainSO.levels[magicSwordLevel - 1], PlayerStats.Instance.GetMagicPower());
                }
                else
                {
                    swordRain.GetComponent<SwordRain>().Initialize((Vector2)skillSpawnPosition.position + PlayerMovement.Instance.GetLookDirection(), swordRainSO.levels[magicSwordLevel - 1], PlayerStats.Instance.GetMagicPower());
                }
            }
        }
    }
    private void InputManager_OnUsingSwordCageSkill(object sender, EventArgs e)
    {
        if(swordCageLevel > 0)
        {
            if (PlayerStats.Instance.HasEnoughMana(swordCageSO.levels[magicSwordLevel - 1].manaCost))
            {
                PlayerStats.Instance.SubtractMana(swordCageSO.levels[magicSwordLevel - 1].manaCost);

                SwordCage swordCage = Instantiate(swordCageSO.skillPrefab, skillSpawnPosition.position, Quaternion.identity);
                Transform targetPosition = PlayerCombat.Instance.GetTargetEnemy();

                if (targetPosition != null)
                {
                    swordCage.GetComponent<SwordCage>().Initialize(targetPosition.position, swordCageSO.levels[magicSwordLevel - 1], PlayerStats.Instance.GetMagicPower());
                }
                else
                {
                    swordCage.GetComponent<SwordCage>().Initialize((Vector2)skillSpawnPosition.position + PlayerMovement.Instance.GetLookDirection(), swordCageSO.levels[magicSwordLevel - 1], PlayerStats.Instance.GetMagicPower());
                }
            }
        }
    }

    public float GetMagicSwordCooldown()
    {
        return magicSwordSO.levels[magicSwordLevel - 1].cooldown;
    }
    public float GetSwordCageCooldown()
    {
        return swordCageSO.levels[swordCageLevel - 1].cooldown;
    }
    public float GetSwordRainCooldown()
    {
        return swordRainSO.levels[swordRainLevel - 1].cooldown;
    }

    public void Save(ref SkillsData data)
    {
        data.magicSwordLevel = magicSwordLevel;
        data.swordRainLevel = swordRainLevel;
        data.swordCageLevel = swordCageLevel;
    }
    public void Load(SkillsData data)
    {
        magicSwordLevel = data.magicSwordLevel;
        swordRainLevel = data.swordRainLevel;
        swordCageLevel = data.swordCageLevel;

        InputUI.Instance.Show();
    }
}

[System.Serializable]
public struct SkillsData
{
    public int magicSwordLevel;
    public int swordRainLevel;
    public int swordCageLevel;
}