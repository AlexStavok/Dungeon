using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimeBoss : Monster, IDamageable
{
    [Header("Settings")]

    [SerializeField] private float maxHealth;
    [SerializeField] private float detectionRadius;

    [SerializeField] private Damage damage;
    [SerializeField] private LayerMask attackLayer;
    [SerializeField] private float delayBetweenAttacks;

    [SerializeField] private Transform firstSkillSpawn;
    [SerializeField] private SlimeBossFirstSkill firstSkill;
    [SerializeField] private Transform secondProjectileSpawn;
    [SerializeField] private SlimeBossSecondProjectile secondProjectile;

    [SerializeField] private Transform[] slimeSpawnPositions;
    [SerializeField] private float delayBetweenSlimeSpawns;
    [SerializeField] private SlimeSpawner slimeSpawner;

    [Header("Other")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Animator animator;
    [SerializeField] private GameEndPortal endPortal;


    private const string FIRST_ATTACK_TRIGGER = "FirstAttack";
    private const string SECOND_ATTACK_TRIGGER = "SecondAttack";

    private Transform target;
    private Stages stage = Stages.Idle;
    private bool canAttack = true;
    private float currentHealth;

    private enum Stages
    {
        Idle,
        Attack
    }

    private void Start()
    {
        InvokeRepeating("CheckForTarget", 1, 1);
        InvokeRepeating("SpawnSlime", 1, delayBetweenSlimeSpawns);
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
    public void SpawnSlime()
    {
        if (target == null)
            return;

        int random = Random.Range(0, slimeSpawnPositions.Length);

        Instantiate(slimeSpawner, slimeSpawnPositions[random].position, Quaternion.identity);
    }
    public void SpawnSecondProjectile()
    {
        SlimeBossSecondProjectile slimeBossSecondProjectile = Instantiate(secondProjectile, secondProjectileSpawn.position, Quaternion.identity);
        slimeBossSecondProjectile.Initialize(target.position, damage);
    }
    public void SpawnFirstSkill()
    {
        SlimeBossFirstSkill slimeBossFirstSkill = Instantiate(firstSkill, firstSkillSpawn.position, Quaternion.identity);
        slimeBossFirstSkill.Initialize(damage);
    }
    void Update()
    {
        if (IsStunned())
        {
            IdleHandler();
            return;
        }

        switch (stage)
        {
            case Stages.Idle:
                IdleHandler();
                break;
            case Stages.Attack:
                AttackHandler();
                break;
        }
    }
    private void IdleHandler()
    {
        if(target != null)
        {
            stage = Stages.Attack;
        }
    }

    private void AttackHandler()
    {
        if (!canAttack)
        {
            return;
        }
        int random = Random.Range(0, 2);
        if (random == 0)
        {
            animator.SetTrigger(FIRST_ATTACK_TRIGGER);
        }
        else
        {
            animator.SetTrigger(SECOND_ATTACK_TRIGGER);
        }
        StartCoroutine(ExecuteDelayBetweenAttacks());
    }
    private IEnumerator ExecuteDelayBetweenAttacks()
    {
        canAttack = false;

        yield return new WaitForSeconds(delayBetweenAttacks);

        canAttack = true;
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
    public void TakeDamage(Damage damage)
    {
        currentHealth -= damage.damageAmount;
        UpdateHealtBar();
        DeathHandler();
    }
    private void DeathHandler()
    {
        if (currentHealth <= 0)
        {
            GiveExperience();
            endPortal.gameObject.SetActive(true);
            Destroy(gameObject);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
