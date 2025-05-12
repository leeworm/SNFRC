public class DH_EnemyUppercutState : DH_EnemyGroundedState
{
    public DH_EnemyUppercutState(DH_Enemy _enemy, DH_EnemyStateMachine stateMachine, string animBoolName)
        : base(_enemy, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        enemy.SetVelocity(0, rb.linearVelocity.y);
        enemy.isBusy = true;
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
        {
            stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        enemy.isBusy = false;
    }
}
