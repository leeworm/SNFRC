using Unity.VisualScripting;
using UnityEngine;

public class JH_Entity : MonoBehaviour
{
    #region Components
    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public SpriteRenderer sr { get; private set; }

    #endregion

    [Header("�⺻����")]
    public int MaxHP = 100;
    public int CurrentHP;
    public float MoveSpeed = 5f;
    public float JumpForce = 5f;
    public Transform POS;

    [Header("�Ÿ�����")]
    [SerializeField] protected Transform GroundCheck;
    [SerializeField] protected float GroundCheckDistance;
    [SerializeField] protected Transform EnemyCheck;
    [SerializeField] protected float EnemyCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;

    protected float jumpGracePeriodTimer = 0f; // protected�� �����Ͽ� �ڽ� Ŭ�������� ���Ѵٸ� ���� ���� (������ public �Լ� ���� ����)
    public float jumpGraceDuration = 0.15f;  // Inspector���� ���� �����ϵ��� public���� ����

    public int facingDir { get; private set; } = 1;

    protected virtual void Awake()
    {
       
    }

    protected virtual void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        if (jumpGracePeriodTimer > 0)
        {
            jumpGracePeriodTimer -= Time.deltaTime;
        }
    }

    protected virtual void Exit()
    {

    }

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.linearVelocity = new Vector2(_xVelocity, _yVelocity);
    }

    #region �浹
    public virtual bool IsGroundDetected()
    {
        // --- ���� ���� �Ⱓ üũ ���� �߰� ---
        if (jumpGracePeriodTimer > 0)
        {
            return false; // ���� �Ⱓ �߿��� �׻� �������� ����
        }
        // --- �߰� �� ---
        if (GroundCheck == null) return false; // GroundCheck Transform�� �Ҵ���� ���� ��� ���� ����
        return Physics2D.Raycast(GroundCheck.position, Vector2.down, GroundCheckDistance, whatIsGround);
    }
    public virtual bool IsEnemyDetected() => Physics2D.Raycast(EnemyCheck.position, Vector2.right * facingDir, EnemyCheckDistance, whatIsGround);


    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(GroundCheck.position, new Vector3(GroundCheck.position.x, GroundCheck.position.y - GroundCheckDistance));
        Gizmos.DrawLine(EnemyCheck.position, new Vector3(EnemyCheck.position.x + EnemyCheckDistance, EnemyCheck.position.y));
    }
    #endregion

    public void InitiateJumpGracePeriod()
    {
        jumpGracePeriodTimer = jumpGraceDuration;
    }
}

