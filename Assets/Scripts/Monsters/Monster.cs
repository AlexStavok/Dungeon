using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    private bool isStunned = false;
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
