using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour, IDamageAble
{
    public static Player Instance;


    [Header("Attributes")]
    [SerializeField] private float strength;
    [SerializeField] private float agility;
    [SerializeField] private float intelligence;


    [Header("Ñharacteristics")]
    [SerializeField] public float maxHealth;
    [SerializeField] public float health;
    [SerializeField] public float maxMana;
    [SerializeField] public float mana;
    [SerializeField] public float maxStamina;
    [SerializeField] public float stamina;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float magicPower;
    [SerializeField] private float blockingDamage;
    [SerializeField] private float armor;
    [SerializeField] private float magicResistance;
    [SerializeField] private float effectsResistance;

    [Space(5)]
    [SerializeField] private float baseMoveSpeed;
    [SerializeField] private float runSpeedMultiplier;
    [SerializeField] private float staminaDrainRate;
    [SerializeField] private float staminaRegenRate;
    private float currentMoveSpeed;


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
    [SerializeField] private Weapon weapon;



    private bool canRotate = true;
    private bool canRun = true;
    private bool isRunning = false;


    public event EventHandler OnHealthChanged;
    public event EventHandler OnManaChanged;
    public event EventHandler OnStaminaChanged;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InputManager.Instance.OnPlayerAttack += InputSystem_OnPlayerAttack;
        InputManager.Instance.OnPlayerStartRun += InputManager_OnPlayerStartRun;
        InputManager.Instance.OnPlayerStopRun += InputManager_OnPlayerStopRun;



        SetStartAttributes();
        CalculateStartCharacteristics();
    }

    private void InputManager_OnPlayerStopRun(object sender, EventArgs e)
    {
        StopRunning();
    }
    private void StopRunning()
    {
        currentMoveSpeed = baseMoveSpeed;
        isRunning = false;

    }

    private void InputManager_OnPlayerStartRun(object sender, EventArgs e)
    {
        if (!canRun)
            return;

        currentMoveSpeed = baseMoveSpeed * runSpeedMultiplier;
        isRunning = true;
    }

    private void InputSystem_OnPlayerAttack(object sender, System.EventArgs e)
    {
        weapon.StartAttackAnimation();
    }

    void Update()
    {
        MovementHandler();
        RotatePlayer();
        RotateWeapon();
    }
    private void MovementHandler()
    {
        Vector2 vector = InputManager.Instance.GetMovementVector();
        StaminaHandler();
        rb.velocity = vector * currentMoveSpeed;
    }

    private void StaminaHandler()
    {
        if (isRunning)
        {
            stamina -= staminaDrainRate * Time.deltaTime;
            stamina = Mathf.Max(stamina, 0);

            if (stamina <= 0)
            {
                StopRunning();
                canRun = false;
            }
        }
        else
        {
            stamina += staminaRegenRate * Time.deltaTime;
            stamina = Mathf.Min(stamina, maxStamina);
            if (stamina >= maxStamina * 0.3f)
                canRun = true;
        }

        OnStaminaChanged?.Invoke(this, EventArgs.Empty);
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
                float finalDamage = Math.Max(0, (damage.damageAmount - blockingDamage) * (1 - (armor / 100)));
                health -= (float)Math.Round(finalDamage, 1);
                health = (float)Math.Round(health, 1);
                break;
            case Damage.DamageType.Magical:
                health -= (float)Math.Round(damage.damageAmount * (1 - magicResistance), 1);
                health = (float)Math.Round(health, 1);
                break;
        }
        DeathHandler();
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
    }
    private void DeathHandler()
    {
        if(health <= 0)
        {
            InputManager.Instance.OnPlayerAttack -= InputSystem_OnPlayerAttack;
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

        baseMoveSpeed = classConfigSO.moveSpeed;
        currentMoveSpeed = baseMoveSpeed;
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

        OnHealthChanged?.Invoke(this, EventArgs.Empty);
        OnManaChanged?.Invoke(this, EventArgs.Empty);
        OnStaminaChanged?.Invoke(this, EventArgs.Empty);
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