using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour, IDamageable
{
    public static Player Instance;

    
    [SerializeField] private Transform playerVisual;
    [SerializeField] private float interactRadius;


    public event EventHandler OnAttackReady;
    public event EventHandler OnInteractReady;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        InputManager.Instance.OnPlayerInteract += InputManager_OnPlayerInteract;
    }
    void Update()
    {
        CheckForInteractableAround();
    }

    private void InputManager_OnPlayerInteract(object sender, EventArgs e)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactRadius);
        foreach (var collider in colliders)
        {
            if (collider.gameObject == this.gameObject)
                continue;

            if (collider.TryGetComponent<IInteractable>(out var interactable))
            {
                interactable.Interact();
                break;
            }
        }
    }

    public Transform GetPlayerVisual()
    {
        return playerVisual;
    }

    private void CheckForInteractableAround()
    {
        OnAttackReady?.Invoke(this, EventArgs.Empty);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactRadius);
        foreach (var collider in colliders)
        {
            if (collider.gameObject == this.gameObject)
                continue;

            if (collider.TryGetComponent<IInteractable>(out var interactable))
            {
                OnInteractReady?.Invoke(this, EventArgs.Empty);
                break;
            }
        }
    }

    public void TakeDamage(Damage damage)
    {
        PlayerStats.Instance.TakeDamage(damage);
    }
    public void DeathHandler()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(gameObject.transform.position, interactRadius);
    }
}