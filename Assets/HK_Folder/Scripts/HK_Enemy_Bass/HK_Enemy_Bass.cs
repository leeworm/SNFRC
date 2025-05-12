using UnityEngine;

public class HK_Enemy_Bass : MonoBehaviour
{
    [Header("Components")]
    public Animator animator;
    public Transform firePoint;

    [Header("Bullet Prefabs")]
    public GameObject rapidShotPrefab;     // RapidFire1 용
    public GameObject rapidShotPrefab2;    // RapidFire2 용

    [Header("Settings")]
    public int maxJumps = 2;
    public float attackRange = 2.0f;
    public float moveSpeed = 2.5f;

    [HideInInspector] public int jumpCount = 0;
    [HideInInspector] public float linearVelocityX = 0f;

    [HideInInspector] public Transform player;
    [HideInInspector] public HK_EnemyStateMachine stateMachine;

    private HK_Health health;
    private Rigidbody2D rb;
    public Vector2 moveDirection;

    public GameObject errorCodeItemPrefab; // 에러코드 아이템 프리팹

    void Awake()
    {
        // 초기화
        animator = animator ?? GetComponent<Animator>();
        stateMachine = GetComponent<HK_EnemyStateMachine>();
        health = GetComponent<HK_Health>();
        rb = GetComponent<Rigidbody2D>();

        // 건강 상태의 변화에 대한 리스너 추가
        if (health != null)
        {
            health.OnDeath.AddListener(OnDeath);
        }
    }

    void Update()
    {
        // 상태 머신 업데이트
        stateMachine?.currentState?.Update();
    }

    // 발사 관련 애니메이션 이벤트 함수
    public void FireRapidShot(GameObject shotPrefab)
    {
        if (shotPrefab != null && firePoint != null)
        {
            GameObject shot = Instantiate(shotPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D shotRb = shot.GetComponent<Rigidbody2D>();

            if (shotRb != null)
            {
                // 배스의 방향에 맞게 발사 방향 설정
                Vector2 direction = transform.localScale.x > 0 ? Vector2.left : Vector2.right; // 배스의 방향에 맞춰 설정
                shotRb.linearVelocity = direction * 12f;  // 총알 속도 설정

                // 배스가 왼쪽을 바라볼 때 180도 회전, 오른쪽을 바라볼 때 0도 회전
                float angle = transform.localScale.x < 0 ? 180f : 0f;
                shot.transform.rotation = Quaternion.Euler(0, 0, angle);  // 총알 회전 적용
            }

            Destroy(shot, 2f); // 총알이 2초 후 삭제
        }
    }

    // RapidShot1 발사
    public void FireRapidShot1()
    {
        FireRapidShot(rapidShotPrefab);
    }

    // RapidShot2 발사
    public void FireRapidShot2()
    {
        FireRapidShot(rapidShotPrefab2);
    }

    // 사망 처리
    void OnDeath()
    {
        stateMachine.ChangeState(new HK_BassDeathState(this));
    }

    public void AnimationFinishTrigger()
    {
        stateMachine?.currentState?.AnimationFinishTrigger();
    }

    public void SetVelocity(Vector2 velocity)
    {
        if (rb != null)
        {
            rb.linearVelocity = velocity; // 수정: Rigidbody2D 속도 업데이트
        }
    }
}
