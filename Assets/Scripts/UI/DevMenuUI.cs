using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DevMenuUI : MonoBehaviour
{
    [SerializeField] private Button levelUpButton;
    private void Awake()
    {
        levelUpButton.onClick.AddListener(() =>
        {
            Player.Instance.LevelUp();
        });
    }
}
