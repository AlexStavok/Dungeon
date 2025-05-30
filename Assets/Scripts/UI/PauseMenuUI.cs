using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private Button exitButton;
    [SerializeField] private Button saveButton;
    [SerializeField] private Button loadButton;
    [SerializeField] private Button menuButton;

    private void Start()
    {
        exitButton.onClick.AddListener(() =>
        {
            Hide();
        });
        saveButton.onClick.AddListener(() =>
        {
            SaveSystem.Save();
        });
        loadButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });
        menuButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MainMenu");
        });
    }
    public void Show()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Hide()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
