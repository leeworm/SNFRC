using UnityEngine;

public class JH_PlayerStateMachine
{
  public JH_PlayerState CurrentState { get; private set; }

    public void Initialize(JH_PlayerState _StartState)
    {
        CurrentState = _StartState;
        CurrentState.Enter();
    }

    public void ChangeState(JH_PlayerState _NewState)
    {
        CurrentState.Exit();
        CurrentState = _NewState;
        CurrentState.Enter();
    }   
}
