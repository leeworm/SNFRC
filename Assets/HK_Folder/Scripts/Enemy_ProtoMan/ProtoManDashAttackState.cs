using UnityEngine;

public class ProtoManDashAttackState : IEnemyState
{
    private Enemy_ProtoMan protoMan;

    public ProtoManDashAttackState(Enemy_ProtoMan protoMan)
    {
        this.protoMan = protoMan;
    }

    public void Enter()
    {
        // �뽬 ������ ���� �ʱ�ȭ
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
