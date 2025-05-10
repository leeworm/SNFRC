public class HK_PlayerStateMachine
{
    public HK_PlayerState currentState { get; private set; }

    //�ʱ�ȭ
    public void Initialize(HK_PlayerState _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }

    public void ChangeState(HK_PlayerState _newState)
    {
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }

}