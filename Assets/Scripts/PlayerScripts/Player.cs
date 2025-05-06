using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour, IDamageable
{
    public static Player Instance;


    [Header("Attributes")]
    [SerializeField] private float strength;
    [SerializeField] private float agility;
    [SerializeField] private float intelligence;


    [Header("—haracteristics")]
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
    [SerializeField] private Player—haracteristicsConfigSO ÒharacteristicsConfigSO;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform playerVisual;
    [SerializeField] private Transform handlePoint;
    [SerializeField] private Weapon weapon;
    [SerializeField] private float detectionRadius;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float interactRadius;


    private bool canRotate = true;
    private bool canRun = true;
    private bool isRunning = false;
    private Transform targetEnemy;


    public event EventHandler OnHealthChanged;
    public event EventHandler OnManaChanged;
    public event EventHandler OnStaminaChanged;
    public event EventHandler OnAttackReady;
    public event EventHandler OnInteractReady;

    private void Awake()
    {
        Instance = this;


        SetStartAttributes();
        CalculateStartCharacteristics();
    }

    private void Start()
    {
        InputManager.Instance.OnPlayerStartRun += InputManager_OnPlayerStartRun;
        InputManager.Instance.OnPlayerStopRun += InputManager_OnPlayerStopRun;
        InputManager.Instance.OnPlayerInteract += InputManager_OnPlayerInteract;
    }

    private void InputManager_OnPlayerInteract(object sender, EventArgs e)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactRadius);
        foreach (var collider in colliders)
        {
            if (collider.gameObject == this.gameObject)
                continue;

            if (collider.TryGetComponent<IInteractable>(out var interactable))
            {
                interactable.Interact();
                break;
            }
        }
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

    void Update()
    {
        CheckForInteractableAround();
        DetectClosestEnemy();
        MovementHandler();
        RotatePlayer();
        RotateWeapon();
    }

    private void CheckForInteractableAround()
    {
        OnAttackReady?.Invoke(this, EventArgs.Empty);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactRadius);
        foreach (var collider in colliders)
        {
            if (collider.gameObject == this.gameObject)
                continue;

            if (collider.TryGetComponent<IInteractable>(out var interactable))
            {
                OnInteractReady?.Invoke(this, EventArgs.Empty);
                break;
            }
        }
    }

    private void DetectClosestEnemy()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, detectionRadius, enemyLayer);
        float minDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        if (enemies.Length <= 0)
        {
            targetEnemy = null;
            return;
        }

        foreach (Collider2D enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestEnemy = enemy.transform;
            }
        }

        targetEnemy = closestEnemy;
    }
    private bool HasTarget()
    {
        return targetEnemy != null;
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
        if(HasTarget())
        {
            if(targetEnemy.position.x < gameObject.transform.position.x)
            {
                playerVisual.localScale = new Vector2(-1, playerVisual.localScale.y);
            }
            else
            {
                playerVisual.localScale = new Vector2(1, playerVisual.localScale.y);
            }
        }
        else
        {
            Vector2 direction = rb.velocity.normalized;

            if (direction == Vector2.zero)
                return;

            if (direction.x > 0)
            {
                playerVisual.localScale = new Vector2(1, playerVisual.localScale.y);
            }
            if (direction.x < 0)
            {
                playerVisual.localScale = new Vector2(-1, playerVisual.localScale.y);
            }
        }
    }
    private void RotateWeapon()
    {
        if (!canRotate)
            return;

        Vector2 direction = Vector2.zero;

        if (HasTarget())
        {
            direction = (targetEnemy.position - handlePoint.transform.position).normalized;
        }
        else
        {
            direction = rb.velocity.normalized;
        }

        if (direction == Vector2.zero)
            return;

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
            InputManager.Instance.OnPlayerStartRun -= InputManager_OnPlayerStartRun;
            InputManager.Instance.OnPlayerStopRun -= InputManager_OnPlayerStopRun;
            Destroy(gameObject);
        }
    }

    private void SetStartAttributes()
    {
        if (ÒharacteristicsConfigSO == null)
            return;
        strength = ÒharacteristicsConfigSO.startStrength;
        agility = ÒharacteristicsConfigSO.startAgility;
        intelligence = ÒharacteristicsConfigSO.startIntelligence;

        baseMoveSpeed = ÒharacteristicsConfigSO.moveSpeed;
        currentMoveSpeed = baseMoveSpeed;
        level = 1;
    }
    private void CalculateStartCharacteristics()
    {
        if(ÒharacteristicsConfigSO == null)
            return;
        maxHealth = strength * ÒharacteristicsConfigSO.health;
        effectsResistance = strength * ÒharacteristicsConfigSO.effectsResistance;
        blockingDamage = strength * ÒharacteristicsConfigSO.blockingDamage;

        attackSpeed = agility * ÒharacteristicsConfigSO.attackSpeed;
        armor = agility * ÒharacteristicsConfigSO.armor;
        maxStamina = agility * ÒharacteristicsConfigSO.stamina;

        maxMana = intelligence * ÒharacteristicsConfigSO.mana;
        magicResistance = intelligence * ÒharacteristicsConfigSO.magicResistance;
        magicPower = intelligence * ÒharacteristicsConfigSO.magicPower;

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

        strength += ÒharacteristicsConfigSO.strengthIncrement;
        agility += ÒharacteristicsConfigSO.agilityIncrement;
        intelligence += ÒharacteristicsConfigSO.intelligenceIncrement;

        CalculateStartCharacteristics();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, detectionRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(gameObject.transform.position, interactRadius);
    }
}