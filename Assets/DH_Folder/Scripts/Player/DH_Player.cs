﻿using System.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class DH_Player : DH_Entity
{
    public Vector2 lastKnockback;

    //[Header("Skill Effects")]
    [HideInInspector]  public GameObject blackFadePrefab;
    [HideInInspector]  public GameObject skillEffectPrefab;

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



    #region States
    public DH_PlayerStateMachine stateMachine { get; private set; }
    public DH_PlayerState currentState { get; private set; }
    public DH_PlayerIdleState idleState { get; private set; }
    public DH_PlayerMoveState moveState { get; private set; }
    public DH_PlayerJumpState jumpState { get; private set; }
    public DH_PlayerTeleportJumpState teleportJumpState { get; private set; }
    public DH_PlayerAirState airState { get; private set; }
    public DH_PlayerLandState landState { get; private set; }
    public DH_PlayerDashState dashState { get; private set; }
    public DH_PlayerDashAttackState dashAttackState { get; private set; }
    public DH_PlayerBackstepState backstepState { get; private set; }
    public DH_PlayerPrimaryAttackState primaryAttack { get; private set; }
    public DH_PlayerAirAttackState airAttackState { get; private set; }
    public DH_PlayerUppercutState uppercutState { get; private set; }
    public DH_PlayerDefenseState defenseState { get; private set; }
    public DH_PlayerAirDefenseState airDefenseState { get; private set; }
    public DH_PlayerSexyJutsuState SexyJutsuState { get; private set; }
    public DH_PlayerCrouchState crouchState { get; private set; }
    public DH_PlayerSubstituteState substituteState { get; private set; }
    public DH_CommandDetector CommandDetector { get; private set; }
    public DH_PlayerDeadState deadState { get; private set; }
    public DH_PlayerHurtState hurtState { get; private set; }
    public DH_PlayerKnockdownState knockdownState { get; private set; }

    #endregion


    protected override void Awake()
    {
        base.Awake();
        // 상태 머신 인스턴스 생성
        stateMachine = new DH_PlayerStateMachine();
        CommandDetector = new DH_CommandDetector();

        // 각 상태 인스턴스 생성 (this: 플레이어 객체, stateMachine: 상태 머신, "Idle"/"Move": 상태 이름)
        idleState = new DH_PlayerIdleState(this, stateMachine, "Idle");
        moveState = new DH_PlayerMoveState(this, stateMachine, "Move");
        jumpState = new DH_PlayerJumpState(this, stateMachine, "Jump", lastXVelocity);
        teleportJumpState = new DH_PlayerTeleportJumpState(this, stateMachine, "Vanish");
        airState = new DH_PlayerAirState(this, stateMachine, "Jump");
        landState = new DH_PlayerLandState(this, stateMachine, "Land");
        dashState = new DH_PlayerDashState(this, stateMachine, "Dash", facingDir);
        dashAttackState = new DH_PlayerDashAttackState(this, stateMachine, "DashAttack");
        backstepState = new DH_PlayerBackstepState(this, stateMachine, "Backstep", facingDir);
        primaryAttack = new DH_PlayerPrimaryAttackState(this, stateMachine, "Attack");
        uppercutState = new DH_PlayerUppercutState(this, stateMachine, "Uppercut");
        airAttackState = new DH_PlayerAirAttackState(this, stateMachine, "AirAttack");
        defenseState = new DH_PlayerDefenseState(this, stateMachine, "Block");
        airDefenseState = new DH_PlayerAirDefenseState(this, stateMachine, "AirBlock");
        SexyJutsuState = new DH_PlayerSexyJutsuState(this, stateMachine, "Skill1");
        crouchState = new DH_PlayerCrouchState(this, stateMachine, "Crouch");
        substituteState = new DH_PlayerSubstituteState(this, stateMachine, "Vanish");
        deadState = new DH_PlayerDeadState(this, stateMachine, "Die");
        hurtState = new DH_PlayerHurtState(this, stateMachine, "Hurt");
        knockdownState = new DH_PlayerKnockdownState(this, stateMachine, "Knockdown");
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
    public void SetCurrentState(DH_PlayerState state)
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
        if (stateMachine.currentState is DH_PlayerBackstepState)
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

    public override void TakeDamage(int damage, Vector2 hitDirection)
    {
        base.TakeDamage(damage, hitDirection);
        lastKnockback = hitDirection;
        stateMachine.ChangeState(hurtState);
        return;
    }
}
