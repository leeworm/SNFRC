using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;

public class VideoOverlayController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public RawImage rawImage;
    public CanvasGroup canvasGroup;
    public float fadeDuration = 1.5f;

    private void Start()
    {
        rawImage.enabled = true;
        canvasGroup.alpha = 0f;
        videoPlayer.Play();
        StartCoroutine(FadeIn());
         StartCoroutine(StopAfterSeconds(4f)); // 4초 뒤 꺼짐
    }

    private IEnumerator FadeIn()
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(timer / fadeDuration);
            yield return null;
        }
    }

    private IEnumerator StopAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        videoPlayer.Stop();                // 영상 중지
        rawImage.enabled = false;         // 화면에서 안보이게
        canvasGroup.alpha = 0f;           // 투명 처리
    }
}
