using UnityEngine;

public class Enemy_ProtoMan_AI : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 8f;
    public float attackRange = 2.5f;
    public float decisionInterval = 1.2f;

    private float timer;
    private EnemyStateMachine stateMachine;

    void Start()
    {
        stateMachine = GetComponent<EnemyStateMachine>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            MakeDecision();
            timer = decisionInterval;
        }
    }

    void MakeDecision()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > detectionRange)
        {
         /*   stateMachine.ChangeState("Idle");
        }
        else if (distance > attackRange)
        {
            stateMachine.ChangeState("Move");
        }
        else
        {
            int r = Random.Range(0, 3);
            switch (r)
            {
                case 0: stateMachine.ChangeState("Attack1"); break; // 기본샷
                case 1: stateMachine.ChangeState("Attack2"); break; // 대쉬슬래시
                case 2: stateMachine.ChangeState("Jump"); break;    // 점프회피
            }*/
        }
    }
}
