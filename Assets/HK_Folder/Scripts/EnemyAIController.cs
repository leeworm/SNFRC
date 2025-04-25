using UnityEngine;

public class EnemyAIController : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public float decisionCooldown = 1.5f;

    private float decisionTimer;
    private EnemyStateMachine stateMachine;

    void Start()
    {
        stateMachine = GetComponent<EnemyStateMachine>();
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
        /*    stateMachine.ChangeState("Idle");
        }
        else if (distance > attackRange)
        {
            stateMachine.ChangeState("Move");
        }
        else
        {
            int rand = Random.Range(0, 3); // 공격 종류 선택
            switch (rand)
            {
                case 0: stateMachine.ChangeState("Attack1"); break;
                case 1: stateMachine.ChangeState("Attack2"); break;
                case 2: stateMachine.ChangeState("Kick"); break;
            }*/
        }
    }

}
