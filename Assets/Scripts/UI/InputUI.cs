using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InputUI : MonoBehaviour
{
    [Header("MagicSword")]
    [SerializeField] private GameObject magicSwordSkillGameObject;
    [SerializeField] private Button magicSwordSkillButton;
    [SerializeField] private Image magicSwordCooldownImage;
    private bool isActiveMagicSword = true;

    [Header("MagicSword")]
    [SerializeField] private GameObject swordRainSkillGameObject;
    [SerializeField] private Button swordRainSkillButton;
    [SerializeField] private Image swordRainCooldownImage;
    private bool isActiveSwordRain = true;

    [Header("MagicSword")]
    [SerializeField] private GameObject swordCageSkillGameObject;
    [SerializeField] private Button swordCageSkillButton;
    [SerializeField] private Image swordCageCooldownImage;
    private bool isActiveSwordCage = true;


    void Start()
    {
        magicSwordSkillButton.onClick.AddListener(() =>
        {
            if (isActiveMagicSword)
            {
                InputManager.Instance.UsingMagicSwordSkill();

                StartCoroutine(MagicSwordCooldownRoutine(SkillManager.Instance.GetMagicSwordCooldown()));
            }
        });
        swordRainSkillButton.onClick.AddListener(() =>
        {
            if (isActiveSwordRain)
            {
                InputManager.Instance.UsingSwordRainSkill();

                StartCoroutine(SwordRainCooldownRoutine(SkillManager.Instance.GetSwordRainCooldown()));
            }
        });
        swordCageSkillButton.onClick.AddListener(() =>
        {
            if (isActiveSwordCage)
            {
                InputManager.Instance.UsingSwordCageSkill();

                StartCoroutine(SwordCageCooldownRoutine(SkillManager.Instance.GetSwordCageCooldown()));
            }
        });

        Show();
    }

    IEnumerator MagicSwordCooldownRoutine(float cooldownTime)
    {
        isActiveMagicSword = false;
        float elapsed = 0f;
        magicSwordCooldownImage.fillAmount = 1f;

        while (elapsed < cooldownTime)
        {
            elapsed += Time.deltaTime;
            magicSwordCooldownImage.fillAmount = 1f - (elapsed / cooldownTime);
            yield return null;
        }

        magicSwordCooldownImage.fillAmount = 0f;
        isActiveMagicSword = true;
    }
    IEnumerator SwordRainCooldownRoutine(float cooldownTime)
    {
        isActiveSwordRain = false;
        float elapsed = 0f;
        swordRainCooldownImage.fillAmount = 1f;

        while (elapsed < cooldownTime)
        {
            elapsed += Time.deltaTime;
            swordRainCooldownImage.fillAmount = 1f - (elapsed / cooldownTime);
            yield return null;
        }

        swordRainCooldownImage.fillAmount = 0f;
        isActiveSwordRain = true;
    }
    IEnumerator SwordCageCooldownRoutine(float cooldownTime)
    {
        isActiveSwordCage = false;
        float elapsed = 0f;
        swordCageCooldownImage.fillAmount = 1f;

        while (elapsed < cooldownTime)
        {
            elapsed += Time.deltaTime;
            swordCageCooldownImage.fillAmount = 1f - (elapsed / cooldownTime);
            yield return null;
        }

        swordCageCooldownImage.fillAmount = 0f;
        isActiveSwordCage = true;
    }
    public void Show()
    {
        gameObject.SetActive(true);
        magicSwordSkillGameObject.SetActive(SkillManager.Instance.magicSwordLevel > 0);
        swordRainSkillGameObject.SetActive(SkillManager.Instance.swordRainLevel > 0);
        swordCageSkillGameObject.SetActive(SkillManager.Instance.swordCageLevel > 0);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
