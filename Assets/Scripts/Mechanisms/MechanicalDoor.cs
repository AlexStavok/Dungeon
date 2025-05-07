using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MechanicalDoor : MonoBehaviour
{

    [SerializeField] private GameObject[] triggers;
    [SerializeField] private Animator animator;
    [SerializeField] private Collider2D col;

    private IMechanicalSwitch[] mechanicalSwitches;

    private bool isOpened = false;
    private void Awake()
    {
        mechanicalSwitches = new IMechanicalSwitch[triggers.Length];
        for (int i = 0; i < triggers.Length; i++)
        {
            mechanicalSwitches[i] = triggers[i].GetComponent<IMechanicalSwitch>();
        }
    }
    public void Input(bool input)
    {
        if (!input)
        {
            CloseDoor();
            return;
        }

        bool isAllActive = true;
        foreach (var mechanicalSwitch in mechanicalSwitches)
        {
            if (!mechanicalSwitch.isActive)
            {
                isAllActive = false;
                CloseDoor();
                break;
            }
        }
        if (isAllActive)
        {
            OpenDoor();
        }
    }
    private void OpenDoor()
    {
        if (!isOpened)
        {
            col.enabled = false;
            animator.SetTrigger("On");
            isOpened = true;
        }
    }
    private void CloseDoor()
    {
        if (isOpened)
        {
            col.enabled = true;
            animator.SetTrigger("Off");
            isOpened = false;
        }
    }
}
