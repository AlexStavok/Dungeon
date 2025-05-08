using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandartManaPotion : PotionEffect
{
    [SerializeField] private float manaAmount = 50;
    public override void ApplyEffect(Player player)
    {
        player.AddMana(manaAmount);
    }

}
