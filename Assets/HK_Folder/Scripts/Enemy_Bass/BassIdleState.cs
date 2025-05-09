using UnityEngine;

public class BassIdleState : IEnemyState
{
    private Enemy_Bass bass;

    public BassIdleState(Enemy_Bass bass)
    {
        this.bass = bass;
    }

    public void Enter()
    {
        bass.animator.SetBool("Idle",true);
    }

    public void Update()
    {
        // �ƹ� �ൿ ���� (AI���� �ڵ� ��ȯ��)
    }

    public void Exit() 
    {
        bass.animator.SetBool("Idle", false);
    }

    public void AnimationFinishTrigger() { }
}
