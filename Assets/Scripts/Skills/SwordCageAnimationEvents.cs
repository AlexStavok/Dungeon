using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCageAnimationEvents : MonoBehaviour
{
    [SerializeField] private SwordCage swordCage;
    public void Stun()
    {
        swordCage.Stun();
    }
    public void DestroySkill()
    {
        swordCage.DestroySkill();
    }
}
