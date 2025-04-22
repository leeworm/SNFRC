using UnityEngine;

public class EnemyStateMachine
{
    public IEnemyState currentState { get; private set; }

    public void ChangeState(IEnemyState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    private void Update()
    {
        currentState?.Update();
    }
}
