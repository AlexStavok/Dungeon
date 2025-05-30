using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Сheckpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SaveSystem.Save();
        }
    }
}
