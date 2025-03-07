using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage
{
    public enum DamageType
    {
        Physical,
        Magical
    }

    public DamageType damageType;
    public float damageAmount;
}