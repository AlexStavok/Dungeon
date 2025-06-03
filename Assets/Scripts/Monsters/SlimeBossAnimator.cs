using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBossAnimator : MonoBehaviour
{
    [SerializeField] private SlimeBoss slimeBoss;
    public void SpawnSecondProjectile()
    {
        slimeBoss.SpawnSecondProjectile();
    }
    public void SpawnFirstSkill()
    {
        slimeBoss.SpawnFirstSkill();
    }
}
