using UnityEngine;
using UnityEngine.SceneManagement;

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

        
        
        Debug.Log("엔딩입니다. 여기서 바꾸셔야 합니다");
        SceneManager.LoadScene("MiddleScene");
        // 여기서 씬이 바뀌어야 합니다.
    }
}
