using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private PlayerActions playerActions;

    private void Awake()
    {
        Instance = this;
        playerActions = new PlayerActions();
        playerActions.Movement.Enable();
    }
    public Vector2 GetMovementVector()
    {
        return playerActions.Movement.Move.ReadValue<Vector2>().normalized;
    }
}
