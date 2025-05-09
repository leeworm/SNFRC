using UnityEngine;

public class BassKickState : IEnemyState
{
    private Enemy_Bass bass;
    private bool hasKicked = false;

    public BassKickState(Enemy_Bass bass)
    {
        this.bass = bass;
    }

    public void Enter()
    {
        hasKicked = false;
        bass.animator.SetTrigger("Kick");
    }

    public void Update()
    {
        if (!hasKicked)
        {
            // ���⿡ ���� ������ ó���� ��Ʈ�ڽ� Ȱ��ȭ ���� ���� �� ����
            // ��: ���� ��Ʈ�ڽ��� ��� Ȱ��ȭ�ϰų� trigger ó��

            hasKicked = true;
        }
    }

    public void Exit()
    {
        // �ʿ�� ��Ʈ�ڽ� ��Ȱ��ȭ ��
    }

    public void AnimationFinishTrigger()
    {
        bass.stateMachine.ChangeState(new BassIdleState(bass));
    }
}
