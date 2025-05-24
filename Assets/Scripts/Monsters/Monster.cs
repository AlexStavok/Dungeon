using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    [SerializeField] protected float experienceForKill;

    private bool isStunned = false;

    protected void GiveExperience()
    {
        PlayerStats.Instance.GetExperience(experienceForKill);
    }
    public void ActivateStun()
    {
        isStunned = true;
    }
    public void DisableStun()
    {
        isStunned = false;
    }
    public bool IsStunned()
    {
        return isStunned;
    }
}
