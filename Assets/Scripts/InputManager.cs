using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;


    public event EventHandler OnPlayerAttack;
    public event EventHandler OnPlayerStartRun;
    public event EventHandler OnPlayerStopRun;

    private PlayerActions playerActions;

    private void Awake()
    {
        Instance = this;
        playerActions = new PlayerActions();
        playerActions.PlayerActionMap.Enable();

        playerActions.PlayerActionMap.Attack.performed += Attack_performed;

        playerActions.PlayerActionMap.Run.performed += Run_performed;
        playerActions.PlayerActionMap.Run.canceled += Run_canceled;
    }
    private void Run_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPlayerStartRun?.Invoke(this, EventArgs.Empty);
    }

    private void Run_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPlayerStopRun?.Invoke(this, EventArgs.Empty);
    }

    private void Attack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPlayerAttack?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVector()
    {
        return playerActions.PlayerActionMap.Move.ReadValue<Vector2>().normalized;
    }
}