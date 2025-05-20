using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusBarsUI : MonoBehaviour
{
    [SerializeField] private Slider healthBarSlider;
    [SerializeField] private Slider manaBarSlider;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI manaText;

    private void Start()
    {
        PlayerStats.Instance.OnHealthChanged += PlayerStats_OnHealthChanged;
        PlayerStats.Instance.OnManaChanged += PlayerStats_OnManaChanged;

        PlayerStats_OnHealthChanged(PlayerStats.Instance, System.EventArgs.Empty);
        PlayerStats_OnManaChanged(PlayerStats.Instance, System.EventArgs.Empty);
    }

    private void PlayerStats_OnManaChanged(object sender, System.EventArgs e)
    {
        manaBarSlider.maxValue = PlayerStats.Instance.GetMaxMana();
        manaBarSlider.value = PlayerStats.Instance.GetMana();

        manaText.text = $"{PlayerStats.Instance.GetMaxMana()}/{PlayerStats.Instance.GetMaxMana()}";
    }

    private void PlayerStats_OnHealthChanged(object sender, System.EventArgs e)
    {
        healthBarSlider.maxValue = PlayerStats.Instance.GetMaxHealth();
        healthBarSlider.value = PlayerStats.Instance.GetHealth();

        healthText.text = $"{PlayerStats.Instance.GetMaxHealth()}/{PlayerStats.Instance.GetHealth()}";
    }
    private void OnDestroy()
    {
        PlayerStats.Instance.OnHealthChanged -= PlayerStats_OnHealthChanged;
        PlayerStats.Instance.OnManaChanged -= PlayerStats_OnManaChanged;
    }
}
