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

    // ��Ÿ�� ����
    private float rapidFireCooldown = 3f;
    /*private float rapidFire2Cooldown = 2f;*/
    private float kickCooldown = 5f;
    private float dodgeCooldown = 3f;

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

        // �÷��̾� �Ѿ��� �轺�� ���� ���ƿ����� üũ
        bool shouldDodgeBullet = IsBulletComingTowardsBass();

        if (shouldDodgeBullet && dodgeTimer <= 0f)
        {
            // ���� ������ ����Ͽ� ȸ��
            ChangeState(new HK_BassJumpState(bass));  // ���� ���·� ��ȯ
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
                int rapidFireType = Random.Range(1, 3);  // 1 �Ǵ� 2
                ChangeState(new HK_BassRapidFireState(bass, rapidFireType));
                rapidFireTimer = rapidFireCooldown;
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

    // �÷��̾��� �Ѿ��� �轺�� ���� ���ƿ����� üũ�ϴ� �Լ�
    bool IsBulletComingTowardsBass()
    {
        // �÷��̾��� �Ѿ��� ���� �轺�� ���� ���ƿ����� ���θ� üũ�ϴ� ����
        // ���� ���, �÷��̾ �߻��� �Ѿ��� ��ġ�� �˰� �־�� �մϴ�.

        var bullets = GameObject.FindGameObjectsWithTag("Skill");  // "Bullet" �±׸� ���� ��� �Ѿ� ã��
        foreach (var bullet in bullets)
        {
            var bulletDirection = (bullet.transform.position - transform.position).normalized;
            var bassDirection = (player.position - transform.position).normalized;
            if (Vector2.Dot(bulletDirection, bassDirection) > 0)  // �Ѿ��� �轺�� ���� ���ƿ����� üũ
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
