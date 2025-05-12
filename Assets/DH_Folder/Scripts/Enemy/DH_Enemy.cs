using System.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class DH_Enemy: DH_Entity
{
    public Vector2 lastKnockback;

    //[Header("Skill Effects")]
    [HideInInspector] public GameObject blackFadePrefab;
    [HideInInspector] public GameObject skillEffectPrefab;

    [Header("Effect info")]
    public GameObject effectPrefab;
    public Transform effectSpawnPoint; // 이펙트 생성 위치 (예: 손 위치, 무기 위치)

    [Header("Attack Info")]
    public float attackDistance;
    public float attackCooldown;
    [HideInInspector] public float lasttimeAttacked;
    [SerializeField] protected LayerMask whatIsEnemy;
    public GameObject attackHitbox;


    [Header("Combo")]
    public int primaryAttackComboCounter = 0;
    public float comboWindow = 0.7f; // 콤보 입력 유효 시간
    public bool bufferedAttackInput = false;

    [Header("Movement info")]
    public float moveSpeed;
    public float jumpForce;

    [Header("Dash info")]
    public float dashSpeed;
    public float dashDir { get; private set; } // 대시 방향 (1: 오른쪽, -1: 왼쪽)

    [Header("Jump info")]
    public int maxJumpCount = 2;
    public int currentJumpCount;

    [Header("Substitution info")]
    public float substitutionCooldown = 1.5f;
    private float lastSubstitutionTime = -999f;
    public bool canSubstitute()
    {
        return Time.time >= lastSubstitutionTime + substitutionCooldown;
    }

    [HideInInspector] public BoxCollider2D col;
    [HideInInspector] public Vector2 originalColliderSize;
    [HideInInspector] public Vector2 originalColliderOffset;

    [Header("Attack Hitboxes")]
    public GameObject primaryHitbox;
    public GameObject primaryFinalHitbox;
    public GameObject uppercutHitbox;
    public GameObject dashAttackHitbox;
    public GameObject airAttackHitbox;

    public float inputX;
    public bool isAttackInput;
    public bool isJumpInput;
    public bool isDashInput;
    public bool isUpInput;


    #region States
    public DH_EnemyStateMachine stateMachine { get; private set; }
    public DH_EnemyState currentState { get; private set; }
    public DH_EnemyIdleState idleState { get; private set; }
    public DH_EnemyMoveState moveState { get; private set; }
    public DH_EnemyJumpState jumpState { get; private set; }
    public DH_EnemyTeleportJumpState teleportJumpState { get; private set; }
    public DH_EnemyAirState airState { get; private set; }
    public DH_EnemyLandState landState { get; private set; }
    public DH_EnemyDashState dashState { get; private set; }
    public DH_EnemyDashAttackState dashAttackState { get; private set; }
    public DH_EnemyBackstepState backstepState { get; private set; }
    public DH_EnemyPrimaryAttackState primaryAttack { get; private set; }
    public DH_EnemyAirAttackState airAttackState { get; private set; }
    public DH_EnemyUppercutState uppercutState { get; private set; }
    public DH_EnemyDefenseState defenseState { get; private set; }
    public DH_EnemyAirDefenseState airDefenseState { get; private set; }
    public DH_EnemySexyJutsuState SexyJutsuState { get; private set; }
    public DH_EnemyCrouchState crouchState { get; private set; }
    public DH_EnemySubstituteState substituteState { get; private set; }
    public DH_CommandDetector CommandDetector { get; private set; }
    public DH_EnemyDeadState deadState { get; private set; }
    public DH_EnemyHurtState hurtState { get; private set; }
    public DH_EnemyKnockdownState knockdownState { get; private set; }

    #endregion


    protected override void Awake()
    {
        base.Awake();
        // 상태 머신 인스턴스 생성
        stateMachine = new DH_EnemyStateMachine();
        CommandDetector = new DH_CommandDetector();

        // 각 상태 인스턴스 생성 (this: 플레이어 객체, stateMachine: 상태 머신, "Idle"/"Move": 상태 이름)
        idleState = new DH_EnemyIdleState(this, stateMachine, "Idle");
        moveState = new DH_EnemyMoveState(this, stateMachine, "Move");
        jumpState = new DH_EnemyJumpState(this, stateMachine, "Jump", lastXVelocity);
        teleportJumpState = new DH_EnemyTeleportJumpState(this, stateMachine, "Vanish");
        airState = new DH_EnemyAirState(this, stateMachine, "Jump");
        landState = new DH_EnemyLandState(this, stateMachine, "Land");
        dashState = new DH_EnemyDashState(this, stateMachine, "Dash", facingDir);
        dashAttackState = new DH_EnemyDashAttackState(this, stateMachine, "DashAttack");
        backstepState = new DH_EnemyBackstepState(this, stateMachine, "Backstep", facingDir);
        primaryAttack = new DH_EnemyPrimaryAttackState(this, stateMachine, "Attack");
        uppercutState = new DH_EnemyUppercutState(this, stateMachine, "Uppercut");
        airAttackState = new DH_EnemyAirAttackState(this, stateMachine, "AirAttack");
        defenseState = new DH_EnemyDefenseState(this, stateMachine, "Block");
        airDefenseState = new DH_EnemyAirDefenseState(this, stateMachine, "AirBlock");
        SexyJutsuState = new DH_EnemySexyJutsuState(this, stateMachine, "Skill1");
        crouchState = new DH_EnemyCrouchState(this, stateMachine, "Crouch");
        substituteState = new DH_EnemySubstituteState(this, stateMachine, "Vanish");
        deadState = new DH_EnemyDeadState(this, stateMachine, "Die");
        hurtState = new DH_EnemyHurtState(this, stateMachine, "Hurt");
        knockdownState = new DH_EnemyKnockdownState(this, stateMachine, "Knockdown");
    }

    protected override void Start()
    {
        base.Start();
        currentJumpCount = maxJumpCount;
        stateMachine.Initialize(idleState);

        col = GetComponent<BoxCollider2D>();

        originalColliderSize = col.size;
        originalColliderOffset = col.offset;

        isBlocking = false;
        isLanding = false;
        isAttackingAir = false;
        commandDetectorEnabled = true;

        currentHealth = maxHealth;
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }
    public void SetCurrentState(DH_EnemyState state)
    {
        currentState = state;
    }

    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;
        yield return new WaitForSeconds(_seconds);
        isBusy = false;
    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    public Transform GetNearestEnemy()
    {
        float range = 10f;
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, range, LayerMask.GetMask("Enemy"));
        Transform nearest = null;
        float minDistance = Mathf.Infinity;

        foreach (var hit in hits)
        {
            float dist = Vector2.Distance(transform.position, hit.transform.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                nearest = hit.transform;
            }
        }
        return nearest;
    }

    public override void FlipController(float _x)
    {
        // 백스텝 상태에서는 Flip 막기
        if (stateMachine.currentState is DH_EnemyBackstepState)
            return;

        base.FlipController(_x); // 나머지는 기존 방식 유지
    }

    public void ActivateHitbox(string hitboxName)
    {
        switch (hitboxName)
        {
            case "Primary":
                primaryHitbox?.SetActive(true);
                break;
            case "PrimaryFinal":
                primaryFinalHitbox?.SetActive(true);
                break;
            case "Uppercut":
                uppercutHitbox?.SetActive(true);
                break;
            case "DashAttack":
                dashAttackHitbox?.SetActive(true);
                break;
            case "AirAttack":
                airAttackHitbox?.SetActive(true);
                break;
        }
    }

    public void DeactivateHitbox(string hitboxName)
    {
        switch (hitboxName)
        {
            case "Primary":
                primaryHitbox?.SetActive(false);
                break;
            case "PrimaryFinal":
                primaryFinalHitbox?.SetActive(false);
                break;
            case "Uppercut":
                uppercutHitbox?.SetActive(false);
                break;
            case "DashAttack":
                dashAttackHitbox?.SetActive(false);
                break;
            case "AirAttack":
                airAttackHitbox?.SetActive(false);
                break;
        }
    }

    public bool IsGrounded() => IsGroundDetected();
}
