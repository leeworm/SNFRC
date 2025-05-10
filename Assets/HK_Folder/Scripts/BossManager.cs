using UnityEngine;

public class BossManager : MonoBehaviour
{
    public GameObject protoMan;
    public GameObject bassPrefab; // Bass를 프리팹으로 설정
    public Transform bassSpawnPoint;

    public GameObject portal;
    public GameObject bassSpawnEffectPrefab; // 등장 이펙트 (선택)

    
    

    private GameObject bassInstance;

    void Start()
    {
        portal.SetActive(false);
    }

    public void OnBossDefeated(string bossName)
    {
        if (bossName == "ProtoMan")
        {
            
            SpawnBass();
        }
        else if (bossName == "Bass")
        {
            
            OpenPortal();
        }
    }

    private void SpawnBass()
    {
        // 등장 이펙트 먼저 생성 (선택 사항)
        if (bassSpawnEffectPrefab != null)
        {
            Instantiate(bassSpawnEffectPrefab, bassSpawnPoint.position, Quaternion.identity);
        }

        // 0.5초 지연 후 등장 (이펙트 시간 조정 가능)
        Invoke(nameof(SpawnBassDelayed), 0.5f);
    }

    private void SpawnBassDelayed()
    {
        bassInstance = Instantiate(bassPrefab, bassSpawnPoint.position, Quaternion.identity);

        // 등장 애니메이션 실행 (Animator 필요)
        Animator anim = bassInstance.GetComponent<Animator>();
        if (anim != null)
        {
            anim.SetTrigger("Appear"); // 등장 애니메이션 트리거
        }
    }

    private void OpenPortal()
    {
        portal.SetActive(true);
    }
}
