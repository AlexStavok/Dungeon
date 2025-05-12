using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAnimator : MonoBehaviour
{
    [SerializeField] private Slime slime;

    public void CauseDamage()
    {
        slime.CauseDamage();
    }
    public void JumoToTarget()
    {
        slime.JumpToTarget();
    }
}
