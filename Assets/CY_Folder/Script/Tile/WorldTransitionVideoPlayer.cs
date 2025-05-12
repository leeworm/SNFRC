using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.Collections;

public class WorldTransitionVideoPlayer : MonoBehaviour
{
    public RawImage rawImage;
    public VideoPlayer videoPlayer;
    public CanvasGroup canvasGroup;

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

        yield return new WaitForSeconds(3f); // 4초 보여주기

        videoPlayer.Stop();
        yield return StartCoroutine(FadeOut());

        rawImage.enabled = false;
    }

    private IEnumerator FadeIn()
    {
        float t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime * 2f;
            canvasGroup.alpha = Mathf.Clamp01(t);
            yield return null;
        }
    }

    private IEnumerator FadeOut()
    {
        float t = 1f;
        while (t > 0)
        {
            t -= Time.deltaTime * 2f;
            canvasGroup.alpha = Mathf.Clamp01(t);
            yield return null;
        }
    }
}
