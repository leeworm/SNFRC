using UnityEngine;

public class HK_PortalSpawner : MonoBehaviour
{
    public GameObject portalPrefab;  // ��Ż ������
    public Vector3 spawnPosition;    // ��Ż�� ������ ��ġ (�ν����Ϳ��� ����)

    private GameObject portalInstance;

    void Start()
    {
        // ��Ż �������� �Ҵ�Ǿ� �ִ��� Ȯ��
        if (portalPrefab != null)
        {
            // ��Ż�� �����ϰ� ��Ȱ��ȭ ���·� ����
            portalInstance = Instantiate(portalPrefab, spawnPosition, Quaternion.identity);
            portalInstance.SetActive(false);  // ó������ ��Ż�� ������ �ʰ� ����
        }
        else
        {
            Debug.LogError("Portal Prefab is not assigned!");
        }
    }

    public void ActivatePortal()
    {
        // ��Ż�� Ȱ��ȭ�Ͽ� ���̰� �ϱ�
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
