using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using TMPro;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{
    [SerializeField] private Transform skillSpawnPosition;

    [SerializeField] private MagicSword magicSwordPrefab;
    [SerializeField] private SwordRain swordRainPrefab;
    void Start()
    {
        InputManager.Instance.OnUsingMagicSwordSkill += InputManager_OnUsingMagicSwordSkill;
        InputManager.Instance.OnUsingSwordRainSkill += InputManager_OnUsingSwordRainSkill;
    }

    private void InputManager_OnUsingSwordRainSkill(object sender, System.EventArgs e)
    {
        if (Player.Instance.HasEnoughMana(swordRainPrefab.GetManaCost()))
        {
            Player.Instance.SubtractMana(swordRainPrefab.GetManaCost());

            SwordRain swordRain = Instantiate(swordRainPrefab, skillSpawnPosition.position, Quaternion.identity);
            Transform targetPosition = Player.Instance.GetTargetEnemy();

            if (targetPosition != null)
            {
                swordRain.GetComponent<SwordRain>().Initialize(targetPosition.position);
            }
            else
            {
                swordRain.GetComponent<SwordRain>().Initialize((Vector2)skillSpawnPosition.position + Player.Instance.GetLookDirection());
            }
        }
    }

    private void InputManager_OnUsingMagicSwordSkill(object sender, System.EventArgs e)
    {
        if (Player.Instance.HasEnoughMana(magicSwordPrefab.GetManaCost()))
        {
            Player.Instance.SubtractMana(magicSwordPrefab.GetManaCost());

            MagicSword sword = Instantiate(magicSwordPrefab, skillSpawnPosition.position, Quaternion.identity);
            Transform targetPosition = Player.Instance.GetTargetEnemy();

            if (targetPosition != null)
            {
                sword.GetComponent<MagicSword>().Initialize(skillSpawnPosition.position, targetPosition.position);
            }
            else
            {
                sword.GetComponent<MagicSword>().Initialize(skillSpawnPosition.position, (Vector2)skillSpawnPosition.position + Player.Instance.GetLookDirection());
            }
        }
    }
}
