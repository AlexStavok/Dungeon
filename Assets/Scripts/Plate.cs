using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Plate : MonoBehaviour, Switch
{
    public UnityEvent OnOn;
    public UnityEvent OnOff;

    [SerializeField] private Animator animator;

    private bool state = false;

    public void On()
    {
        animator.SetTrigger("On");
    }
    public void Off()
    {
        animator.SetTrigger("Off");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        On();
        OnOn.Invoke();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Off();
        OnOff.Invoke();
    }
}
