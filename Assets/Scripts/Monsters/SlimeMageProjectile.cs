using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMageProjectile : MonoBehaviour
{
    [SerializeField] private Vector2 damageCenter;
    [SerializeField] private float damageRadius;
    [SerializeField] private LayerMask attackLayer;

    private Damage projectileDamage;


    public void SetDamage(Damage damage)
    {
        projectileDamage = damage;
    }
    public void DealDamage()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y) + damageCenter, damageRadius, attackLayer);
        foreach (var collider in colliders)
        {
            if (collider.gameObject == this.gameObject)
                continue;

            if (collider.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(projectileDamage);
            }
        }
    }
    public void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y) + damageCenter, damageRadius);
    }
}
