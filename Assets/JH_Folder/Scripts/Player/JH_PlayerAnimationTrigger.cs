using UnityEngine;

public class JH_PlayerAnimationTrigger : MonoBehaviour
{
    public JH_Player mainPlayerScript;

    void Awake()
    {
        // 만약 Inspector에서 할당하지 않았다면 부모에서 찾아봅니다.
        if (mainPlayerScript == null)
        {
            mainPlayerScript = GetComponentInParent<JH_Player>();
        }

        if (mainPlayerScript == null)
        {
            Debug.LogError("PlayerAnimationEventReceiver: 부모에서 JH_Player 스크립트를 찾을 수 없습니다!", this.gameObject);
        }
    }

    // 애니메이션 이벤트에서 호출될 public 함수
    public void AnimationTrigger()
    {
        if (mainPlayerScript != null)
        {
            // 실제 로직은 mainPlayerScript의 함수를 호출하여 처리
            mainPlayerScript.AnimationTrigger();
            // Debug.Log("Animation Event Received by Receiver Script"); // 디버그용
        }
        else
        {
            Debug.LogError("AnimationTrigger 호출되었으나 mainPlayerScript가 없습니다!", this.gameObject);
        }
    }
}
