using UnityEngine;

public interface IEnemyState
{
    void Enter();
    void Update();
    void Exit();

    void AnimationFinishTrigger();
}
