using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SlimeBossFirstSkill : MonoBehaviour
{
    [SerializeField] private SlimeBossFirstProjectile slimeBossFirstProjectile;

    [SerializeField] private Vector2[] ProjectilesVectorAttack;

    public void Initialize(Damage damage)
    {
        for(int i = 0; i < ProjectilesVectorAttack.Length; i++)
        {
            SlimeBossFirstProjectile projectile = Instantiate(slimeBossFirstProjectile, gameObject.transform.position, Quaternion.identity);
            projectile.Initialize(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y) + ProjectilesVectorAttack[i], damage);
        }
    }
}
