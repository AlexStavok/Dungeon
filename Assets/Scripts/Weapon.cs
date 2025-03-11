using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("WeaponSettings")]
    [SerializeField] private WeaponType weaponType;
    [SerializeField] private Damage damage;


    [Header("Melle")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackPointRange;


    [Header("Range")]
    [SerializeField] private Transform projectTile;


    [Header("Other")]
    [SerializeField] private Animator animator;

    private const string attackTrigger = "attack";

    public enum WeaponType
    {
        Melle,
        Range
    }

    public void StartAttackAnimation()
    {
        animator.SetTrigger(attackTrigger);
    }

    public void Attack()
    {
        switch (weaponType)
        {
            case WeaponType.Melle:
                MelleAttack();
                break;
            case WeaponType.Range:
                RangeAttack();
                break;
        }
    }
    private void MelleAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackPointRange);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.TryGetComponent<IDamageAble>(out var damageable))
            {
                damageable.TakeDamage(damage);
            }
        }
    }
    private void RangeAttack()
    {

    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackPointRange);
    }
}
