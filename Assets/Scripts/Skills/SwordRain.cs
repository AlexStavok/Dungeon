using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwordRain : MonoBehaviour
{
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private float manaCost;
    [SerializeField] private Damage damage;
    [SerializeField] private Vector2 damageCenter;
    [SerializeField] private Vector2 damageSize;
    [SerializeField] private LayerMask enemyLayer;


    public void Initialize(Vector2 startPos)
    {
        transform.position = startPos;
    }

    public void GiveDamage() {

        Collider2D[] enemies = Physics2D.OverlapCapsuleAll((Vector2)transform.position + damageCenter, damageSize, CapsuleDirection2D.Horizontal, 0f, enemyLayer);

        foreach (var enemy in enemies)
        {
            if (enemy.TryGetComponent<IDamageable>(out var damagable))
            {
                damagable.TakeDamage(damage);

                if (hitEffect != null)
                    Instantiate(hitEffect, enemy.transform.position, Quaternion.identity);
            }
        }
    }

    public void DestroySkill()
    {
        Destroy(gameObject);
    }

    public float GetManaCost()
    {
        return manaCost;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Vector2 center = (Vector2)transform.position + damageCenter;
        float width = damageSize.x;
        float height = damageSize.y;

        float radius = height / 2f;
        Vector2 rectCenter = center;
        Vector2 rectSize = new Vector2(width - height, height);

        Gizmos.DrawWireCube(rectCenter, rectSize);

        Gizmos.DrawWireSphere(center + Vector2.left * (width / 2f - radius), radius);

        Gizmos.DrawWireSphere(center + Vector2.right * (width / 2f - radius), radius);
    }
}
