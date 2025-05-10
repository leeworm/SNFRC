using UnityEngine;

public interface HK_IEnemyState
{
    void Enter();
    void Update();
    void Exit();

    void AnimationFinishTrigger();
}
