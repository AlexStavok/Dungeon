using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSwordCageSO", menuName = "ScriptableObjects/Skills/SwordCageSO")]
public class SwordCageSO : ScriptableObject
{
    public SwordCage skillPrefab;

    public SwordCageLevel[] levels;


}

[System.Serializable]
public class SwordCageLevel
{
    public float manaCost;
    public float cooldown;
    public float stunTime;
}
