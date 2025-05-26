using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [SerializeField] protected float manaCost;
    [SerializeField] protected float cooldown;

    public void Initialize(Vector2 targetPos)
    {

    }

}