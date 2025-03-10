using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;


    [Header("Attributes")]
    [SerializeField] private float strength;
    [SerializeField] private float agility;
    [SerializeField] private float intelligence;


    [Header("Ñharacteristics")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float health;
    [SerializeField] private float maxMana;
    [SerializeField] private float mana;
    [SerializeField] private float maxStamina;
    [SerializeField] private float stamina;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float magicPower;
    [SerializeField] private float blockingDamage;
    [SerializeField] private float armor;
    [SerializeField] private float magicResistance;
    [SerializeField] private float effectsResistance;

    [Space(5)]
    [SerializeField] private float moveSpeed;


    [Header("Level")]
    [SerializeField] private int level;
    [SerializeField] private int characteristicPoints;
    [SerializeField] private int skillPoints;


    [Header("Other")]
    [SerializeField] private ClassConfigSO classConfigSO;
    [SerializeField] private ÑharacteristicsConfigSO ñharacteristicsConfigSO;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform playerVisual;
    [SerializeField] private Transform handlePoint;
    [SerializeField] private Animator weaponAnimator;



    private bool canRotate = true;


    private void Start()
    {
        Instance = this;

        InputManager.Instance.onPlayerAttack += InputSystem_onPlayerAttack;

        SetStartAttributes();
        CalculateStartCharacteristics();
    }

    private void InputSystem_onPlayerAttack(object sender, System.EventArgs e)
    {
        weaponAnimator.SetTrigger("attack");
    }

    void Update()
    {
        MovementHandler();
    }
    private void MovementHandler()
    {
        Vector2 vector = InputManager.Instance.GetMovementVector();
        RotatePlayer();
        RotateWeapon();
        rb.velocity = vector * moveSpeed;
    }

    private void RotatePlayer()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePosition.x > transform.position.x)
        {
            playerVisual.localScale = new Vector2(1, playerVisual.localScale.y);
        }
        if (mousePosition.x < transform.position.x)
        {
            playerVisual.localScale = new Vector2(-1, playerVisual.localScale.y);
        }
    }
    private void RotateWeapon()
    {
        if (!canRotate)
            return;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Vector3 direction = (mousePosition - handlePoint.transform.position).normalized;
        float angle = 0;
        if (playerVisual.localScale.x == 1)
        {
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        }
        if (playerVisual.localScale.x == -1)
        {
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 180;
        }
        handlePoint.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void TakeDamage(Damage damage)
    {
        switch (damage.damageType)
        {
            case Damage.DamageType.Physical:
                health -= (damage.damageAmount - blockingDamage) * (1 - armor);
                break;
            case Damage.DamageType.Magical:
                health -= damage.damageAmount* (1 - magicResistance);
                break;
        }
        DeathHandler();
    }
    private void DeathHandler()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void SetStartAttributes()
    {
        if (classConfigSO == null)
            return;
        strength = classConfigSO.startStrength;
        agility = classConfigSO.startAgility;
        intelligence = classConfigSO.startIntelligence;

        moveSpeed = classConfigSO.moveSpeed;
        level = 1;
    }
    private void CalculateStartCharacteristics()
    {
        if(ñharacteristicsConfigSO == null)
            return;
        maxHealth = strength * ñharacteristicsConfigSO.health;
        effectsResistance = strength * ñharacteristicsConfigSO.effectsResistance;
        blockingDamage = strength * ñharacteristicsConfigSO.blockingDamage;

        attackSpeed = agility * ñharacteristicsConfigSO.attackSpeed;
        armor = agility * ñharacteristicsConfigSO.armor;
        maxStamina = agility * ñharacteristicsConfigSO.stamina;

        maxMana = intelligence * ñharacteristicsConfigSO.mana;
        magicResistance = intelligence * ñharacteristicsConfigSO.magicResistance;
        magicPower = intelligence * ñharacteristicsConfigSO.magicPower;

        health = maxHealth;
        mana = maxMana;
        stamina = maxStamina;
    }
    public void LevelUp()
    {
        level++;

        strength += classConfigSO.strengthIncrement;
        agility += classConfigSO.agilityIncrement;
        intelligence += classConfigSO.intelligenceIncrement;

        CalculateStartCharacteristics();
    }
}