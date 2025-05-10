using UnityEngine;

public class DH_PlayerStateMachine
{
    public DH_PlayerState currentState { get; private set; }

    public void Initialize(DH_PlayerState _startstate)
    {
        currentState = _startstate;
        currentState.Enter();
    }

    public void ChangeState(DH_PlayerState _newState)
    {
        currentState?.Exit();
        currentState = _newState;
        currentState.Enter();

        if (currentState.player != null)
        {
            currentState.player.SetCurrentState(currentState);
        }
    }
}
