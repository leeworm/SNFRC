using UnityEngine;

public class HK_EnemyBase : MonoBehaviour
{
    [Header("Components")]
    public Animator animator;
    public Transform player;
    public HK_EnemyStateMachine stateMachine { get; protected set; }

    [Header("Common Settings")]
    public float moveSpeed = 2f;
    public Transform firePoint;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        stateMachine = GetComponent<HK_EnemyStateMachine>();
    }

    protected virtual void Start()
    {
        // ��� ���� ���������� Ÿ���� �÷��̾�� ����
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
