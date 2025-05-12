using UnityEngine;

public class HK_PortalSpawner : MonoBehaviour
{
    public GameObject portalPrefab;  // 포탈 프리팹
    public Vector3 spawnPosition;    // 포탈이 생성될 위치 (인스펙터에서 설정)

    private GameObject portalInstance;

    void Start()
    {
        // 포탈 프리팹이 할당되어 있는지 확인
        if (portalPrefab != null)
        {
            // 포탈을 생성하고 비활성화 상태로 설정
            portalInstance = Instantiate(portalPrefab, spawnPosition, Quaternion.identity);
            portalInstance.SetActive(false);  // 처음에는 포탈을 보이지 않게 설정
        }
        else
        {
            Debug.LogError("Portal Prefab is not assigned!");
        }
    }

    public void ActivatePortal()
    {
        // 포탈을 활성화하여 보이게 하기
        if (portalInstance != null)
        {
            portalInstance.SetActive(true);
        }
        else
        {
            Debug.LogError("Portal instance is null. Check if the portalPrefab is instantiated properly.");
        }
    }
}
