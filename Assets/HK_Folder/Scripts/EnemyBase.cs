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
        // 각 보스마다 오버라이드해서 첫 상태 지정
    }

    public void AnimationFinishTrigger()
    {
        stateMachine.currentState.AnimationFinishTrigger();
    }
}
