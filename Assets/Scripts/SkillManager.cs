using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using TMPro;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField] private Transform skillSpawnPosition;

    [SerializeField] private MagicSword magicSwordPrefab;
    [SerializeField] private SwordRain swordRainPrefab;
    [SerializeField] private SwordCage swordCagePrefab;
    void Start()
    {
        InputManager.Instance.OnUsingMagicSwordSkill += InputManager_OnUsingMagicSwordSkill;
        InputManager.Instance.OnUsingSwordRainSkill += InputManager_OnUsingSwordRainSkill;
        InputManager.Instance.OnUsingSwordCageSkill += InputManager_OnUsingSwordCageSkill;
    }

    private void InputManager_OnUsingSwordCageSkill(object sender, EventArgs e)
    {
        if (PlayerStats.Instance.HasEnoughMana(swordCagePrefab.GetManaCost()))
        {
            PlayerStats.Instance.SubtractMana(swordCagePrefab.GetManaCost());

            SwordCage swordCage = Instantiate(swordCagePrefab, skillSpawnPosition.position, Quaternion.identity);
            Transform targetPosition = PlayerCombat.Instance.GetTargetEnemy();

            if (targetPosition != null)
            {
                swordCage.GetComponent<SwordCage>().Initialize(targetPosition.position);
            }
            else
            {
                swordCage.GetComponent<SwordCage>().Initialize((Vector2)skillSpawnPosition.position + PlayerMovement.Instance.GetLookDirection());
            }
        }
    }

    private void InputManager_OnUsingSwordRainSkill(object sender, System.EventArgs e)
    {
        if (PlayerStats.Instance.HasEnoughMana(swordRainPrefab.GetManaCost()))
        {
            PlayerStats.Instance.SubtractMana(swordRainPrefab.GetManaCost());

            SwordRain swordRain = Instantiate(swordRainPrefab, skillSpawnPosition.position, Quaternion.identity);
            Transform targetPosition = PlayerCombat.Instance.GetTargetEnemy();

            if (targetPosition != null)
            {
                swordRain.GetComponent<SwordRain>().Initialize(targetPosition.position);
            }
            else
            {
                swordRain.GetComponent<SwordRain>().Initialize((Vector2)skillSpawnPosition.position + PlayerMovement.Instance.GetLookDirection());
            }
        }
    }

    private void InputManager_OnUsingMagicSwordSkill(object sender, System.EventArgs e)
    {
        if (PlayerStats.Instance.HasEnoughMana(magicSwordPrefab.GetManaCost()))
        {
            PlayerStats.Instance.SubtractMana(magicSwordPrefab.GetManaCost());

            MagicSword sword = Instantiate(magicSwordPrefab, skillSpawnPosition.position, Quaternion.identity);
            Transform targetPosition = PlayerCombat.Instance.GetTargetEnemy();

            if (targetPosition != null)
            {
                sword.GetComponent<MagicSword>().Initialize(targetPosition.position);
            }
            else
            {
                sword.GetComponent<MagicSword>().Initialize((Vector2)skillSpawnPosition.position + PlayerMovement.Instance.GetLookDirection());
            }
        }
    }
}
