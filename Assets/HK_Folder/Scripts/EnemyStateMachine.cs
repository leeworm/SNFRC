using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    public IEnemyState currentState { get; private set; }
    private Enemy_Bass bass;  // Enemy_Bass 객체를 참조

    void Awake()
    {
        bass = GetComponent<Enemy_Bass>();  // Enemy_Bass 컴포넌트를 가져옴
    }

    // 상태 변경 시 Enemy_Bass 객체를 상태에 전달
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

    public Enemy_Bass GetBass() => bass;  // Enemy_Bass를 반환하는 메서드 추가
}
