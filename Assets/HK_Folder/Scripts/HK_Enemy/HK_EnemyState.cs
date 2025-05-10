using UnityEngine;

public class HK_EnemyState
{
    protected HK_EnemyStateMachine stateMachine;
    protected HK_Enemy enemyBase;
    protected Rigidbody2D rb;

    protected bool triggerCalled;
    private string animBoolName;

    protected float stateTimer;

    public HK_EnemyState(HK_Enemy _enemyBase, HK_EnemyStateMachine _stateMachine, string _animBoolName)
    {
        this.enemyBase = _enemyBase;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        triggerCalled = false;
        rb = enemyBase.rb;
        enemyBase.anim.SetBool(animBoolName, true);
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }


    public virtual void Exit()
    {
        enemyBase.anim.SetBool(animBoolName, false);
    }



    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }

}
