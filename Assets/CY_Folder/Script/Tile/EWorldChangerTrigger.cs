using UnityEngine;

public class EWorldChangerTrigger : MonoBehaviour
{
    public EWorldTransitionVideoPlayer videoPlayerOverlay;

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered || !other.CompareTag("Player")) return;

        hasTriggered = true;

        if (videoPlayerOverlay != null)
        {
            StartCoroutine(PlayVideoThenEnd());
        }
    }

    private System.Collections.IEnumerator PlayVideoThenEnd()
    {
        videoPlayerOverlay.PlayTransitionVideo();
        yield return new WaitForSeconds(4f); // 영상 길이

        // 예: 엔딩 처리
        Debug.Log("✅ 엔딩 영상 재생 완료. 게임 종료 처리 등 추가");
        // SceneManager.LoadScene("EndingScene"); 도 가능
    }
}
