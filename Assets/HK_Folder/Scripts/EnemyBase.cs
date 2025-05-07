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
        // 모든 적이 공통적으로 타겟을 플레이어로 설정
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning($"{name}: Player not found in scene!");
        }
    }

    public void AnimationFinishTrigger()
    {
        stateMachine.currentState?.AnimationFinishTrigger();
    }
}
