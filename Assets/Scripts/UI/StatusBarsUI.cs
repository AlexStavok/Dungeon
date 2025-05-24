using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusBarsUI : MonoBehaviour
{
    [Header("Experience")]
    [SerializeField] private Slider experienceBarSlider;
    [SerializeField] private TextMeshProUGUI experienceText;
    [SerializeField] private TextMeshProUGUI experienceLevelText;

    [Header("Health")]
    [SerializeField] private Slider healthBarSlider;
    [SerializeField] private TextMeshProUGUI healthText;

    [Header("Maxa")]
    [SerializeField] private Slider manaBarSlider;
    [SerializeField] private TextMeshProUGUI manaText;

    private void Start()
    {
        PlayerStats.Instance.OnHealthChanged += PlayerStats_OnHealthChanged;
        PlayerStats.Instance.OnManaChanged += PlayerStats_OnManaChanged;
        PlayerStats.Instance.OnExperienceChanged += PlayerStats_OnExperienceChanged;

        PlayerStats_OnHealthChanged(PlayerStats.Instance, System.EventArgs.Empty);
        PlayerStats_OnManaChanged(PlayerStats.Instance, System.EventArgs.Empty);
        PlayerStats_OnExperienceChanged(PlayerStats.Instance, System.EventArgs.Empty);
    }

    private void PlayerStats_OnExperienceChanged(object sender, System.EventArgs e)
    {
        experienceBarSlider.maxValue = PlayerStats.Instance.GetTargetExperience();
        experienceBarSlider.value = PlayerStats.Instance.GetCurrentExperience();

        experienceText.text = $"{PlayerStats.Instance.GetCurrentExperience()}/{PlayerStats.Instance.GetTargetExperience()}";
        experienceLevelText.text = $"{PlayerStats.Instance.GetLevel()}";
    }

    private void PlayerStats_OnManaChanged(object sender, System.EventArgs e)
    {
        manaBarSlider.maxValue = PlayerStats.Instance.GetMaxMana();
        manaBarSlider.value = PlayerStats.Instance.GetMana();

        manaText.text = $"{PlayerStats.Instance.GetMana()}/{PlayerStats.Instance.GetMaxMana()}";
    }

    private void PlayerStats_OnHealthChanged(object sender, System.EventArgs e)
    {
        healthBarSlider.maxValue = PlayerStats.Instance.GetMaxHealth();
        healthBarSlider.value = PlayerStats.Instance.GetHealth();

        healthText.text = $"{PlayerStats.Instance.GetHealth()}/{PlayerStats.Instance.GetMaxHealth()}";
    }
    private void OnDestroy()
    {
        PlayerStats.Instance.OnHealthChanged -= PlayerStats_OnHealthChanged;
        PlayerStats.Instance.OnManaChanged -= PlayerStats_OnManaChanged;
    }
}
