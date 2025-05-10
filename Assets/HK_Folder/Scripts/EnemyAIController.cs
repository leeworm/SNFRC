using UnityEngine;

public class EnemyAIController : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 10f;     // 인식 범위
    public float attackRange = 2f;         // 공격 범위
    public float decisionCooldown = 1.5f;  // 판단 주기

    private float decisionTimer;
    private EnemyStateMachine stateMachine;
    private Enemy_Bass bass;

    void Start()
    {
        stateMachine = GetComponent<EnemyStateMachine>();
        bass = GetComponent<Enemy_Bass>();
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
            stateMachine.ChangeState(new BassIdleState(bass));  // Idle 상태로 변경
        }
        else if (distance > attackRange)
        {
            stateMachine.ChangeState(new BassMoveState(bass));  // Move 상태로 변경
        }
        else
        {
            int rand = Random.Range(0, 2); // 공격 종류 선택
            switch (rand)
            {
                case 0: stateMachine.ChangeState(new BassRapidFireState(bass)); break;
                
                case 1: stateMachine.ChangeState(new BassKickState(bass)); break;
            }
        }
    }
}
