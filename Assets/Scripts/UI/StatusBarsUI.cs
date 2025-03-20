using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusBarsUI : MonoBehaviour
{
    [SerializeField] private Slider healthBarSlider;
    [SerializeField] private Slider manaBarSlider;
    [SerializeField] private Slider staminaBarSlider;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI manaText;

    private void Start()
    {
        Player.Instance.OnHealthChanged += Player_OnHealthChanged;
        Player.Instance.OnManaChanged += Player_OnManaChanged;
        Player.Instance.OnStaminaChanged += Player_OnStaminaChanged;

        Player_OnHealthChanged(Player.Instance, System.EventArgs.Empty);
        Player_OnManaChanged(Player.Instance, System.EventArgs.Empty);
        Player_OnStaminaChanged(Player.Instance, System.EventArgs.Empty);
    }

    private void Player_OnStaminaChanged(object sender, System.EventArgs e)
    {
        staminaBarSlider.maxValue = Player.Instance.maxStamina;
        staminaBarSlider.value = Player.Instance.stamina;
    }

    private void Player_OnManaChanged(object sender, System.EventArgs e)
    {
        manaBarSlider.maxValue = Player.Instance.maxMana;
        manaBarSlider.value = Player.Instance.mana;

        manaText.text = $"{Player.Instance.mana}/{Player.Instance.maxMana}";
    }

    private void Player_OnHealthChanged(object sender, System.EventArgs e)
    {
        healthBarSlider.maxValue = Player.Instance.maxHealth;
        healthBarSlider.value = Player.Instance.health;

        healthText.text = $"{Player.Instance.health}/{Player.Instance.maxHealth}";
    }
}
