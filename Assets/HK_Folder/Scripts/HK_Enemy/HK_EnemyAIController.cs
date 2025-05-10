using UnityEngine;

public class HK_EnemyAIController : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 10f;     // �ν� ����
    public float attackRange = 2f;         // ���� ����
    public float decisionCooldown = 1.5f;  // �Ǵ� �ֱ�

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
            stateMachine.ChangeState(new HK_BassIdleState(bass));  // Idle ���·� ����
        }
        else if (distance > attackRange)
        {
            stateMachine.ChangeState(new HK_BassMoveState(bass));  // Move ���·� ����
        }
        else
        {
            int rand = Random.Range(0, 2); // ���� ���� ����
            switch (rand)
            {
                case 0: stateMachine.ChangeState(new HK_BassRapidFireState(bass)); break;
                
                case 1: stateMachine.ChangeState(new HK_BassKickState(bass)); break;
            }
        }
    }
}
