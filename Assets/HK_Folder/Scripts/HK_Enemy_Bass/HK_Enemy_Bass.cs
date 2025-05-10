using UnityEngine;

public class HK_Enemy_Bass : MonoBehaviour
{
    public Animator animator;
    public GameObject rapidShotPrefab;
    public Transform firePoint;

    [HideInInspector] public Transform player;
    [HideInInspector] public HK_EnemyStateMachine stateMachine;

    [Header("���� ����")]
    public int maxJumps = 1;
    [HideInInspector] public int jumpCount = 0;
    [HideInInspector] public float linearVelocityX = 0f;

    private HK_Health health;
    public float attackRange = 2.0f; // ���� ���� �߰�
    void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        stateMachine = GetComponent<HK_EnemyStateMachine>();
        health = GetComponent<HK_Health>();

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
        stateMachine.ChangeState(new HK_BassDeathState(this));
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
