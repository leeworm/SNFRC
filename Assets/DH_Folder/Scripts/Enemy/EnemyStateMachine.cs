using UnityEngine;

public class EnemyStateMachine
{
    public EnemyState currentState { get; private set; }

    public void Initialize(EnemyState _startstate)
    {
        currentState = _startstate;
        currentState.Enter();
    }

    public void ChangeState(EnemyState _newState)
    {
        currentState?.Exit();
        currentState = _newState;
        currentState.Enter();

        if (currentState.enemy != null)
        {
            currentState.enemy.SetCurrentState(currentState);
        }
    }
}
