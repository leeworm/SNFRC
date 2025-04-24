using UnityEngine;

public class KH_Enemy : KH_Entity
{
    protected Animator animator;
    protected BoxCollider2D boxCollider;
    [SerializeField]protected float moveSpeed = 2f;

    protected bool isDeath = false;

    protected override void Awake()
    {
        base.Awake();

    }

    protected override void Start()
    {
        base.Start();
        
        animator = GetComponentInChildren<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected override void Update()
    {
        base.Update();
    }
    protected void Move()
    {
        if (IsWallDetected())
        {
            Flip();
        }

        if (IsGroundDetected())
        {
            rb.linearVelocity = new Vector2(facingDir * -moveSpeed, rb.linearVelocity.y);
        }
    }

    public void Death()
    {
        if (isDeath) return; // 이미 죽은 경우 함수 종료
        
        anim.SetBool("Death", true);
        rb.linearVelocity = Vector2.zero; // 속도 초기화
        moveSpeed = 0;
        isDeath = true;
    }
}
