using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelsManager : MonoBehaviour
{
    public static BarrelsManager Instance;

    public Transform[] barrelsPositions;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void Save(ref BarrelsData data)
    {
        data.barrelsPositions = new Vector3[barrelsPositions.Length];
        for(int i = 0; i < barrelsPositions.Length; i++)
        {
            data.barrelsPositions[i] = barrelsPositions[i].position;
        }
    }
    public void Load(BarrelsData data)
    {
        for (int i = 0; i < barrelsPositions.Length; i++)
        {
            barrelsPositions[i].position = data.barrelsPositions[i];
        }
    }
}
[System.Serializable]
public struct BarrelsData
{
    public Vector3[] barrelsPositions;
}
