using UnityEngine;

public class WorldChangerTrigger : MonoBehaviour
{
    public WorldChanger_W changerW;
    public WorldChanger_F changerF;
    public WorldChanger changer;
    public WorldTransitionVideoPlayer videoPlayerOverlay;


    [Header("배경 변경용")]
    public WorldBackgroundManager backgroundManager;

    [Header("몬스터 교체용")]
    public B_MonsterManager monsterManager;

    private bool hasTriggered = false;

    public GameObject endTriggerB; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered || !other.CompareTag("Player")) return;

        hasTriggered = true;

        // ✅ 영상 재생 후 전환 실행
        if (videoPlayerOverlay != null)
            StartCoroutine(PlayAndThenTransition());
        else
            RunWorldTransition(); // 영상 없으면 즉시 전환
    }

    private System.Collections.IEnumerator PlayAndThenTransition()
    {
        videoPlayerOverlay.PlayTransitionVideo();
        yield return new WaitForSeconds(3f); // 영상 길이만큼 대기
        RunWorldTransition();
    }

    private void RunWorldTransition()
    {
        if (changerW != null)
            changerW.StartWorldTransition();

        if (changerF != null)
            changerF.StartWorldTransition();

        if (changer != null)
            changer.StartWorldTransition();

        if (backgroundManager != null)
            backgroundManager.SwitchToNether();

        if (monsterManager != null)
            monsterManager.ReplaceMonsters();

        if (endTriggerB != null)
        endTriggerB.SetActive(true);

                gameObject.SetActive(false);
}

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        hasTriggered = false;
    }
}
