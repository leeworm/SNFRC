using UnityEngine;

public class HK_Enemy_Bass_AI : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 10f;
    public float shootRange = 6f;
    public float kickRange = 2.5f;
    public float decisionInterval = 1.5f;
    public float minDistance = 3f; // �ʹ� ����� �� �ڷ� ������ �Ÿ�

    private float timer;
    private HK_Enemy_Bass bass;
    private HK_EnemyStateMachine stateMachine;
    private System.Type currentStateType;

    // ��Ÿ�� ����
    private float rapidFireCooldown = 5f;    // RapidFire 1�� ��Ÿ��
    private float rapidFire2Cooldown = 10f;  // RapidFire 2�� ��Ÿ��
    private float kickCooldown = 5f;         // ű ���� ��Ÿ��
    private float dodgeCooldown = 3f;        // ȸ�� ��Ÿ��

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

        // ��Ÿ�� Ÿ�̸� ������Ʈ
        timer -= Time.deltaTime;
        rapidFireTimer -= Time.deltaTime;
        rapidFire2Timer -= Time.deltaTime;
        kickTimer -= Time.deltaTime;
        dodgeTimer -= Time.deltaTime;

        // �ֱ������� MakeDecision ȣ��
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

        // ȸ�� ���·� ��ȯ
        if (shouldDodgeBullet && dodgeTimer <= 0f)
        {
            ChangeState(new HK_BassJumpState(bass));
            dodgeTimer = dodgeCooldown;
            return;
        }

        // �ʹ� ����� �� �ڷ� ����
        if (distance < minDistance)
        {
            bass.moveDirection = (transform.position - player.position).normalized;
            ChangeState(new HK_BassMoveState(bass));
            return;
        }

        // �÷��̾�� ���� �Ÿ� �̻� �������� Idle ���·�
        if (distance > detectionRange)
        {
            ChangeState(new HK_BassIdleState(bass));
        }
        else if (distance > shootRange)  // ���� �������� �ָ� �̵�
        {
            bass.moveDirection = (player.position - transform.position).normalized;
            ChangeState(new HK_BassMoveState(bass));
        }
        else if (distance > kickRange)  // ű �������� �ָ� ����
        {
            // RapidFire ������ ��Ÿ���� ������ ���� ����
            if (rapidFireTimer <= 0f || rapidFire2Timer <= 0f)
            {
                int rapidFireType = Random.Range(1, 3);  // 1 �Ǵ� 2
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
        else  // ű ���� �������� ű ����
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
