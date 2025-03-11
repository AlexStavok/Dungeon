using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("WeaponSettings")]
    [SerializeField] private WeaponType weaponType;
    [SerializeField] private Damage damage;
    [SerializeField] private float attackRange;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private Transform projectTile;

    [Header("Other")]
    [SerializeField] private Animator animator;

    private const string attackTrigger = "attack";

    public enum WeaponType
    {
        Melle,
        Range
    }

    public void Attack()
    {
        animator.SetTrigger(attackTrigger);
    }

    public void MelleAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);

        foreach(Collider2D enemy in hitEnemies)
        {
            if (enemy.TryGetComponent<IDamagable>(out var damageable))
            {
                damageable.TakeDamage(damage);
            }
        }
    }
    public void RangeAttack()
    {

    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
