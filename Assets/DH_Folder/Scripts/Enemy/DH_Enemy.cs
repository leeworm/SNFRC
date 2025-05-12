
using System.Collections;
using UnityEngine;

public class DH_Enemy : DH_Entity
{
    [Header("Drop Item")]
    public GameObject dropItemPrefab;
    public Transform dropSpawnPoint;

    public Vector2 lastKnockback;

    [Header("Effect info")]
    public GameObject effectPrefab;
    public Transform effectSpawnPoint;

    [Header("Attack Info")]
    public float attackDistance;
    public float attackCooldown;
    [HideInInspector] public float lasttimeAttacked;
    [SerializeField] protected LayerMask whatIsEnemy;
    public GameObject attackHitbox;

    [Header("Combo")]
    public int primaryAttackComboCounter = 0;
    public float comboWindow = 0.7f;
    public bool bufferedAttackInput = false;

    [Header("Movement info")]
    public float moveSpeed;
    public float jumpForce;

    [Header("Dash info")]
    public float dashSpeed;
    public int dashDir { get; set; } = 1;

    [Header("Jump info")]
    public int maxJumpCount = 2;
    public int currentJumpCount;

    [Header("Substitution info")]
    public float substitutionCooldown = 1.5f;
    private float lastSubstitutionTime = -999f;
    public bool canSubstitute() => Time.time >= lastSubstitutionTime + substitutionCooldown;

    [HideInInspector] public BoxCollider2D col;
    [HideInInspector] public Vector2 originalColliderSize;
    [HideInInspector] public Vector2 originalColliderOffset;

    [Header("Attack Hitboxes")]
    public GameObject primaryHitbox;
    public GameObject primaryFinalHitbox;
    public GameObject uppercutHitbox;
    public GameObject dashAttackHitbox;
    public GameObject airAttackHitbox;

    [Header("AI 입력값")]
    public float inputX;
    public bool isJumpInput;
    public bool isDashInput;
    public bool isAttackInput;
    public bool isUpInput;
    public bool isCrouching;

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
    public DH_EnemyCrouchState crouchState { get; private set; }
    public DH_EnemySubstituteState substituteState { get; private set; }
    public DH_EnemyDeadState deadState { get; private set; }
    public DH_EnemyHurtState hurtState { get; private set; }
    public DH_EnemyKnockDownState knockdownState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new DH_EnemyStateMachine();

        idleState = new DH_EnemyIdleState(this, stateMachine, "Idle");
        moveState = new DH_EnemyMoveState(this, stateMachine, "Move");
        jumpState = new DH_EnemyJumpState(this, stateMachine, "Jump");
        teleportJumpState = new DH_EnemyTeleportJumpState(this, stateMachine, "Vanish");
        airState = new DH_EnemyAirState(this, stateMachine, "Jump");
        landState = new DH_EnemyLandState(this, stateMachine, "Land");
        dashState = new DH_EnemyDashState(this, stateMachine, "Dash", facingDir);
        dashAttackState = new DH_EnemyDashAttackState(this, stateMachine, "DashAttack");
        backstepState = new DH_EnemyBackstepState(this, stateMachine, "Backstep");
        primaryAttack = new DH_EnemyPrimaryAttackState(this, stateMachine, "Attack");
        uppercutState = new DH_EnemyUppercutState(this, stateMachine, "Uppercut");
        airAttackState = new DH_EnemyAirAttackState(this, stateMachine, "AirAttack");
        defenseState = new DH_EnemyDefenseState(this, stateMachine, "Block");
        airDefenseState = new DH_EnemyAirDefenseState(this, stateMachine, "AirBlock");
        crouchState = new DH_EnemyCrouchState(this, stateMachine, "Crouch");
        substituteState = new DH_EnemySubstituteState(this, stateMachine, "Vanish");
        deadState = new DH_EnemyDeadState(this, stateMachine, "Knockdown");
        hurtState = new DH_EnemyHurtState(this, stateMachine, "Hurt");
        knockdownState = new DH_EnemyKnockDownState(this, stateMachine, "Knockdown");
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

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);
        return;
    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    public void ActivateHitbox(string hitboxName)
    {
        switch (hitboxName)
        {
            case "Primary": primaryHitbox?.SetActive(true); break;
            case "PrimaryFinal": primaryFinalHitbox?.SetActive(true); break;
            case "Uppercut": uppercutHitbox?.SetActive(true); break;
            case "DashAttack": dashAttackHitbox?.SetActive(true); break;
            case "AirAttack": airAttackHitbox?.SetActive(true); break;
        }
    }

    public void DeactivateHitbox(string hitboxName)
    {
        switch (hitboxName)
        {
            case "Primary": primaryHitbox?.SetActive(false); break;
            case "PrimaryFinal": primaryFinalHitbox?.SetActive(false); break;
            case "Uppercut": uppercutHitbox?.SetActive(false); break;
            case "DashAttack": dashAttackHitbox?.SetActive(false); break;
            case "AirAttack": airAttackHitbox?.SetActive(false); break;
        }
    }

    public bool IsGrounded() => IsGroundDetected();

    public override void TakeDamage(int damage, Vector2 hitDirection)
    {
        base.TakeDamage(damage, hitDirection);
        stateMachine.ChangeState(hurtState);
        return;
    }
}
