using UnityEngine;

public class HK_ProtoManJumpState : HK_IEnemyState
{
    private HK_Enemy_ProtoMan protoMan;

    public HK_ProtoManJumpState(HK_Enemy_ProtoMan protoMan)
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

