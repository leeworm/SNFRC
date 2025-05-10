using System.Collections;
using UnityEngine;

public class DH_Enemy : DH_Entity
{
    #region Bool Variables
    public bool isBusy = false;
    public bool isAttacking = false;
    public bool isMoving = false;
    public bool isIdle = false;
    public bool isDead = false;
    public bool isGrounded = false;
    public bool isWall = false;
    public bool isDashing = false;
    public bool isSubstituting = false;
    public bool isAttackingAir = false;
    public bool isBlocking = false;
    public bool isJumping = false;
    public bool isLanding = false;
    #endregion

    #region States
    public DH_EnemyStateMachine stateMachine { get; private set; }
    public DH_EnemyState currentState { get; private set; }
    public DH_EnemyIdleState idleState { get; private set; }
    public DH_EnemyMoveState moveState { get; private set; }
    public DH_EnemyAttackState attackState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new DH_EnemyStateMachine();
        idleState = new DH_EnemyIdleState(this, stateMachine, "Idle");
        moveState = new DH_EnemyMoveState(this, stateMachine, "Move");
        attackState = new DH_EnemyAttackState(this, stateMachine, "Attack");
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);

    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }

    public void SetCurrentState(DH_EnemyState newState)
    {
        currentState = newState;
    }
    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    public void ChangeState(DH_EnemyState newState)
    {
        stateMachine.ChangeState(newState);
    }
    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;
        yield return new WaitForSeconds(_seconds);
        isBusy = false;
    }
    protected override IEnumerator HitKnockBack()
    {
        return base.HitKnockBack();
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }

    public override void Die()
    {
        base.Die();
    }

    public override void Flip()
    {
        base.Flip();
    }

    public override void FlipController(float _x)
    {
        base.FlipController(_x);
    }

    public override void SetVelocity(float _xVelocity, float _yVelocity)
    {
        base.SetVelocity(_xVelocity, _yVelocity);
    }
}
