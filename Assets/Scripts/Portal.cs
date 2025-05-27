using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Portal : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform teleportPoint;
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 1f;

    public void Interact()
    {
        StartCoroutine(TeleportWithFade());
    }

    private IEnumerator TeleportWithFade()
    {
        yield return Fade(0f, 1f);

        Player.Instance.gameObject.transform.position = teleportPoint.position;

        yield return new WaitForSeconds(1f);

        yield return Fade(1f, 0f);
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
