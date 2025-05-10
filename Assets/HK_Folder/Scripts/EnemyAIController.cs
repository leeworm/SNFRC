using UnityEngine;

public class EnemyAIController : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 10f;     // �ν� ����
    public float attackRange = 2f;         // ���� ����
    public float decisionCooldown = 1.5f;  // �Ǵ� �ֱ�

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
            stateMachine.ChangeState(new BassIdleState(bass));  // Idle ���·� ����
        }
        else if (distance > attackRange)
        {
            stateMachine.ChangeState(new BassMoveState(bass));  // Move ���·� ����
        }
        else
        {
            int rand = Random.Range(0, 2); // ���� ���� ����
            switch (rand)
            {
                case 0: stateMachine.ChangeState(new BassRapidFireState(bass)); break;
                
                case 1: stateMachine.ChangeState(new BassKickState(bass)); break;
            }
        }
    }
}
