using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Damage
{
    public enum DamageType
    {
        Physical,
        Magical
    }

    public DamageType damageType;
    public float damageAmount;

    public Damage Clone()
    {
        return new Damage
        {
            damageType = this.damageType,
            damageAmount = this.damageAmount
        };
    }
}