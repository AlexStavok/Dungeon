using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;


    public event EventHandler OnPlayerAttack;

    public event EventHandler OnPlayerInteract;

    public event EventHandler OnUsingMagicSwordSkill;
    public event EventHandler OnUsingSwordRainSkill;
    public event EventHandler OnUsingSwordCageSkill;

    private PlayerActions playerActions;

    private void Awake()
    {
        Instance = this;
        playerActions = new PlayerActions();
        playerActions.PlayerActionMap.Enable();

        playerActions.PlayerActionMap.Attack.performed += Attack_performed;

        playerActions.PlayerActionMap.Interact.performed += Interact_performed;
    }


    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPlayerInteract?.Invoke(this, EventArgs.Empty);
    }

    private void Attack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPlayerAttack?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVector()
    {
        return playerActions.PlayerActionMap.Move.ReadValue<Vector2>().normalized;
    }

    public void UsingMagicSwordSkill()
    {
        OnUsingMagicSwordSkill?.Invoke(this, EventArgs.Empty);
    }

    public void UsingSwordRainSkill()
    {
        OnUsingSwordRainSkill?.Invoke(this, EventArgs.Empty);
    }
    public void UsingSwordCageSkill()
    {
        OnUsingSwordCageSkill?.Invoke(this, EventArgs.Empty);
    }
}