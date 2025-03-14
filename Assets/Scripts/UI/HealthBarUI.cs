using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Slider healthBarSlider;
    [SerializeField] private TextMeshProUGUI healthText;

    private void Start()
    {
        Player.Instance.OnHealthChanged += Player_OnHealthChanged;
    }

    private void Player_OnHealthChanged(object sender, System.EventArgs e)
    {
        healthBarSlider.maxValue = Player.Instance.maxHealth;
        healthBarSlider.value = Player.Instance.health;

        healthText.text = $"{Player.Instance.health}/{Player.Instance.maxHealth}";
    }
}
