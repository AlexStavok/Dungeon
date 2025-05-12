using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour, IDamageable
{
    [Header("Settings")]
    [SerializeField] private float health;

    [SerializeField] private float followRadius;
    [SerializeField] private float detectionRadius;
    [SerializeField] private float attackRadius;
    [SerializeField] private float slimeRadius;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    [SerializeField] private Damage damage;
    [SerializeField] private LayerMask attackLayer;
    [SerializeField] private float delayBetweenAttacks;


    [Header("Other")]
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;


    private const string ATTACK_TRIGGER = "Attack";
    private const string SEE_ENEMY_BOOL = "SeeEnemy";

    private Transform target;
    private Stages stage = Stages.Idle;
    private bool canAttack = true;

    private enum Stages
    {
        Idle,
        Move,
        Attack
    }

    private void Start()
    {
        InvokeRepeating("CheckForTarget", 1, 1);
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
        if(target != null)
        {
            if(Vector2.Distance(transform.position, target.transform.position) <= attackRadius)
            {
                stage = Stages.Attack;
            }
            else if(Vector2.Distance(transform.position, target.transform.position) <= followRadius)
            {
                stage = Stages.Move;
            }
            else if(Vector2.Distance(transform.position, target.transform.position) > followRadius)
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
        animator.SetBool(SEE_ENEMY_BOOL, false);
        RotateSprite();
        if (!canAttack)
        {
            return;
        }
        animator.SetTrigger(ATTACK_TRIGGER);
        StartCoroutine(ExecuteDelayBetweenAttacks());
    }
    public void JumpToTarget()
    {

        RotateSprite();

        Vector2 moveVector = target.position - transform.position;
        rb.velocity = moveVector.normalized * jumpForce;
    }
    public void CauseDamage()
    {
        rb.velocity = Vector3.zero;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, slimeRadius, attackLayer);
        foreach (var collider in colliders)
        {
            if (collider.gameObject == this.gameObject)
                continue;

            if (collider.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(damage);
            }
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
        health -= damage.damageAmount;
        Debug.Log(health);
        if (health <= 0)
        {
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
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, slimeRadius);
    }

    //[Header("Settings")]
    //[SerializeField] private float health;
    //[SerializeField] private float detectionRadius;
    //[SerializeField] private float attackRadius;
    //[SerializeField] private float slimeRadius;
    //[SerializeField] private Damage damage;
    //[SerializeField] private float moveSpeed; 
    //[SerializeField] private float jumpForce;
    //[SerializeField] private LayerMask attackLayer;
    //[SerializeField] private float delayBetweenAttacks;


    //[Header("Other")]
    //[SerializeField] private Animator animator;
    //[SerializeField] private Rigidbody2D rb;


    //private const string AttackTrigger = "Attack";
    //private const string SeeEnemyBool = "SeeEnemy";
    //private Transform target;
    //private Stages stage = Stages.Idle;
    //private bool canAttack = true;

    //private enum Stages
    //{
    //    Idle,
    //    Move,
    //    Attack
    //}

    //private void Start()
    //{
    //    InvokeRepeating("CheckForTarget", 1, 1);
    //}

    //public void CheckForTarget()
    //{
    //    if (target != null)
    //        return;

    //    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, attackLayer);
    //    foreach (var collider in colliders)
    //    {
    //        if (collider.TryGetComponent<Player>(out var player))
    //        {
    //            target = player.transform;
    //            stage = Stages.Move;
    //            animator.SetBool(SeeEnemyBool, true);
    //            break;
    //        }
    //    }
    //}
    //void Update()
    //{
    //    switch (stage)
    //    {
    //        case Stages.Idle:
    //            rb.velocity = Vector3.zero;
    //            if(target != null)
    //            {
    //                stage = Stages.Move;
    //            }
    //            break;
    //        case Stages.Move:
    //            CheckDistanceToTarget();
    //            MoveHandler();
    //            break;
    //        case Stages.Attack:
    //            break;
    //    }
    //}
    //private void MoveHandler()
    //{
    //    if (target != null)
    //    {

    //        if (Vector2.Distance(transform.position, target.position) <= attackRadius)
    //        {
    //            animator.SetBool(SeeEnemyBool, true);
    //        }
    //        Vector2 moveVector = target.position - transform.position;
    //        if (moveVector.x < 0)
    //            transform.localScale = new Vector2(-1, transform.localScale.y);
    //        else
    //            transform.localScale = new Vector2(1, transform.localScale.y);
    //        rb.velocity = moveVector.normalized * moveSpeed;
    //    }
    //}
    //private void CheckDistanceToTarget()
    //{
    //    float distance = Vector2.Distance(transform.position, target.position);
    //    if (distance <= attackRadius)
    //    {
    //        if (canAttack)
    //        {
    //            stage = Stages.Attack;
    //            animator.SetBool(SeeEnemyBool, false);
    //            animator.SetTrigger(AttackTrigger);
    //        }
    //    }
    //    else
    //    {
    //        stage = Stages.Move;
    //        animator.SetBool(SeeEnemyBool, true);
    //    }
    //    if (distance > detectionRadius)
    //    {
    //        target = null;
    //        stage = Stages.Idle;
    //        animator.SetBool(SeeEnemyBool, false);
    //    }
    //}
    //public void JumpToTarget()
    //{
    //    Vector2 moveVector = target.position - transform.position;

    //    // Розворот спрайта
    //    if (moveVector.x < 0)
    //        transform.localScale = new Vector2(-1, transform.localScale.y);
    //    else
    //        transform.localScale = new Vector2(1, transform.localScale.y);

    //    // Рух до гравця
    //    rb.velocity = moveVector.normalized * jumpForce;
    //}

    //public void Attack()
    //{
    //    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, slimeRadius, attackLayer);
    //    foreach (var collider in colliders)
    //    {
    //        if (collider.gameObject == this.gameObject)
    //            continue;

    //        if (collider.TryGetComponent<IDamageable>(out var damageable))
    //        {
    //            damageable.TakeDamage(damage);
    //        }
    //    }
    //    stage = Stages.Idle;
    //    StartCoroutine(ExecuteDelayBetweenAttacks());
    //}

    //private IEnumerator ExecuteDelayBetweenAttacks()
    //{
    //    canAttack = false;

    //    yield return new WaitForSeconds(delayBetweenAttacks);

    //    canAttack = true;
    //}

    //void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(transform.position, detectionRadius);
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, attackRadius);
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawWireSphere(transform.position, slimeRadius);
    //}

    //public void TakeDamage(Damage damage)
    //{
    //    health -= damage.damageAmount;
    //    Debug.Log(health);
    //    if(health <= 0)
    //    {
    //        Debug.Log("Slime is dead");
    //        Destroy(gameObject);
    //    }
    //}
}
