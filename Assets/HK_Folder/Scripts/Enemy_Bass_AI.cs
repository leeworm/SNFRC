using UnityEngine;

public class Enemy_Bass_AI : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 10f;
    public float shootRange = 6f;
    public float kickRange = 2.5f;
    public float decisionInterval = 1f;

    private float timer;
    private Enemy_Bass bass;
    private EnemyStateMachine stateMachine;
    private System.Type currentStateType;

    void Awake()
    {
        bass = GetComponent<Enemy_Bass>();
        stateMachine = GetComponent<EnemyStateMachine>();
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

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            MakeDecision();
            timer = decisionInterval;
        }

        UpdateDirection();
    }

    void MakeDecision()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > detectionRange)
            ChangeState(new BassIdleState(bass));
        else if (distance > shootRange)
            ChangeState(new BassMoveState(bass));
        else if (distance > kickRange)
            ChangeState(new BassRapidFireState(bass));
        else
            ChangeState(new BassKickState(bass));
    }

    void UpdateDirection()
    {
        Vector3 dir = player.position - transform.position;
        if (dir.x > 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);
    }

    void ChangeState(IEnemyState newState)
    {
        if (newState.GetType() == currentStateType) return;
        stateMachine.ChangeState(newState);
        currentStateType = newState.GetType();
    }
}
