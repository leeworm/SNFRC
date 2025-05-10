using UnityEngine;

public class HK_Enemy_Bass_AI : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 10f;
    public float shootRange = 6f;
    public float kickRange = 2.5f;
    public float decisionInterval = 1f;

    private float timer;
    private HK_Enemy_Bass bass;
    private HK_EnemyStateMachine stateMachine;
    private System.Type currentStateType;

    void Awake()
    {
        bass = GetComponent<HK_Enemy_Bass>();
        stateMachine = GetComponent<HK_EnemyStateMachine>();
    }

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        bass.player = player;
        ChangeState(new HK_BassIdleState(bass));
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
            ChangeState(new HK_BassIdleState(bass));
        else if (distance > shootRange)
            ChangeState(new HK_BassMoveState(bass));
        else if (distance > kickRange)
            ChangeState(new HK_BassRapidFireState(bass));
        else
            ChangeState(new HK_BassKickState(bass));
    }

    void UpdateDirection()
    {
        Vector3 dir = player.position - transform.position;
        if (dir.x > 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);
    }

    void ChangeState(HK_IEnemyState newState)
    {
        if (newState.GetType() == currentStateType) return;
        stateMachine.ChangeState(newState);
        currentStateType = newState.GetType();
    }
}
