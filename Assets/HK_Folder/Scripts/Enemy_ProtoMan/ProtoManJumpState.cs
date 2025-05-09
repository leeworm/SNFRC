using UnityEngine;

public class ProtoManJumpState : IEnemyState
{
    private Enemy_ProtoMan protoMan;

    public ProtoManJumpState(Enemy_ProtoMan protoMan)
    {
        this.protoMan = protoMan;
    }

    public void Enter()
    {
        protoMan.animator.Play("ProtoMan_Jump", 0);
    }

    public void Exit()
    {
        // ���� ���� �� ó��
    }

    public void Update()
    {
        // ���� ������Ʈ ����
    }

    public void AnimationFinishTrigger()
    {
        // �ִϸ��̼��� ������ �� ����Ǵ� ����
    }
}

