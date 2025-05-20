using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public static PlayerCombat Instance;

    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float detectionRadius;
    [SerializeField] private Transform handlePoint;

    private Transform targetEnemy = null;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    private void Update()
    {
        DetectClosestEnemy();
        RotateWeapon();
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

    private void RotateWeapon()
    {

        Vector2 direction = Vector2.zero;

        if (HasTarget())
        {
            direction = (targetEnemy.position - handlePoint.transform.position).normalized;
        }
        else
        {
            direction = PlayerMovement.Instance.GetLookDirection();
        }

        if (direction == Vector2.zero)
            return;

        float angle = 0;
        if (Player.Instance.GetPlayerVisual().localScale.x == 1)
        {
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        }
        if (Player.Instance.GetPlayerVisual().localScale.x == -1)
        {
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 180;
        }
        handlePoint.rotation = Quaternion.Euler(0, 0, angle);
    }
    public bool HasTarget()
    {
        return targetEnemy != null;
    }

    public Transform GetTargetEnemy()
    {
        return targetEnemy;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, detectionRadius);
    }
}
