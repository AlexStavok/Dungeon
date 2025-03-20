using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAnimator : MonoBehaviour
{
    [SerializeField] private Slime slime;

    public void Attack()
    {
        slime.Attack();
    }
    public void JumoToTarget()
    {
        slime.JumpToTarget();
    }
}
