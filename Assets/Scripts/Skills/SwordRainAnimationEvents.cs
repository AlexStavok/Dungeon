using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordRainAnimationEvents : MonoBehaviour
{
    [SerializeField] private SwordRain swordRain;

    public void GiveDamage()
    {
        swordRain.GiveDamage();
    }

    public void DestroySkill()
    {
        swordRain.DestroySkill();
    }
}