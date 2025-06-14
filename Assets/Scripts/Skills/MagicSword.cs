using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSword : Skill 
{
    [SerializeField] protected Damage damage;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private float flySpeed;
    [SerializeField] private float maxDistance;
    [SerializeField] private Rigidbody2D rb;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool isFlying = false;


    private const string ATTACK_TRIGGER = "Attack";
    private const string DESTROY_TRIGGER = "Destroy";
    public void Initialize(Vector2 targetPos, MagicSwordLevel magicSwordLevel, float magicPower)
    {
        damage = magicSwordLevel.damage.Clone();
        damage.damageAmount *= magicPower;

        flySpeed = magicSwordLevel.flySpeed;
        maxDistance = magicSwordLevel.maxDistance;

        startPosition = gameObject.transform.position;
        targetPosition = targetPos;

        Vector3 direction = (targetPosition - startPosition).normalized;

        float angle = 0;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void StartFlying()
    {
        anim.SetTrigger(ATTACK_TRIGGER);
        isFlying = true;
    }

    private void Update()
    {
        if (!isFlying) 
            return;

        rb.velocity = (targetPosition - startPosition).normalized * flySpeed;

        if (Vector3.Distance(startPosition, transform.position) >= maxDistance)
        {
            anim.SetTrigger(DESTROY_TRIGGER);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamageable>(out var damagable))
        {

            damagable.TakeDamage(damage);

            if (hitEffect != null)
            {
                Vector3 hitPosition = (transform.position + collision.transform.position) / 2f;

                Instantiate(hitEffect, hitPosition, Quaternion.identity);
            }
        }
        else
        {
            anim.SetTrigger(DESTROY_TRIGGER);
        }
    }

    public void DestroySword()
    {
        Destroy(gameObject);
    }
}
