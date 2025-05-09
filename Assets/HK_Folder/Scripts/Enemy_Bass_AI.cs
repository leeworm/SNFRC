using UnityEngine;

public class Enemy_Bass_AI : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 10f;     // 인식 범위
    public float shootRange = 6f;          // 원거리 공격 범위
    public float kickRange = 2.5f;         // 근접 킥 범위
    public float decisionInterval = 1.0f;  // 판단 주기

    private float timer;
    private EnemyStateMachine stateMachine;
    private Enemy_Bass bass;
    private System.Type currentStateType;

    void Awake()
    {
        stateMachine = GetComponent<EnemyStateMachine>();
        bass = GetComponent<Enemy_Bass>();

        if (bass.animator == null)
            bass.animator = GetComponent<Animator>();
    }

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player")?.transform;

        bass.player = player;
        ChangeState(new BassIdleState(bass));
        timer = decisionInterval;
    }

    void Update()
    {
        if (player == null) return;

        // 방향 설정: 애니메이션에서 방향을 처리하므로 transform.localScale 변경을 하지 않음
        UpdateDirection();  // 방향 설정 함수 호출

        // AI 판단 간격
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            MakeDecision();
            timer = decisionInterval;
        }
    }

    // 방향을 설정하는 함수 (애니메이션에 맡기도록 수정)
    void UpdateDirection()
    {
        Vector3 dir = player.position - transform.position;

        if (dir.x > 0)
            transform.localScale = new Vector3(-1, 1, 1); // 오른쪽 바라보기 (기본이 왼쪽이므로 반전)
        else if (dir.x < 0)
            transform.localScale = new Vector3(1, 1, 1);  // 왼쪽 바라보기
    }


    void MakeDecision()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > detectionRange)
        {
            // 너무 멀면 대기
            ChangeState(new BassIdleState(bass));
        }
        else if (distance > shootRange)
        {
            // 중간 거리 – 이동
            ChangeState(new BassMoveState(bass));
        }
        else if (distance > kickRange)
        {
            // 원거리 공격
            ChangeState(new BassRapidFireState(bass));
        }
        else
        {
            // 근접 킥 공격
            ChangeState(new BassKickState(bass));
        }
    }

    void ChangeState(IEnemyState newState)
    {
        if (newState.GetType() == currentStateType) return;

        stateMachine.ChangeState(newState);
        currentStateType = newState.GetType();
    }
}
