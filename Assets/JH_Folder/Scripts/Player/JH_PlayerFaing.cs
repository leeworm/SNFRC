using UnityEngine;

public class JH_PlayerFaing : MonoBehaviour
{
    public Transform opponentTransform; // 마주 볼 상대방. Inspector에서 할당하거나 코드로 찾습니다.
    private JH_Entity entityScript;     // 플레이어 자신의 JH_Entity 스크립트

    // (선택 사항) 방향 전환 민감도 조절용 임계값
    private float facingThreshold = 0.05f;

    void Start()
    {
        entityScript = GetComponentInParent<JH_Entity>();
        if (entityScript == null)
        {
            Debug.LogError("JH_Entity 스크립트를 찾을 수 없습니다!", gameObject);
            enabled = false; // JH_Entity 없이는 이 로직이 의미 없음
            return;
        }

        // opponentTransform이 Inspector에서 할당되지 않았다면,
        // "Enemy" 또는 "Player2" 태그 등으로 상대방을 찾아 할당할 수 있습니다.
        if (opponentTransform == null)
        {
            // 예시: 상대방이 "Enemy" 태그를 가지고 있을 경우
            GameObject opponentObject = GameObject.FindGameObjectWithTag("Enemy");
            if (opponentObject != null)
            {
                opponentTransform = opponentObject.transform;
            }
            
        }
    }

    void Update()
    {
        AutoFaceOpponent();
    }

    void AutoFaceOpponent()
    {
        if (opponentTransform == null || entityScript == null)
            return;

        float direction = opponentTransform.position.x - transform.position.x;

        // 일정 거리 이상 차이가 나야 방향 전환 (노이즈 방지)
        if (Mathf.Abs(direction) > facingThreshold)
        {
            int facing = direction > 0 ? 1 : -1;
            entityScript.SetFacingDirection(facing);
        }
    }
}
