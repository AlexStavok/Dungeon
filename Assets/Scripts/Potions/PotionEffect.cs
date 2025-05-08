using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PotionEffect : MonoBehaviour
{
    public string effectName;

    public abstract void ApplyEffect(Player player);
}
