using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelingMenuUI : MonoBehaviour
{

    [Header("MagicSword")]
    [SerializeField] private Button magicSwordLevelUPButton;
    [SerializeField] private Image magicSwordImage;
    [SerializeField] private Image magicSwordLevelImage;

    [Header("SwordCage")]
    [SerializeField] private Button swordCageLevelUPButton;
    [SerializeField] private Image swordCageImage;
    [SerializeField] private Image swordCageLevelImage;

    [Header("SwordRain")]
    [SerializeField] private Button swordRainLevelUPButton;
    [SerializeField] private Image swordRainImage;
    [SerializeField] private Image swordRainLevelImage;

    [Header("Points")]
    [SerializeField] private TextMeshProUGUI skillPointsText;

    [Header("Other")]
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Button exitButton;
    [SerializeField] private Sprite[] skillLevels;
    [SerializeField] private InputUI inputUI;

    private int skillPoints;
    private int characteristicPoints;
    void Start()
    {
        magicSwordLevelUPButton.onClick.AddListener(() =>
        {
            PlayerStats.Instance.SubtractSkillPoints();
            SkillManager.Instance.magicSwordLevel++;
            UpdateUI();
        });
        swordRainLevelUPButton.onClick.AddListener(() =>
        {
            PlayerStats.Instance.SubtractSkillPoints();
            SkillManager.Instance.swordRainLevel++;
            UpdateUI();
        });
        swordCageLevelUPButton.onClick.AddListener(() =>
        {
            PlayerStats.Instance.SubtractSkillPoints();
            SkillManager.Instance.swordCageLevel++;
            UpdateUI();
        });
        exitButton.onClick.AddListener(() =>
        {
            Hide();
        });
    }
    private void UpdateUI()
    {
        magicSwordImage.color = SkillManager.Instance.magicSwordLevel > 0 ? Color.white : Color.grey;
        swordCageImage.color = SkillManager.Instance.swordCageLevel > 0 ? Color.white : Color.grey;
        swordRainImage.color = SkillManager.Instance.swordRainLevel > 0 ? Color.white : Color.grey;

        magicSwordLevelImage.sprite = skillLevels[SkillManager.Instance.magicSwordLevel];
        swordCageLevelImage.sprite = skillLevels[SkillManager.Instance.swordCageLevel];
        swordRainLevelImage.sprite = skillLevels[SkillManager.Instance.swordRainLevel];

        levelText.text = PlayerStats.Instance.GetLevel().ToString();

        skillPoints = PlayerStats.Instance.GetSkillPoints();
        characteristicPoints = PlayerStats.Instance.GetCharacteristicPoints();

        skillPointsText.text = skillPoints.ToString();

        if (skillPoints > 0)
        {
            magicSwordLevelUPButton.gameObject.SetActive(SkillManager.Instance.magicSwordLevel < 3);
            swordCageLevelUPButton.gameObject.SetActive(SkillManager.Instance.swordCageLevel < 3);
            swordRainLevelUPButton.gameObject.SetActive(SkillManager.Instance.swordRainLevel < 3);
        }
        else
        {
            magicSwordLevelUPButton.gameObject.SetActive(false);
            swordCageLevelUPButton.gameObject.SetActive(false);
            swordRainLevelUPButton.gameObject.SetActive(false);
        }
    }
    public void Show()
    {
        gameObject.SetActive(true);

        UpdateUI();
    }
    public void Hide()
    {
        inputUI.Show();

        gameObject.SetActive(false);
    }
}
