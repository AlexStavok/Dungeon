using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PotionMenuUI : MonoBehaviour
{
    [SerializeField] private Sprite emptyPotionSprite;
    [SerializeField] private Sprite healPotionSprite;
    [SerializeField] private Sprite manaPotionSprite;

    [SerializeField] private Button healButton;
    [SerializeField] private Button manaButton;

    [SerializeField] private TextMeshProUGUI healPotionAmountText;
    [SerializeField] private TextMeshProUGUI manaPotionAmountText;

    [SerializeField] private int maxHealPotionAmount;
    [SerializeField] private int maxManaPotionAmount;

    [SerializeField] private float healAmount;
    [SerializeField] private float manaAmount;

    private int healPotionAmount;
    private int manaPotionAmount;

    private void Awake()
    {
        healButton.onClick.AddListener(() =>
        {
            if (healPotionAmount <= 0)
                return;

            StandartHealPotion standartHealPotion = new StandartHealPotion(healAmount);
            standartHealPotion.ApplyEffect();

            healPotionAmount -= 1;
            UpdatePotionUI();
        });
        manaButton.onClick.AddListener(() =>
        {
            if (manaPotionAmount <= 0)
                return;

            StandartManaPotion standartManaPotion = new StandartManaPotion(manaAmount);
            standartManaPotion.ApplyEffect();

            manaPotionAmount -= 1;
            UpdatePotionUI();
        });
    }

    private void Start()
    {
        healPotionAmount = maxHealPotionAmount;
        manaPotionAmount = maxManaPotionAmount;
        UpdatePotionUI();
    }

    private void UpdatePotionUI()
    {
        healPotionAmountText.text = healPotionAmount.ToString();
        manaPotionAmountText.text = manaPotionAmount.ToString();

        healButton.image.sprite = healPotionAmount > 0 ? healPotionSprite : emptyPotionSprite;
        manaButton.image.sprite = manaPotionAmount > 0 ? manaPotionSprite : emptyPotionSprite;
    }
}