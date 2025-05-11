using UnityEngine;

public class HK_PlayerInventory : MonoBehaviour
{
    public bool hasErrorCode = false;

    public void AcquireErrorCode()
    {
        hasErrorCode = true;
        Debug.Log("ErrorCode 획득! 클리어 조건 충족됨.");

        // 클리어 UI나 다음 이벤트 연동 가능
        // 예: GameManager.Instance.OnPlayerGotErrorCode();
    }
}
