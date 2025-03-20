using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour, IDamageAble
{
    [Header("Settings")]
    [SerializeField] private float health;
    [SerializeField] private float detectionRadius;
    [SerializeField] private float attackRadius;
    [SerializeField] private float slimeRadius;
    [SerializeField] private Damage damage;
    [SerializeField] private float moveSpeed; 
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask attackLayer;
    [SerializeField] private float delayBetweenAttacks;


    [Header("Other")]
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;



    private const string AttackTrigger = "Attack";
    private const string SeeEnemyBool = "SeeEnemy";
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
                stage = Stages.Move;
                animator.SetBool(SeeEnemyBool, true);
                break;
            }
        }
    }
    void Update()
    {
        switch (stage)
        {
            case Stages.Idle:
                rb.velocity = Vector3.zero;
                if(target != null)
                {
                    stage = Stages.Move;
                }
                break;
            case Stages.Move:
                CheckDistanceToTarget();
                MoveHandler();
                break;
            case Stages.Attack:
                break;
        }
    }
    private void MoveHandler()
    {
        if (target != null)
        {
            Vector2 moveVector = target.position - transform.position;
            if (moveVector.x < 0)
                transform.localScale = new Vector2(-1, transform.localScale.y);
            else
                transform.localScale = new Vector2(1, transform.localScale.y);
            rb.velocity = moveVector.normalized * moveSpeed;
        }
    }
    private void CheckDistanceToTarget()
    {
        float distance = Vector2.Distance(transform.position, target.position);
        if (distance <= attackRadius)
        {
            if (canAttack)
            {
                stage = Stages.Attack;
                animator.SetBool(SeeEnemyBool, false);
                animator.SetTrigger(AttackTrigger);
            }
        }
        else
        {
            stage = Stages.Move;
            animator.SetBool(SeeEnemyBool, true);
        }
        if (distance > detectionRadius)
        {
            target = null;
            stage = Stages.Idle;
            animator.SetBool(SeeEnemyBool, false);
        }
    }
    private void JumpToTarget()
    {
        Vector2 moveVector = target.position - transform.position;

        // Розворот спрайта
        if (moveVector.x < 0)
            transform.localScale = new Vector2(-1, transform.localScale.y);
        else
            transform.localScale = new Vector2(1, transform.localScale.y);

        // Рух до гравця
        rb.velocity = moveVector.normalized * jumpForce;
    }

    public void Attack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, slimeRadius, attackLayer);
        foreach (var collider in colliders)
        {
            if (collider.gameObject == this.gameObject)
                continue;

            if (collider.TryGetComponent<IDamageAble>(out var damageable))
            {
                damageable.TakeDamage(damage);
            }
        }
        stage = Stages.Idle;
        StartCoroutine(ExecuteDelayBetweenAttacks());
    }

    private IEnumerator ExecuteDelayBetweenAttacks()
    {
        canAttack = false;

        yield return new WaitForSeconds(delayBetweenAttacks);

        canAttack = true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, slimeRadius);
    }

    public void TakeDamage(Damage damage)
    {
        health -= damage.damageAmount;
        Debug.Log(health);
        if(health <= 0)
        {
            Debug.Log("Slime is dead");
            Destroy(gameObject);
        }
    }
}
