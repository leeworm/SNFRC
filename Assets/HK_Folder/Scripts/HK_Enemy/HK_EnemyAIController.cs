using UnityEngine;

public class HK_EnemyAIController : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 10f;     // 감지 범위
    public float attackRange = 2f;         // 공격 범위
    public float decisionCooldown = 1.5f;  // 결정 주기

    private float decisionTimer;
    private HK_EnemyStateMachine stateMachine;
    private HK_Enemy_Bass bass;

    void Start()
    {
        stateMachine = GetComponent<HK_EnemyStateMachine>();
        bass = GetComponent<HK_Enemy_Bass>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        decisionTimer -= Time.deltaTime;
        if (decisionTimer <= 0f)
        {
            MakeDecision();
            decisionTimer = decisionCooldown;
        }
    }

    void MakeDecision()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > detectionRange)
        {
            stateMachine.ChangeState(new HK_BassIdleState(bass));  // Idle 상태로 전환
        }
        else if (distance > attackRange)
        {
            stateMachine.ChangeState(new HK_BassMoveState(bass));  // Move 상태로 전환
        }
        else
        {
            int rand = Random.Range(0, 2); // 공격 종류 랜덤 선택
            switch (rand)
            {
                case 0:
                    int rapidFireType = Random.Range(1, 3); // 1 또는 2를 랜덤으로 선택
                    stateMachine.ChangeState(new HK_BassRapidFireState(bass, rapidFireType));
                    break;

                case 1:
                    stateMachine.ChangeState(new HK_BassKickState(bass));
                    break;
            }
        }
    }
}
