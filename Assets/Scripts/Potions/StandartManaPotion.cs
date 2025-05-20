using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandartManaPotion : PotionEffect
{
    [SerializeField] private float manaAmount;

    public StandartManaPotion(float manaAmount)
    {
        this.manaAmount = manaAmount;
    }
    public override void ApplyEffect()
    {
        PlayerStats.Instance.AddMana(manaAmount);
    }

}
