using UnityEngine;

public class MushRoom : KH_Enemy
{
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

        Move();

        if(isDeath)
        {
            PostDeath();
            isDeath = false;
        }
    }

    public void PostDeath()
    {
        boxCollider.enabled = false; // 콜라이더 비활성화
        rb.bodyType = RigidbodyType2D.Kinematic; // 물리적 상호작용 비활성화
        Destroy(gameObject, 0.4f); // 1초 후에 오브젝트 제거
    }
}
