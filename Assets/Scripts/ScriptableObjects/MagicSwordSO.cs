using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMagicSwordSO", menuName = "ScriptableObjects/Skills/MagicSwordSO")]
public class MagicSwordSO : ScriptableObject
{
    public MagicSword skillPrefab;

    public MagicSwordLevel[] levels;


}
[System.Serializable]
public class MagicSwordLevel
{
    public float manaCost;
    public float cooldown;
    public Damage damage;
    public float flySpeed;
    public float maxDistance;
}