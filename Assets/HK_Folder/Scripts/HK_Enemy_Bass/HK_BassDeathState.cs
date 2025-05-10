using UnityEngine;

public class HK_BassDeathState : HK_IEnemyState
{
    private HK_Enemy_Bass bass;

    public HK_BassDeathState(HK_Enemy_Bass bass)
    {
        this.bass = bass;
    }

    public void Enter()
    {
        bass.animator.SetTrigger("Die");
    }

    public void Update()
    {
        AnimatorStateInfo stateInfo = bass.animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Die") && stateInfo.normalizedTime >= 1f)
        {
            // �ִϸ��̼��� ������ ������Ʈ ����
            GameObject.Destroy(bass.gameObject);
        }
    }

    public void Exit() { }

    public void AnimationFinishTrigger() { }
}


