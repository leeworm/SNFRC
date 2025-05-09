public class ProtoManIdleState : IEnemyState
{
    private Enemy_ProtoMan protoMan;

    public ProtoManIdleState(Enemy_ProtoMan protoMan)
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
        // 아무것도 하지 않음 (AI가 결정해서 상태를 바꿔야 함)
    }

    public void Exit() { }

    public void AnimationFinishTrigger() { }
}
