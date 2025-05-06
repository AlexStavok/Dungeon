using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private BoxCollider2D collier;
    public void OpenDoor()
    {
        animator.SetTrigger("Open");
        collier.enabled = false;
    }

    public void CloseDoor()
    {
        animator.SetTrigger("Close");
        collier.enabled = true;
    }

}
