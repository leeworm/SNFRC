using UnityEngine;

public class HK_PlayerInventory : MonoBehaviour
{
    public bool hasErrorCode = false;

    public void AcquireErrorCode()
    {
        hasErrorCode = true;
        Debug.Log("ErrorCode ȹ��! Ŭ���� ���� ������.");

        // Ŭ���� UI�� ���� �̺�Ʈ ���� ����
        // ��: GameManager.Instance.OnPlayerGotErrorCode();
    }
}
