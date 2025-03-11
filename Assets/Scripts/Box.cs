using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour, IDamageAble
{
    private float HP = 100;
    public void TakeDamage(Damage damage)
    {
        HP -= damage.damageAmount;
        Debug.Log(HP);
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }
}