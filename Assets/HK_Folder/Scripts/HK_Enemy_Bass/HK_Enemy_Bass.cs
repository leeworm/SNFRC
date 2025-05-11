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
            rb.linearVelocity = velocity; // Rigidbody2D 속도 업데이트
        }
    }

    // 🎯 발사 관련 애니메이션 이벤트 함수
    public void FireRapidShot(GameObject shotPrefab)
    {
        if (shotPrefab != null && firePoint != null)
        {
            GameObject shot = Instantiate(shotPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D shotRb = shot.GetComponent<Rigidbody2D>();

            if (shotRb != null)
            {
                // 총알이 플레이어를 향해 발사
                Vector2 direction = (player.position - firePoint.position).normalized;
                shotRb.linearVelocity = direction * 12f;  // 수정: 총알 속도 설정
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

    // 플레이어의 공격을 맞았을 때 넉백 처리 및 히트 애니메이션 트리거
    public void OnHitByBullet(Vector2 bulletPosition)
    {
        if (rb != null)
        {
            // 넉백 방향 계산 (배스 위치와 불렛 위치의 반대 방향)
            Vector2 knockbackDirection = (transform.position - (Vector3)bulletPosition).normalized;

            // Rigidbody2D에 힘을 추가하여 넉백 효과
            float knockbackStrength = 5f; // 넉백 강도 조정
            rb.AddForce(knockbackDirection * knockbackStrength, ForceMode2D.Impulse);
        }

        // 'Hit' 애니메이션 트리거
        animator.SetTrigger("Hit");

        /*// 체력 감소 처리 (Health 스크립트에서 처리)
        health?.TakeDamage(10);  // 예시로 10만큼 체력 감소*/
    }
}
