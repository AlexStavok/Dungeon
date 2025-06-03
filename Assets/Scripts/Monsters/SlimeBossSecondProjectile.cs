using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SlimeBossSecondProjectile : MonoBehaviour
{
    [SerializeField] private float flySpeed;
    [SerializeField] private float maxDistance;
    [SerializeField] private Rigidbody2D rb;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private Damage damage;
  
    public void Initialize(Vector2 targetPos, Damage damage)
    {
        this.damage = damage;

        startPosition = gameObject.transform.position;
        targetPosition = targetPos;

        Vector3 direction = (targetPosition - startPosition).normalized;

        float angle = 0;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    private void Update()
    {
        rb.velocity = (targetPosition - startPosition).normalized * flySpeed;

        if (Vector3.Distance(startPosition, transform.position) >= maxDistance)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamageable>(out var damagable))
        {
            if(collision.tag == "Player")
            {
                damagable.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
