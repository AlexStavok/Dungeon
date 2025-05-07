using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicalLevel : MonoBehaviour, IMechanicalSwitch, IInteractable
{
    [SerializeField] private MechanicalDoor door;
    [SerializeField] private Animator animator;

    public bool isActive { get; private set; } = false;

    private void Activate()
    {
        animator.SetTrigger("Activate");
        isActive = true;
        door.Input(true);
    }
    private void Deactivate()
    {
        animator.SetTrigger("Deactivate");
        isActive = false;
        door.Input(false);
    }

    public void Interact()
    {
        if (!isActive)
        {
            Activate();
        }
        else
        {
            Deactivate();
        }
    }
}
