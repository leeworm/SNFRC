using UnityEngine;

public class MushRoom : KH_Enemy
{
    [SerializeField] private Vector2 deathVelocity; // 적이 죽을 때의 속도

    protected override void Awake()
    {
        base.Awake();

    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if(isDeath)
        {
            PostDeath();
            isDeath = false;
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public void PostDeath()
    {
        boxCollider.enabled = false; // 콜라이더 비활성화
        rb.bodyType = RigidbodyType2D.Kinematic; // 물리적 상호작용 비활성화
        Destroy(gameObject, 0.4f); // 1초 후에 오브젝트 제거
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("DamageObject"))
        {
            transform.rotation = Quaternion.Euler(180, 0, 0); // 적을 반전시킴
            boxCollider.enabled = false; // 콜라이더 비활성화
            rb.linearVelocity = deathVelocity;
            rb.gravityScale = 3;
        }
    }
}
