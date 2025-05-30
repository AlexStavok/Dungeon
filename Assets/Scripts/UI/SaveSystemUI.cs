using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSystemUI : MonoBehaviour
{
    [SerializeField] private Button saveButton;
    [SerializeField] private Button loadButton;
    private void Start()
    {
        saveButton.onClick.AddListener(() =>
        {
            SaveSystem.Save();
        });
        loadButton.onClick.AddListener(() =>
        {
            SaveSystem.Load();
        });
    }
}
