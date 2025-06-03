using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpawner : MonoBehaviour
{
    [SerializeField] private Monster[] monster;
    [SerializeField] private Transform spawnPosition;

    public void SpawnSlime()
    {
        int random = Random.Range(0,monster.Length);
        Instantiate(monster[random], spawnPosition.position, Quaternion.identity);
    }
    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
