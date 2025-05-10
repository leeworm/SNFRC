using UnityEngine;

public class DH_EnemyStateMachine
{
    public DH_EnemyState currentState { get; private set; }

    public void Initialize(DH_EnemyState _startstate)
    {
        currentState = _startstate;
        currentState.Enter();
    }

    public void ChangeState(DH_EnemyState _newState)
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
