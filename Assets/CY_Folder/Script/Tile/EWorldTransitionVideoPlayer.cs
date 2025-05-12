using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.Collections;

public class EWorldTransitionVideoPlayer : MonoBehaviour
{
    public RawImage rawImage;
    public VideoPlayer videoPlayer;
    public CanvasGroup canvasGroup;
    public float fadeDuration = 1.5f;

    private void Awake()
    {
        rawImage.enabled = false;
        canvasGroup.alpha = 0f;
    }

    public void PlayTransitionVideo()
    {
        StartCoroutine(PlaySequence());
    }

    private IEnumerator PlaySequence()
    {
        rawImage.enabled = true;
        videoPlayer.Play();

        yield return StartCoroutine(FadeIn());

        yield return new WaitForSeconds(4f); // 4초간 유지

        videoPlayer.Stop();
        yield return StartCoroutine(FadeOut());

        rawImage.enabled = false;
    }

    private IEnumerator FadeIn()
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / fadeDuration;
            canvasGroup.alpha = Mathf.Clamp01(t);
            yield return null;
        }
    }

    private IEnumerator FadeOut()
    {
        float t = 1f;
        while (t > 0f)
        {
            t -= Time.deltaTime / fadeDuration;
            canvasGroup.alpha = Mathf.Clamp01(t);
            yield return null;
        }
    }
}
