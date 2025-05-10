using UnityEngine;

public class KoopaStateMachine
{
    public KoopaState currentState { get; private set; }
 
    public void Initialize(KoopaState _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }

    public void ChangeState(KoopaState _newState)
    {
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }
}
