using UnityEngine;

public class EWorldChangerTrigger : MonoBehaviour
{
    public EWorldTransitionVideoPlayer videoPlayerOverlay;

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered || !other.CompareTag("Player")) return;


        if (ItemCollector.hasEnderItem)
            {
                // 오버레이 영상 재생 + 맵 전환 등
                StartCoroutine(PlayVideoThenEnd());
            }
            else
            {
                Debug.Log("엔더 아이템이 없어 아직 못 들어감");
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
