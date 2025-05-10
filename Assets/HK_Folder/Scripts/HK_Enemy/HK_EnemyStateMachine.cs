using UnityEngine;

public class HK_EnemyStateMachine : MonoBehaviour
{
    public HK_IEnemyState currentState { get; private set; }
    private HK_Enemy_Bass bass;  // Enemy_Bass ��ü�� ����

    void Awake()
    {
        bass = GetComponent<HK_Enemy_Bass>();  // Enemy_Bass ������Ʈ�� ������
    }

    // ���� ���� �� Enemy_Bass ��ü�� ���¿� ����
    public void ChangeState(HK_IEnemyState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    private void Update()
    {
        currentState?.Update();
    }

    public HK_Enemy_Bass GetBass() => bass;  // Enemy_Bass�� ��ȯ�ϴ� �޼��� �߰�
}
