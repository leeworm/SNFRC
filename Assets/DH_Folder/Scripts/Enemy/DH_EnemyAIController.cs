using UnityEngine;

public class DH_EnemyAIController : MonoBehaviour
{
    private DH_Enemy enemy;
    private Transform player;

    [Header("AI info")]
    public float decisionInterval = 0.3f;
    public float visionRange = 10f;
    public float attackRange = 2.5f;
    public float dashRange = 5f;
    public float jumpHeightThreshold = 1f;

    private float lastDecisionTime;

    [Header("Jump Pattern")]
    public float jumpCheckInterval = 1.5f;
    public float jumpChance = 0.2f; // 0.0 ~ 1.0 중 확률
    private float lastJumpCheckTime = -999f;


    private void Awake()
    {
        enemy = GetComponent<DH_Enemy>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    private void Update()
    {
        if (player == null || enemy == null || enemy.isDead)
            return;

        if (Time.time - lastDecisionTime < decisionInterval)
            return;

        lastDecisionTime = Time.time;

        Vector2 dirToPlayer = player.position - enemy.transform.position;
        float distance = dirToPlayer.magnitude;
        float yDiff = player.position.y - enemy.transform.position.y;

        // 탐색 범위 밖이면 아무 행동도 하지 않음
        if (distance > visionRange)
        {
            enemy.inputX = 0;
            return;
        }
        // 탐색 범위 안일 때만 방향 판단
        if (distance <= visionRange)
        {
            float faceDir = Mathf.Sign(dirToPlayer.x);
            enemy.inputX = faceDir;

            // 👉 자동으로 바라보게 하기 (Flip)
            if ((faceDir > 0 && !enemy.facingRight) || (faceDir < 0 && enemy.facingRight))
                enemy.FlipController(faceDir);
        }
        else
        {
            enemy.inputX = 0;
        }

        // 탐색 범위 내일 때만 입력 처리 시작
        enemy.inputX = Mathf.Sign(dirToPlayer.x);

        // 어퍼컷
        if (distance <= 1.2f && yDiff > jumpHeightThreshold)
        {
            enemy.isUpInput = true;
            enemy.isAttackInput = true;
            return;
        }

        // 점프 공격
        if (distance <= 2f && yDiff > jumpHeightThreshold)
        {
            enemy.isJumpInput = true;
            enemy.isAttackInput = true;
            return;
        }

        // 랜덤 점프 패턴 (플랫지형 대응)
        if (Time.time >= lastJumpCheckTime + jumpCheckInterval)
        {
            lastJumpCheckTime = Time.time;

            if (enemy.IsGrounded() && Random.value < jumpChance)
            {
                enemy.isJumpInput = true;
                return;
            }
        }


        // 대시 접근
        if (!enemy.isDashing && distance >= dashRange && Mathf.Abs(dirToPlayer.x) > 1f)
        {
            enemy.dashDir = (int)Mathf.Sign(dirToPlayer.x);
            enemy.isDashInput = true;
            return;
        }

        // 백스텝
        if (Random.value < 0.05f && distance < 2f)
        {
            enemy.inputX = -Mathf.Sign(dirToPlayer.x);
            enemy.isDashInput = true;
            return;
        }

        // 방어
        if (Random.value < 0.05f && distance < 2f)
        {
            enemy.isBlocking = true;
            return;
        }

        // 공격
        if (distance <= attackRange)
        {
            Debug.Log("[AI] 공격 입력 감지");
            enemy.isAttackInput = true;
            return;
        }
    }
}
