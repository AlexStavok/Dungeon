using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicalPlate : MonoBehaviour, IMechanicalSwitch
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isActive)
        {
            Activate();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isActive)
        {
            Deactivate();
        }
    }
}
