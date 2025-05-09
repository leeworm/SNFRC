public class ProtoManMoveState : IEnemyState
{
    private Enemy_ProtoMan protoMan;

    public ProtoManMoveState(Enemy_ProtoMan protoMan)
    {
        this.protoMan = protoMan;
    }

    public void Enter()
    {
        protoMan.animator.SetBool("isMoving", true); // �Ķ���� ����
    }

    public void Update()
    {
        protoMan.MoveTowardsPlayer();
    }

    public void Exit()
    {
        protoMan.animator.SetBool("isMoving", false); // �̵� ������ false��
    }

    public void AnimationFinishTrigger() { }
}

