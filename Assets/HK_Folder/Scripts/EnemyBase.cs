using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [Header("Components")]
    public Animator animator;
    public Transform player;
    public EnemyStateMachine stateMachine { get; protected set; }

    [Header("Common Settings")]
    public float moveSpeed = 2f;
    public Transform firePoint;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        stateMachine = GetComponent<EnemyStateMachine>();
    }

    protected virtual void Start()
    {
        // �� �������� �������̵��ؼ� ù ���� ����
    }

    public void AnimationFinishTrigger()
    {
        stateMachine.currentState.AnimationFinishTrigger();
    }
}
