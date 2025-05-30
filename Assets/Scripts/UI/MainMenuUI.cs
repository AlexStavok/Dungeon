using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button exitButton;

    private void Start()
    {
        continueButton.gameObject.SetActive(SaveSystem.SaveExists());

        continueButton.onClick.AddListener(() =>
        {
            Loader.Instance.ShouldLoadSave = true;
            SceneManager.LoadScene("GameScene");
        });

        newGameButton.onClick.AddListener(() =>
        {
            Loader.Instance.ShouldLoadSave = false;
            SceneManager.LoadScene("GameScene");
        });

        exitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
