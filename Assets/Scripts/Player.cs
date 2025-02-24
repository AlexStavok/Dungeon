using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int movementSpeed = 5;

    void Update()
    {
        Vector2 vector = InputManager.Instance.GetMovementVector();

        gameObject.transform.Translate(vector * movementSpeed * Time.deltaTime);
    }
}
