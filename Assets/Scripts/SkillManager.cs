using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using TMPro;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField] Transform skillSpawnPosition;

    [SerializeField] MagicSword magicSwordPrefab;
    void Start()
    {
        InputManager.Instance.OnUsingMagicSwordSkill += InputManager_OnUsingMagicSwordSkill;
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

    void Update()
    {
        
    }
}
