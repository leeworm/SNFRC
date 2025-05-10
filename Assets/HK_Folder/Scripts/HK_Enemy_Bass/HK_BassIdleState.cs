using UnityEngine;

public class HK_BassIdleState : HK_IEnemyState
{
    private HK_Enemy_Bass bass;

    public HK_BassIdleState(HK_Enemy_Bass bass)
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
