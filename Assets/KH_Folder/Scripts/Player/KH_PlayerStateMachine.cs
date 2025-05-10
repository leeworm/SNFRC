using UnityEngine;

public class KH_PlayerStateMachine
{
    public KH_PlayerState currentState { get; private set; }
 
    public void Initialize(KH_PlayerState _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }

    public void ChangeState(KH_PlayerState _newState)
    {
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }
}
