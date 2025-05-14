using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputUI : MonoBehaviour
{
    [SerializeField] private Button magicSwordSkillButton;
    [SerializeField] private Button swordRainSkillButton;

    void Start()
    {
        magicSwordSkillButton.onClick.AddListener(() =>
        {
            InputManager.Instance.UsingMagicSwordSkill();
        });
        swordRainSkillButton.onClick.AddListener(() =>
        {
            InputManager.Instance.UsingSwordRainSkill();
        });
    }
}
