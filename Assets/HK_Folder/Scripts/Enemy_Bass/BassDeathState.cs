using UnityEngine;

public class BassDeathState : IEnemyState
{
    private Enemy_Bass bass;

    public BassDeathState(Enemy_Bass bass)
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
            // 애니메이션이 끝나면 오브젝트 삭제
            GameObject.Destroy(bass.gameObject);
        }
    }

    public void Exit() { }

    public void AnimationFinishTrigger() { }
}


