using UnityEngine;

public class Enemy_ProtoMan_AI : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 8f;
    public float attackRange = 2.5f;
    public float decisionInterval = 1.2f;

    private float timer;
    private EnemyStateMachine stateMachine;
    private Enemy_ProtoMan protoMan;

    void Start()
    {
        stateMachine = GetComponent<EnemyStateMachine>();
        protoMan = GetComponent<Enemy_ProtoMan>(); 

        player = GameObject.FindGameObjectWithTag("Player").transform;

        stateMachine.ChangeState(new ProtoManIdleState(protoMan));
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
            stateMachine.ChangeState(new ProtoManIdleState(protoMan));
        }
        else if (distance > attackRange)
        {
            stateMachine.ChangeState(new ProtoManMoveState(protoMan));
        }
        else
        {
            int r = Random.Range(0, 3);
            switch (r)
            {
                case 0: stateMachine.ChangeState(new ProtoManAttackState(protoMan)); break;
                case 1: stateMachine.ChangeState(new ProtoManDashAttackState(protoMan)); break;
                case 2: stateMachine.ChangeState(new ProtoManJumpState(protoMan)); break;
            }
        }
    }
}
