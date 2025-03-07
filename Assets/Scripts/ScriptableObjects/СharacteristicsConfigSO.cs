using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewСharacteristicsConfig", menuName = "Game/Сharacteristics Config")]
public class СharacteristicsConfigSO : ScriptableObject
{
    public float health;
    public float effectsResistance;
    public float blockingDamage;

    public float stamina;
    public float attackSpeed;
    public float armor;
    
    public float mana;
    public float magicResistance;
    public float magicPower;
}
