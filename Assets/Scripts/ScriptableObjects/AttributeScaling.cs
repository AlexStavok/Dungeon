using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAttributeScaling", menuName = "Game/Attribute Scaling")]
public class AttributeScaling : ScriptableObject
{
    // How many attributes a player will get for each level

    public float strength = 3f;
    public float agility = 3f;
    public float intelligence = 3f;
}
