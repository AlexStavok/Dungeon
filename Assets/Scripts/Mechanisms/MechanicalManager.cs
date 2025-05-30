using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicalManager : MonoBehaviour
{
    public static MechanicalManager Instance;

    [SerializeField] private GameObject[] switches;

    private IMechanicalSwitch[] mechanicalSwitches;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        mechanicalSwitches = new IMechanicalSwitch[switches.Length];
        for (int i = 0; i < switches.Length; i++)
        {
            mechanicalSwitches[i] = switches[i].GetComponent<IMechanicalSwitch>();
        }
    }
    public void Save(ref MechanismsData data)
    {
        data.mechanisms = new bool[mechanicalSwitches.Length];
        for (int i = 0; i < mechanicalSwitches.Length; i++)
        {
            data.mechanisms[i] = mechanicalSwitches[i].isActive;
        }
    }
    public void Load(MechanismsData data)
    {
        for (int i = 0; i < mechanicalSwitches.Length; i++)
        {
            if (data.mechanisms[i])
            {
                mechanicalSwitches[i].Activate();
            }
        }
    }
}

[System.Serializable]
public struct MechanismsData
{
    public bool[] mechanisms;
}
