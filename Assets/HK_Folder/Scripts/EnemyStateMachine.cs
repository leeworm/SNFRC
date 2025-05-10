using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    public IEnemyState currentState { get; private set; }
    private Enemy_Bass bass;  // Enemy_Bass ��ü�� ����

    void Awake()
    {
        bass = GetComponent<Enemy_Bass>();  // Enemy_Bass ������Ʈ�� ������
    }

    // ���� ���� �� Enemy_Bass ��ü�� ���¿� ����
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

    public Enemy_Bass GetBass() => bass;  // Enemy_Bass�� ��ȯ�ϴ� �޼��� �߰�
}
