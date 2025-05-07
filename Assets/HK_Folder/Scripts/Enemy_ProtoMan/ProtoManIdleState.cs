public class ProtoManIdleState : IEnemyState
{
    private Enemy_ProtoMan protoMan;

    public ProtoManIdleState(Enemy_ProtoMan protoMan)
    {
        this.protoMan = protoMan;
    }

    public void Enter()
    {
        protoMan.animator.Play("Idle");
    }

    public void Update()
    {
        // �ƹ��͵� ���� ���� (AI�� �����ؼ� ���¸� �ٲ�� ��)
    }

    public void Exit() { }

    public void AnimationFinishTrigger() { }
}
