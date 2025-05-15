using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputUI : MonoBehaviour
{
    [SerializeField] private Button magicSwordSkillButton;
    [SerializeField] private Button swordRainSkillButton;
    [SerializeField] private Button swordCageSkillButton;

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
        swordCageSkillButton.onClick.AddListener(() =>
        {
            InputManager.Instance.UsingSwordCageSkill();
        });
    }
}
