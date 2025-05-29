using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimeMage : Monster, IDamageable
{

    [Header("Settings")]
    [SerializeField] private float maxHealth;

    [SerializeField] private float followRadius;
    [SerializeField] private float detectionRadius;
    [SerializeField] private float attackRadius;

    [SerializeField] private float moveSpeed;

    [SerializeField] private Damage damage;
    [SerializeField] private LayerMask attackLayer;
    [SerializeField] private float delayBetweenAttacks;
    [SerializeField] private float delayBeforeProjectileSpawn;


    [Header("Other")]
    [SerializeField] private SlimeMageProjectile projectile;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;


    private const string ATTACK_TRIGGER = "Attack";
    private const string SEE_ENEMY_BOOL = "SeeEnemy";

    private Transform target;
    private Stages stage = Stages.Idle;
    private bool canAttack = true;
    private float currentHealth;

    private enum Stages
    {
        Idle,
        Move,
        Attack
    }

    private void Start()
    {
        InvokeRepeating("CheckForTarget", 1, 1);
        currentHealth = maxHealth;
        UpdateHealtBar();
    }

    public void CheckForTarget()
    {
        if (target != null)
            return;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, attackLayer);
        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent<Player>(out var player))
            {
                target = player.transform;
                break;
            }
        }
    }

    void Update()
    {
        if (IsStunned())
        {
            IdleHandler();
            return;
        }

        CheckDistanceToTarget();
        switch (stage)
        {
            case Stages.Idle:
                IdleHandler();
                break;
            case Stages.Move:
                MoveHandler();
                break;
            case Stages.Attack:
                AttackHandler();
                break;
        }
    }

    private void CheckDistanceToTarget()
    {
        if (target != null)
        {
            if (Vector2.Distance(transform.position, target.transform.position) <= attackRadius)
            {
                stage = Stages.Attack;
            }
            else if (Vector2.Distance(transform.position, target.transform.position) <= followRadius)
            {
                stage = Stages.Move;
            }
            else if (Vector2.Distance(transform.position, target.transform.position) > followRadius)
            {
                target = null;
                stage = Stages.Idle;
            }
        }
        else
        {
            stage = Stages.Idle;
        }
    }

    private void IdleHandler()
    {
        animator.SetBool(SEE_ENEMY_BOOL, false);
        rb.velocity = Vector3.zero;
    }

    private void MoveHandler()
    {
        RotateSprite();

        animator.SetBool(SEE_ENEMY_BOOL, true);
        Vector2 moveVector = target.position - transform.position;
        rb.velocity = moveVector.normalized * moveSpeed;
    }
    private void AttackHandler()
    {
        rb.velocity = Vector3.zero;
        animator.SetBool(SEE_ENEMY_BOOL, false);
        RotateSprite();
        if (!canAttack)
        {
            return;
        }
        animator.SetTrigger(ATTACK_TRIGGER);
        StartCoroutine(ExecuteDelayBetweenAttacks());
    }
    public void SpawnProjectileWithDelay()
    {
        rb.velocity = Vector3.zero;
        StartCoroutine(SpawnProjectileCoroutine());
    }
    private IEnumerator SpawnProjectileCoroutine()
    {
        yield return new WaitForSeconds(delayBeforeProjectileSpawn);

        if (target != null)
        {
            Vector3 spawnPosition =  new Vector3(target.position.x, target.position.y- 0.5f, target.position.z);
            SlimeMageProjectile spawnedProjectile = Instantiate(projectile, spawnPosition, Quaternion.identity);
            spawnedProjectile.SetDamage(damage);
        }
    }

    private IEnumerator ExecuteDelayBetweenAttacks()
    {
        canAttack = false;

        yield return new WaitForSeconds(delayBetweenAttacks);

        canAttack = true;
    }
    private void RotateSprite()
    {
        Vector2 moveVector = target.position - transform.position;

        if (moveVector.x < 0)
            transform.localScale = new Vector2(-1, transform.localScale.y);
        else
            transform.localScale = new Vector2(1, transform.localScale.y);
    }

    public void TakeDamage(Damage damage)
    {
        currentHealth -= damage.damageAmount;
        UpdateHealtBar();
        Debug.Log(currentHealth);
        DeathHandler();
    }

    private void DeathHandler()
    {
        if (currentHealth <= 0)
        {
            GiveExperience();
            Debug.Log("Slime is dead");
            Destroy(gameObject);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, followRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    private void UpdateHealtBar()
    {
        if (currentHealth == maxHealth)
        {
            healthSlider.gameObject.SetActive(false);
        }
        else
        {
            healthSlider.gameObject.SetActive(true);
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }
}
