public class HK_ProtoManIdleState : HK_IEnemyState
{
    private HK_Enemy_ProtoMan protoMan;

    public HK_ProtoManIdleState(HK_Enemy_ProtoMan protoMan)
    {
        this.protoMan = protoMan;
    }

    public void Enter()
    {
        if (protoMan.animator != null)
        {
            protoMan.animator.Play("ProtoMan_Idle",0);
        }
        
    }

    public void Update()
    {
        // �ƹ��͵� ���� ���� (AI�� �����ؼ� ���¸� �ٲ�� ��)
    }

    public void Exit() { }

    public void AnimationFinishTrigger() { }
}
