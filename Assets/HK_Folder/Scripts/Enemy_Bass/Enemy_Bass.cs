using UnityEngine;

public class Enemy_Bass : MonoBehaviour
{
    public Animator animator;
    public GameObject rapidShotPrefab;
    public Transform firePoint;

    [HideInInspector] public Transform player;
    [HideInInspector] public EnemyStateMachine stateMachine;

    [Header("점프 관련")]
    public int maxJumps = 1;
    [HideInInspector] public int jumpCount = 0;
    [HideInInspector] public float linearVelocityX = 0f;

    private Health health;
    public float attackRange = 2.0f; // 공격 범위 추가
    void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        stateMachine = GetComponent<EnemyStateMachine>();
        health = GetComponent<Health>();

        if (health != null)
        {
            health.OnDeath.AddListener(OnDeath);
        }
    }

    void Update()
    {
        stateMachine?.currentState?.Update();
    }

    void OnDeath()
    {
        stateMachine.ChangeState(new BassDeathState(this));
    }

    public void AnimationFinishTrigger()
    {
        stateMachine?.currentState?.AnimationFinishTrigger();
    }

    internal void SetVelocity(Vector2 velocity)
    {
        // Rigidbody2D가 있다면 여기에 적용
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = velocity;
    }
}
