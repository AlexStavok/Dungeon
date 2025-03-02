using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;


    public event EventHandler onPlayerAttack;


    private PlayerActions playerActions;

    private void Awake()
    {
        Instance = this;
        playerActions = new PlayerActions();
        playerActions.PlayerActionMap.Enable();
        playerActions.PlayerActionMap.Attack.performed += Attack_performed;
    }

    private void Attack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        onPlayerAttack?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVector()
    {
        return playerActions.PlayerActionMap.Move.ReadValue<Vector2>().normalized;
    }
}
