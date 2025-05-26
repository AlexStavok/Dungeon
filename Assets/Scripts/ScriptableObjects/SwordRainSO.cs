using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewSwordRainSO", menuName = "ScriptableObjects/Skills/SwordRainSO")]
public class SwordRainSO : ScriptableObject
{
    public SwordRain skillPrefab;

    public SwordRainLevel[] levels;

}

[System.Serializable]
public class SwordRainLevel
{
    public float manaCost;
    public float cooldown;
    public Damage damage;
}
