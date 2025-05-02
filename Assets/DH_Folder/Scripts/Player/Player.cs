using System.Collections;
using UnityEngine;

public class Player : Entity
{
    public PlayerStateMachine stateMachine;
    // 플레이어의 상태 (대기 상태, 이동 상태)
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    //public PlayerWallSlideState wallSlide { get; private set; }
    //public PlayerWallJumpState wallJump { get; private set; }
    //public PlayerPrimaryAttackState primaryAttack { get; private set; }
    //public PlayerCounterAttackState counterAttack { get; private set; }
    //public PlayerAimSwordState aimSword { get; private set; }
    //public PlayerCatchSwordState catchSword { get; private set; }
    //public PlayerBlackholeState blackHole { get; private set; }
    //public PlayerDeadState deadState { get; private set; }

    public DashCommandDetector dashCommandDetector;

    public int currentJumpCount;
    public int maxJumpCount = 2;
    public bool isBusy { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        stateMachine = GetComponent<PlayerStateMachine>();
        dashCommandDetector = new DashCommandDetector();
    }

    protected override void Start()
    {
        base.Start();
        currentJumpCount = maxJumpCount;
        stateMachine.Initialize(new PlayerIdleState(this, stateMachine));
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.CurrentState?.Update();
    }
    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;
        yield return new WaitForSeconds(_seconds);
        isBusy = false;
    }
    public Transform GetNearestEnemy()
    {
        float range = 5f;
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
}
