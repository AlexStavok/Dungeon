using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionManager : MonoBehaviour
{
    [SerializeField] PotionEffect[] potions;

    private void Start()
    {
        InputManager.Instance.OnUsingFirstPotion += Instance_OnUsingFirstPotion;
        InputManager.Instance.OnUsingSecondPotion += Instance_OnUsingSecondPotion;
        InputManager.Instance.OnUsingThirdPotion += Instance_OnUsingThirdPotion;
    }

    private void Instance_OnUsingFirstPotion(object sender, System.EventArgs e)
    {
        potions[0].ApplyEffect(Player.Instance);
    }

    private void Instance_OnUsingSecondPotion(object sender, System.EventArgs e)
    {
        potions[1].ApplyEffect(Player.Instance);
    }

    private void Instance_OnUsingThirdPotion(object sender, System.EventArgs e)
    {
        potions[2].ApplyEffect(Player.Instance);
    }
    private void OnDestroy()
    {
        InputManager.Instance.OnUsingFirstPotion -= Instance_OnUsingFirstPotion;
        InputManager.Instance.OnUsingSecondPotion -= Instance_OnUsingSecondPotion;
        InputManager.Instance.OnUsingThirdPotion -= Instance_OnUsingThirdPotion;
    }

}
