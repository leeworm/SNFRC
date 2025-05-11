using UnityEngine;

public class HK_Enemy_Bass_AI : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 10f;
    public float shootRange = 6f;
    public float kickRange = 2.5f;
    public float decisionInterval = 1.5f;

    private float timer;
    private HK_Enemy_Bass bass;
    private HK_EnemyStateMachine stateMachine;
    private System.Type currentStateType;

    // 쿨타임 설정
    private float rapidFireCooldown = 3f;
    private float rapidFire2Cooldown = 2f;
    private float kickCooldown = 5f;
    private float dodgeCooldown = 3f;

    private float rapidFireTimer = 0f;
    private float rapidFire2Timer = 0f;
    private float kickTimer = 0f;
    private float dodgeTimer = 0f;

    private float dodgeChance = 0.5f;

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
        rapidFireTimer -= Time.deltaTime;
        rapidFire2Timer -= Time.deltaTime;
        kickTimer -= Time.deltaTime;
        dodgeTimer -= Time.deltaTime;

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

        // 플레이어 공격 중일 때 회피 행동
        bool playerAttacking = distance < 4f; // 추후 개선 가능
        if (playerAttacking && dodgeTimer <= 0f && Random.value < dodgeChance)
        {
            ChangeState(new HK_BassMoveState(bass));
            dodgeTimer = dodgeCooldown;
            return;
        }

        if (distance > detectionRange)
        {
            ChangeState(new HK_BassIdleState(bass));
        }
        else if (distance > shootRange)
        {
            ChangeState(new HK_BassMoveState(bass));
        }
        else if (distance > kickRange)
        {
            if (rapidFireTimer <= 0f)
            {
                int rapidFireType = Random.Range(1, 3);  // 1 또는 2
                ChangeState(new HK_BassRapidFireState(bass, rapidFireType));
                rapidFireTimer = rapidFireCooldown;
            }
            else if (rapidFire2Timer <= 0f)
            {
                ChangeState(new HK_BassRapidFire2State(bass));
                rapidFire2Timer = rapidFire2Cooldown;
            }
            else
            {
                ChangeState(new HK_BassMoveState(bass));
            }
        }
        else
        {
            if (kickTimer <= 0f)
            {
                ChangeState(new HK_BassKickState(bass));
                kickTimer = kickCooldown;
            }
            else
            {
                ChangeState(new HK_BassMoveState(bass));
            }
        }
    }

    void UpdateDirection()
    {
        Vector3 dir = player.position - transform.position;
        transform.localScale = new Vector3(dir.x > 0 ? -1 : 1, 1, 1);
    }

    void ChangeState(HK_IEnemyState newState)
    {
        if (newState.GetType() == currentStateType) return;
        stateMachine.ChangeState(newState);
        currentStateType = newState.GetType();
    }
}
