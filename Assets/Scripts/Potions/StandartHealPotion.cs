using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandartHealPotion : PotionEffect
{
    [SerializeField] private float healAmount;

    public StandartHealPotion(float healAmount)
    {
        this.healAmount = healAmount;
    }
    public override void ApplyEffect()
    {
        PlayerStats.Instance.AddHeal(healAmount);
    }

}
