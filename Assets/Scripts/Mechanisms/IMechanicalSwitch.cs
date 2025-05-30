using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMechanicalSwitch
{
    public bool isActive { get; }

    public bool IsActive();

    public void Activate();
}
