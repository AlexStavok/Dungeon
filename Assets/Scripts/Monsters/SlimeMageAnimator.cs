using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMageAnimator : MonoBehaviour
{
    [SerializeField] private SlimeMage slimeMage;
    public void SpawnProjectileWithDelay()
    {
        slimeMage.SpawnProjectileWithDelay();
    }
}
