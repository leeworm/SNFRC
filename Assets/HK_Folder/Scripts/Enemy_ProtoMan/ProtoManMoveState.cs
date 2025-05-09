public class ProtoManMoveState : IEnemyState
{
    private Enemy_ProtoMan protoMan;

    public ProtoManMoveState(Enemy_ProtoMan protoMan)
    {
        this.protoMan = protoMan;
    }

    public void Enter()
    {
        protoMan.animator.SetBool("isMoving", true); // 파라미터 설정
    }

    public void Update()
    {
        protoMan.MoveTowardsPlayer();
    }

    public void Exit()
    {
        protoMan.animator.SetBool("isMoving", false); // 이동 끝나면 false로
    }

    public void AnimationFinishTrigger() { }
}

