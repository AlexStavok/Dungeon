using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCage : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private float manaCost;
    [SerializeField] private float stunTime;
    [SerializeField] private Vector2 skillCenter;
    [SerializeField] private Vector2 skillSize;
    [SerializeField] private LayerMask enemyLayer;

    private const string DISAPPEAR_TRIGGER = "Dissapear";
    public void Initialize(Vector2 startPos)
    {
        transform.position = startPos;
    }

    public void Stun()
    {
        Collider2D[] enemies = Physics2D.OverlapCapsuleAll((Vector2)transform.position + skillCenter, skillSize, CapsuleDirection2D.Horizontal, 0f, enemyLayer);

        List<Monster> stunnedMonsters = new List<Monster>();

        foreach (var enemy in enemies)
        {
            if (enemy.TryGetComponent<Monster>(out var monster))
            {
                stunnedMonsters.Add(monster);
                monster.ActivateStun();
            }
        }

        StartCoroutine(DisableStunAfterDelay(stunnedMonsters));
    }

    private IEnumerator DisableStunAfterDelay(List<Monster> monsters)
    {
        yield return new WaitForSeconds(stunTime);

        foreach (var monster in monsters)
        {
            if (monster != null)
            {
                monster.DisableStun();
            }
        }
        anim.SetTrigger(DISAPPEAR_TRIGGER);
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

        Vector2 center = (Vector2)transform.position + skillCenter;
        float width = skillSize.x;
        float height = skillSize.y;

        float radius = height / 2f;
        Vector2 rectCenter = center;
        Vector2 rectSize = new Vector2(width - height, height);

        Gizmos.DrawWireCube(rectCenter, rectSize);

        Gizmos.DrawWireSphere(center + Vector2.left * (width / 2f - radius), radius);

        Gizmos.DrawWireSphere(center + Vector2.right * (width / 2f - radius), radius);
    }
}
