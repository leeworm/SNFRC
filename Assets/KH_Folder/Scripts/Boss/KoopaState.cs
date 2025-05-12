using UnityEngine;

public class KoopaState
{
    protected KoopaStateMachine stateMachine;
    protected Koopa koopa;

    protected Rigidbody2D rb;

    protected float xInput;
    protected float yInput;
    private string animBoolName;

    protected float stateTimer;
    protected bool triggerCalled;


    public KoopaState(Koopa _koopa, KoopaStateMachine _stateMachine, string _animBoolName)
    {
        this.koopa = _koopa;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        koopa.anim.SetBool(animBoolName, true);
        rb = koopa.rb;
        
        triggerCalled = false;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;

        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        
        if(koopa.healthPoint <= 0 && !koopa.IsDeath)
        {
            stateMachine.ChangeState(koopa.deathState);
            koopa.IsDeath = true;
        }
    }

    public virtual void Exit()
    {
        koopa.anim.SetBool(animBoolName, false);
    }   

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
