using UnityEngine;

public class Enemy_Bass : MonoBehaviour
{
    public Animator animator;
    public GameObject rapidShotPrefab;
    public Transform firePoint;

    [HideInInspector] public Transform player;
    [HideInInspector] public EnemyStateMachine stateMachine;

    [Header("���� ����")]
    public int maxJumps = 1;
    [HideInInspector] public int jumpCount = 0;
    [HideInInspector] public float linearVelocityX = 0f;

    private Health health;
    public float attackRange = 2.0f; // ���� ���� �߰�
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
        // Rigidbody2D�� �ִٸ� ���⿡ ����
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = velocity;
    }
}
