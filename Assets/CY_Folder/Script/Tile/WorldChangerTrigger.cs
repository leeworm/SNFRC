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
    public MonsterManager monsterManager;

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered || !other.CompareTag("Player")) return;

        hasTriggered = true;

        if (videoPlayerOverlay != null)
        videoPlayerOverlay.PlayTransitionVideo();

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
    }
}
