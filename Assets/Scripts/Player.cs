using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float strength = 0f;
    [SerializeField] private float agility = 0f;
    [SerializeField] private float intelligence = 0f;

    [Header("Ñharacteristics")]
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
    [SerializeField] private ClassStatsScaling classStatsScaling;
    [SerializeField] private Rigidbody2D rb;

    private void Start()
    {
        CalculateAllStats();
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

    public void SetClassStatsScaling(ClassStatsScaling newClassStatsScaling)
    {
        classStatsScaling = newClassStatsScaling;
    }

    public void CalculateAllStats()
    {
        CalculateAttributes();
        CalculateÑharacteristics();
        CalculateStats();
    }
    private void CalculateAttributes()
    {
        if(classStatsScaling != null)
        {
            strength = level * classStatsScaling.strength;
            agility = level * classStatsScaling.agility;
            intelligence = level * classStatsScaling.intelligence;
        }
    }
    private void CalculateÑharacteristics()
    {
        if (classStatsScaling != null)
        {
            physicalPower = strength * classStatsScaling.physicalPower;
            resistance = strength * classStatsScaling.resistance;

            endurance = agility * classStatsScaling.endurance;
            quickness = agility * classStatsScaling.quickness;

            magicalPower = intelligence * classStatsScaling.magicalPower;
            wisdom = intelligence * classStatsScaling.wisdom;
        }
    }
    private void CalculateStats()
    {
        if (classStatsScaling != null)
        {
            health += classStatsScaling.startHealth + (physicalPower * classStatsScaling.physicalPowerToHealth + resistance * classStatsScaling.resistanceToHealth);
            mana += classStatsScaling.startMana + (magicalPower * classStatsScaling.magicalPowerToMana + magicalProtection * classStatsScaling.magicalPowerToMana);
            stamina += classStatsScaling.startStamina + (endurance * classStatsScaling.enduranceToStamina + quickness * classStatsScaling.quicknessToStamina);
            moveSpeed += classStatsScaling.startMoveSpeed + endurance * classStatsScaling.enduranceToMoveSpeed;
            attackSpeed += classStatsScaling.startAttackSpeed + quickness * classStatsScaling.quicknessToAttackSpeed;
            physicalDamage += classStatsScaling.startPhysicalDamage + physicalPower * classStatsScaling.physicalPowerToPhysicalDamage;
            magicalDamage += classStatsScaling.startMagicalDamage + magicalPower * classStatsScaling.magicalPowerToMagicalDamage;
            physicalProtection += resistance * classStatsScaling.resistanceToPhysicalProtection;
            magicalProtection += wisdom * classStatsScaling.wisdomToMagicalProtection;
        }
    }
}
