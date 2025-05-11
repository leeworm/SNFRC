using UnityEngine;

public class HK_Enemy_Bass_AI : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 10f;
    public float shootRange = 6f;
    public float kickRange = 2.5f;
    public float decisionInterval = 1.5f;
    public float minDistance = 3f; // 너무 가까울 때 뒤로 빠지는 거리

    private float timer;
    private HK_Enemy_Bass bass;
    private HK_EnemyStateMachine stateMachine;
    private System.Type currentStateType;

    // 쿨타임 설정
    private float rapidFireCooldown = 5f;    // RapidFire 1의 쿨타임
    private float rapidFire2Cooldown = 10f;  // RapidFire 2의 쿨타임
    private float kickCooldown = 5f;         // 킥 공격 쿨타임
    private float dodgeCooldown = 3f;        // 회피 쿨타임

    private float rapidFireTimer = 0f;
    private float rapidFire2Timer = 0f;
    private float kickTimer = 0f;
    private float dodgeTimer = 0f;

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

        // 쿨타임 타이머 업데이트
        timer -= Time.deltaTime;
        rapidFireTimer -= Time.deltaTime;
        rapidFire2Timer -= Time.deltaTime;
        kickTimer -= Time.deltaTime;
        dodgeTimer -= Time.deltaTime;

        // 주기적으로 MakeDecision 호출
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
        bool shouldDodgeBullet = IsBulletComingTowardsBass();

        // 회피 상태로 전환
        if (shouldDodgeBullet && dodgeTimer <= 0f)
        {
            ChangeState(new HK_BassJumpState(bass));
            dodgeTimer = dodgeCooldown;
            return;
        }

        // 너무 가까울 때 뒤로 빠짐
        if (distance < minDistance)
        {
            bass.moveDirection = (transform.position - player.position).normalized;
            ChangeState(new HK_BassMoveState(bass));
            return;
        }

        // 플레이어와 일정 거리 이상 떨어지면 Idle 상태로
        if (distance > detectionRange)
        {
            ChangeState(new HK_BassIdleState(bass));
        }
        else if (distance > shootRange)  // 공격 범위보다 멀면 이동
        {
            bass.moveDirection = (player.position - transform.position).normalized;
            ChangeState(new HK_BassMoveState(bass));
        }
        else if (distance > kickRange)  // 킥 범위보다 멀면 공격
        {
            // RapidFire 공격이 쿨타임이 끝났을 때만 실행
            if (rapidFireTimer <= 0f || rapidFire2Timer <= 0f)
            {
                int rapidFireType = Random.Range(1, 3);  // 1 또는 2
                bass.FireRapidShot(rapidFireType == 1 ? bass.rapidShotPrefab : bass.rapidShotPrefab2);

                if (rapidFireType == 1)
                    rapidFireTimer = rapidFireCooldown;
                else
                    rapidFire2Timer = rapidFire2Cooldown;
            }
            else
            {
                bass.moveDirection = (player.position - transform.position).normalized;
                ChangeState(new HK_BassMoveState(bass));
            }
        }
        else  // 킥 범위 내에서는 킥 공격
        {
            if (kickTimer <= 0f)
            {
                ChangeState(new HK_BassKickState(bass));
                kickTimer = kickCooldown;
            }
            else
            {
                bass.moveDirection = (player.position - transform.position).normalized;
                ChangeState(new HK_BassMoveState(bass));
            }
        }
    }

    bool IsBulletComingTowardsBass()
    {
        var bullets = GameObject.FindGameObjectsWithTag("Skill");
        foreach (var bullet in bullets)
        {
            var bulletDirection = (bullet.transform.position - transform.position).normalized;
            var bassDirection = (player.position - transform.position).normalized;
            if (Vector2.Dot(bulletDirection, bassDirection) > 0)
            {
                return true;
            }
        }
        return false;
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
