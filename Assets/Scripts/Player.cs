using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float strength = 0f;
    [SerializeField] private float agility = 0f;
    [SerializeField] private float intelligence = 0f;

    [Header("—haracteristics")]
    [SerializeField] private float physicalPower = 0f;
    [SerializeField] private float resistance = 0f;

    [Space(5)]
    [SerializeField] private float endurance = 0f;
    [SerializeField] private float quickness = 0f;

    [Space(5)]
    [SerializeField] private float magicalPower = 0f;
    [SerializeField] private float wisdom = 0f;

    [Header("Stats")]
    [SerializeField] private float health = 0f;
    [SerializeField] private float mana = 0f;
    [SerializeField] private float stamina = 0f;
    [SerializeField] private float moveSpeed = 0f;
    [SerializeField] private float attackSpeed = 0f;
    [SerializeField] private float physicalDamage = 0f;
    [SerializeField] private float magicalDamage = 0f;
    [SerializeField] private float physicalProtection = 0f;
    [SerializeField] private float magicalProtection = 0f;

    [Space(5)]
    [SerializeField] private int level = 0;


    [Header("Other")]
    [SerializeField] private AttributeScaling attributeScaling;
    [SerializeField] private CharacteristicsStatScaling characteristicsStatScaling;
    [SerializeField] private Rigidbody2D rb;

    private void Start()
    {
        CalculateAttributes();
        Calculate—haracteristics();
        CalculateStats();
    }
    void Update()
    {
        DeathHandler();
        MovementHandler();
    }
    private void MovementHandler()
    {
        Vector2 vector = InputManager.Instance.GetMovementVector();

        rb.velocity = vector * moveSpeed;
        //gameObject.transform.Translate(vector * movementSpeed * Time.deltaTime);
    }
    private void DeathHandler()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void CalculateAttributes()
    {
        if(attributeScaling != null)
        {
            strength = level * attributeScaling.strength;
            agility = level * attributeScaling.agility;
            intelligence = level * attributeScaling.intelligence;
        }
    }
    private void Calculate—haracteristics()
    {
        if (characteristicsStatScaling != null)
        {
            physicalPower = strength * characteristicsStatScaling.physicalPower;
            resistance = strength * characteristicsStatScaling.resistance;

            endurance = agility * characteristicsStatScaling.endurance;
            quickness = agility * characteristicsStatScaling.quickness;

            magicalPower = intelligence * characteristicsStatScaling.magicalPower;
            wisdom = intelligence * characteristicsStatScaling.wisdom;
        }
    }
    private void CalculateStats()
    {
        if (characteristicsStatScaling != null)
        {
            health += (physicalPower * characteristicsStatScaling.physicalPowerToHealth + resistance * characteristicsStatScaling.resistanceToHealth);
            mana += (magicalPower * characteristicsStatScaling.magicalPowerToMana + magicalProtection * characteristicsStatScaling.magicalPowerToMana);
            stamina += (endurance * characteristicsStatScaling.enduranceToStamina + quickness * characteristicsStatScaling.quicknessToStamina);
            moveSpeed += endurance * characteristicsStatScaling.enduranceToMoveSpeed;
            attackSpeed += quickness * characteristicsStatScaling.quicknessToAttackSpeed;
            physicalDamage += physicalPower * characteristicsStatScaling.physicalPowerToPhysicalDamage;
            magicalDamage += magicalPower * characteristicsStatScaling.magicalPowerToMagicalDamage;
            physicalProtection += resistance * characteristicsStatScaling.resistanceToPhysicalProtection;
            magicalProtection += wisdom * characteristicsStatScaling.wisdomToMagicalProtection;
        }
    }
}
