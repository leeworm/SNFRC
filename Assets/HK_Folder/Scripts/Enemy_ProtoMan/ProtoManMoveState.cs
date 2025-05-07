public class ProtoManMoveState : IEnemyState
{
    private Enemy_ProtoMan protoMan;

    public ProtoManMoveState(Enemy_ProtoMan protoMan)
    {
        this.protoMan = protoMan;
    }

    public void Enter()
    {
        protoMan.animator.Play("Move");
    }

    public void Update()
    {
        protoMan.MoveTowardsPlayer();
    }

    public void Exit() { }

    public void AnimationFinishTrigger() { }
}
