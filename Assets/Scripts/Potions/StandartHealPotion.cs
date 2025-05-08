using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandartHealPotion : PotionEffect
{
    [SerializeField] private float healAmount = 50;
    public override void ApplyEffect(Player player)
    {
        player.AddHeal(healAmount);
    }

}
