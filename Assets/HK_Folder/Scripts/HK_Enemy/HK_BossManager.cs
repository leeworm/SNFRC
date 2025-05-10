using UnityEngine;

public class HK_BossManager : MonoBehaviour
{
    public GameObject protoMan;
    public GameObject bassPrefab; // Bass�� ���������� ����
    public Transform bassSpawnPoint;

    public GameObject portal;
    public GameObject bassSpawnEffectPrefab; // ���� ����Ʈ (����)

    
    

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
        // ���� ����Ʈ ���� ���� (���� ����)
        if (bassSpawnEffectPrefab != null)
        {
            Instantiate(bassSpawnEffectPrefab, bassSpawnPoint.position, Quaternion.identity);
        }

        // 0.5�� ���� �� ���� (����Ʈ �ð� ���� ����)
        Invoke(nameof(SpawnBassDelayed), 0.5f);
    }

    private void SpawnBassDelayed()
    {
        bassInstance = Instantiate(bassPrefab, bassSpawnPoint.position, Quaternion.identity);

        // ���� �ִϸ��̼� ���� (Animator �ʿ�)
        Animator anim = bassInstance.GetComponent<Animator>();
        if (anim != null)
        {
            anim.SetTrigger("Appear"); // ���� �ִϸ��̼� Ʈ����
        }
    }

    private void OpenPortal()
    {
        portal.SetActive(true);
    }
}
