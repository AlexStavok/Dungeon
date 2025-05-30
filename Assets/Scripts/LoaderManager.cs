using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoaderManager : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 1f;

    private void Start()
    {
        Time.timeScale = 1f;
        if (Loader.Instance == null)
            return;

        if (Loader.Instance.ShouldLoadSave)
        {
            SaveSystem.Load();
        }
        else
        {
            SaveSystem.Save();
            Loader.Instance.ShouldLoadSave = true;
        }
        StartCoroutine(Fade(1f, 0f));
    }
    private IEnumerator Fade(float from, float to)
    {
        float elapsed = 0f;
        Color color = fadeImage.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(from, to, elapsed / fadeDuration);
            fadeImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        fadeImage.color = new Color(color.r, color.g, color.b, to);
    }
}
