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

    private System.Type currentStateType;

    void Awake()
    {
        stateMachine = GetComponent<EnemyStateMachine>();
        protoMan = GetComponent<Enemy_ProtoMan>();

        if (protoMan == null)
            Debug.LogError("Enemy_ProtoMan 컴포넌트를 찾을 수 없습니다!");

        if (protoMan.animator == null)
            protoMan.animator = GetComponent<Animator>();

        if (protoMan.animator == null)
            Debug.LogError("Animator가 ProtoMan에 연결되어 있지 않습니다.");
    }

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player")?.transform;

        protoMan.player = player;

        if (stateMachine != null && protoMan != null)
        {
            ChangeState(new ProtoManIdleState(protoMan));
        }

        timer = decisionInterval;
    }

    void Update()
    {
        Vector3 direction = player.position - transform.position;
        if (direction.x > 0)
        {
            // 플레이어가 오른쪽에 있을 때
            transform.localScale = new Vector3(1, 1, 1); // 오른쪽을 향하게
        }
        else if (direction.x < 0)
        {
            // 플레이어가 왼쪽에 있을 때
            transform.localScale = new Vector3(-1, 1, 1); // 왼쪽을 향하게
        }

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            MakeDecision();
            timer = decisionInterval;
        }
    }

    void MakeDecision()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > detectionRange)
        {
            ChangeState(new ProtoManIdleState(protoMan));
        }
        else if (distance > attackRange)
        {
            ChangeState(new ProtoManMoveState(protoMan));
        }
        else
        {
            int r = Random.Range(0, 3);
            switch (r)
            {
                case 0: ChangeState(new ProtoManAttackState(protoMan)); break;
                case 1: ChangeState(new ProtoManChargeShotState(protoMan)); break;
                case 2: ChangeState(new ProtoManJumpState(protoMan)); break;
            }
        }
    }

    void ChangeState(IEnemyState newState)
    {
        if (newState.GetType() == currentStateType) return;

        stateMachine.ChangeState(newState);
        currentStateType = newState.GetType();
    }
}
