// ✅ DH_EnemyAIController: 모든 Enemy 상태를 AI로 제어 (SexyJutsu 제외)

using UnityEngine;

public class DH_EnemyAIController : MonoBehaviour
{
    private DH_Enemy enemy;
    private Transform player;

    [Header("AI 설정")]
    public float decisionInterval = 0.3f;
    public float attackRange = 1.5f;
    public float dashRange = 3f;
    public float jumpHeightThreshold = 1f;

    private float lastDecisionTime;

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

        Vector2 dir = player.position - enemy.transform.position;
        float distance = dir.magnitude;
        float yDiff = player.position.y - enemy.transform.position.y;

        // 기본 방향 설정
        enemy.inputX = Mathf.Sign(dir.x);

        // 어퍼컷 조건: 가까이 + 위
        if (distance <= 1.2f && yDiff > jumpHeightThreshold)
        {
            enemy.isUpInput = true;
            enemy.isAttackInput = true;
            return;
        }

        // 점프 공격 (위에 있고 거리도 중간)
        if (distance <= 2f && yDiff > jumpHeightThreshold)
        {
            enemy.isJumpInput = true;
            enemy.isAttackInput = true;
            return;
        }

        // 점프 (단차 대응)
        if (yDiff > jumpHeightThreshold)
        {
            enemy.isJumpInput = true;
            return;
        }

        // 대시로 접근
        if (distance >= dashRange && Mathf.Abs(dir.x) > 1f)
        {
            enemy.isDashInput = true;
            return;
        }

        // 백스텝 (플레이어 뒤로 이동 시도)
        if (Random.value < 0.05f && distance < 2f)
        {
            enemy.inputX = -enemy.facingDir;
            enemy.isDashInput = true;
            return;
        }

        // 방어 (확률적으로)
        if (Random.value < 0.05f && distance < 2f)
        {
            enemy.isBlocking = true;
            return;
        }

        // 콤보 공격
        if (distance <= attackRange)
        {
            enemy.isAttackInput = true;
            return;
        }

        // 이동
        enemy.inputX = Mathf.Sign(dir.x);
    }
}
