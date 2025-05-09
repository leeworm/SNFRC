using UnityEngine;

public class EnemyState
{
    protected EnemyStateMachine stateMachine;
    public Enemy enemy;
    public Rigidbody2D rb;
    protected float xInput;
    protected float yInput;
    private string animBoolName;
    protected float stateTimer;
    protected bool triggerCalled = false;
    public EnemyState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName)
    {
        this.enemy = _enemy;            // 적 객체 저장
        this.stateMachine = _stateMachine; // 상태 머신 저장
        this.animBoolName = _animBoolName; // 애니메이션 트리거 변수 이름 저장
    }

    public virtual void Enter()
    {
        enemy.anim.SetBool(animBoolName, true); // 애니메이션 트리거 활성화
        rb = enemy.rb;
        triggerCalled = false;
    }

    public virtual void Update()
    {
        if (stateTimer > 0)
            stateTimer -= Time.deltaTime; // 상태 타이머 감소

        enemy.anim.SetFloat("yVelocity", rb.linearVelocity.y);

    }

    public virtual void Exit()
    {
        enemy.anim.SetBool(animBoolName, false); // 애니메이션 트리거 비활성화
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true; // 애니메이션 종료 트리거 호출
    }
}