using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Level : MonoBehaviour, Switch, IInteractable
{
    public UnityEvent OnOn;

    [SerializeField] private Animator animator;

    public void On()
    {
        animator.SetTrigger("On");
    }
    public void Off()
    {
        
    }
    public void Interact()
    {
        On();
        OnOn.Invoke();
    }

}