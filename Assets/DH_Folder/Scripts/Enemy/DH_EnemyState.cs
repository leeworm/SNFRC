
using UnityEngine;

public class DH_EnemyState
{
    protected DH_EnemyStateMachine stateMachine;
    public DH_Enemy enemy;

    public Rigidbody2D rb;

    protected float xInput;
    protected float yInput;
    private string animBoolName;

    protected float stateTimer;
    protected bool triggerCalled = false;

    public DH_EnemyState(DH_Enemy _enemy, DH_EnemyStateMachine _stateMachine, string _animBoolName)
    {
        this.enemy = _enemy;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        enemy.anim.SetBool(animBoolName, true);
        rb = enemy.rb;
        triggerCalled = false;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;

        xInput = enemy.inputX;
        yInput = enemy.isUpInput ? 1f : (enemy.isCrouching ? -1f : 0f);

        enemy.anim.SetFloat("yVelocity", rb.linearVelocity.y);
    }

    public virtual void Exit()
    {
        enemy.anim.SetBool(animBoolName, false);
        Debug.Log($"{animBoolName} 상태 종료");
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
