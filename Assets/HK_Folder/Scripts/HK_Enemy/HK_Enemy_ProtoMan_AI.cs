using UnityEngine;

public class HK_Enemy_ProtoMan_AI : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 8f;
    public float attackRange = 2.5f;
    public float decisionInterval = 1.2f;

    private float timer;
    private HK_EnemyStateMachine stateMachine;
    private HK_Enemy_ProtoMan protoMan;

    private System.Type currentStateType;

    void Awake()
    {
        stateMachine = GetComponent<HK_EnemyStateMachine>();
        protoMan = GetComponent<HK_Enemy_ProtoMan>();

        if (protoMan == null)
            Debug.LogError("Enemy_ProtoMan ������Ʈ�� ã�� �� �����ϴ�!");

        if (protoMan.animator == null)
            protoMan.animator = GetComponent<Animator>();

        if (protoMan.animator == null)
            Debug.LogError("Animator�� ProtoMan�� ����Ǿ� ���� �ʽ��ϴ�.");
    }

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player")?.transform;

        protoMan.player = player;

        if (stateMachine != null && protoMan != null)
        {
            ChangeState(new HK_ProtoManIdleState(protoMan));
        }

        timer = decisionInterval;
    }

    void Update()
    {
        Vector3 direction = player.position - transform.position;
        if (direction.x > 0)
        {
            // �÷��̾ �����ʿ� ���� ��
            transform.localScale = new Vector3(1, 1, 1); // �������� ���ϰ�
        }
        else if (direction.x < 0)
        {
            // �÷��̾ ���ʿ� ���� ��
            transform.localScale = new Vector3(-1, 1, 1); // ������ ���ϰ�
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
            ChangeState(new HK_ProtoManIdleState(protoMan));
        }
        else if (distance > attackRange)
        {
            ChangeState(new HK_ProtoManMoveState(protoMan));
        }
        else
        {
            int r = Random.Range(0, 3);
            switch (r)
            {
                case 0: ChangeState(new HK_ProtoManAttackState(protoMan)); break;
                case 1: ChangeState(new HK_ProtoManChargeShotState(protoMan)); break;
                case 2: ChangeState(new HK_ProtoManJumpState(protoMan)); break;
            }
        }
    }

    void ChangeState(HK_IEnemyState newState)
    {
        if (newState.GetType() == currentStateType) return;

        stateMachine.ChangeState(newState);
        currentStateType = newState.GetType();
    }
}
