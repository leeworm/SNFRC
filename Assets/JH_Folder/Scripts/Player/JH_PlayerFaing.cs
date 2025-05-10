using UnityEngine;

public class JH_PlayerFaing : MonoBehaviour
{
    public Transform opponentTransform; // 마주 볼 상대방. Inspector에서 할당하거나 코드로 찾습니다.
    private JH_Entity entityScript;     // 플레이어 자신의 JH_Entity 스크립트

    // (선택 사항) 방향 전환 민감도 조절용 임계값
    private float facingThreshold = 0.05f;

    void Start()
    {
        entityScript = GetComponent<JH_Entity>();
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
        // 상대방이 없거나, entityScript가 없거나, 플레이어가 KO 상태이면 실행하지 않음
        if (opponentTransform == null || entityScript == null || entityScript.isKnocked)
        {
            return;
        }

        // 1. 상대방과의 수평 거리 계산
        float distanceToOpponentX = opponentTransform.position.x - transform.position.x;

        // 2. 새로운 바라볼 방향 결정 (기본값은 현재 방향)
        int newFacingDirection = entityScript.facingDir;

        if (distanceToOpponentX > facingThreshold) // 상대방이 오른쪽에 있다면
        {
            newFacingDirection = 1; // 오른쪽 (facingDir = 1)
        }
        else if (distanceToOpponentX < -facingThreshold) // 상대방이 왼쪽에 있다면
        {
            newFacingDirection = -1; // 왼쪽 (facingDir = -1)
        }

        // 3. 실제로 방향을 바꿔야 할 때만 SetFacingDirection 호출
        if (newFacingDirection != entityScript.facingDir)
        {
            entityScript.SetFacingDirection(newFacingDirection);
        }
    }
}
