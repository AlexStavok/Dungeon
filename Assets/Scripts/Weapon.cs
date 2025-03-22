using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("WeaponSettings")]
    [SerializeField] private WeaponType weaponType;
    [SerializeField] private Damage damage;
    [SerializeField] private LayerMask attackLayer;


    [Header("Melle")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackPointRange;


    [Header("Range")]
    [SerializeField] private Transform projectTile;


    [Header("Other")]
    [SerializeField] private Animator animator;


    private const string attackTrigger = "attack";
    private bool isAttacking = false;

    public enum WeaponType
    {
        Melle,
        Range
    }

    private void Start()
    {
        InputManager.Instance.OnPlayerAttack += InputManager_OnPlayerAttack;
    }

    private void InputManager_OnPlayerAttack(object sender, System.EventArgs e)
    {
        if(!isAttacking)
        {
            StartCoroutine(AttackRoutine());
        }
    }
    private IEnumerator AttackRoutine()
    {
        isAttacking = true; // Блокуємо повторний удар
        animator.SetTrigger(attackTrigger);

        // Чекаємо поки анімація завершиться
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        isAttacking = false; // Дозволяємо атакувати знову
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
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackPointRange, attackLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.TryGetComponent<IDamageable>(out var damageable))
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
    private void OnDestroy()
    {
        InputManager.Instance.OnPlayerAttack -= InputManager_OnPlayerAttack;
    }
}
