using UnityEngine;

public class KH_Enemy : KH_Entity
{
    [SerializeField]public float moveSpeed = 2f;


    protected BoxCollider2D boxCollider;
    protected bool isDeath = false;
    private Vector2 deathVelocity; // 적이 죽을 때의 velocity

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        
        boxCollider = GetComponent<BoxCollider2D>();

    }

    protected override void Update()
    {
        base.Update();
    }

    protected virtual void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rb.linearVelocity = new Vector2(facingDir * -moveSpeed, rb.linearVelocity.y);
        
        if (IsWallDetected())
        {
            //Debug.Log("Flip!");
            Flip();
        }

        // if (IsGroundDetected())
        // {
        //     //Debug.Log("Velocity: " + rb.linearVelocity);
        //     rb.linearVelocity = new Vector2(facingDir * -moveSpeed, rb.linearVelocity.y);
        // }
    }


    public void Death()
    {
        if (isDeath) return; // 이미 죽은 경우 함수 종료
        
        anim.SetBool("Death", true);
        moveSpeed = 0;
        isDeath = true;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("DamageObject"))
        {
            if(transform.position.y < collision.transform.position.y) // 위에 있는 플레이어 발의 의한 충돌
            {
                Death();
                collision.transform.parent.GetComponent<KH_Player>().Bounce();
            }
            else // 아래에 있는 벽돌 오브젝트에 의한 충돌
            {
                transform.rotation = Quaternion.Euler(180, 0, 0); // 적을 반전시킴
                boxCollider.enabled = false; // 콜라이더 비활성화
                rb.linearVelocity = deathVelocity;
                rb.gravityScale = 3;
            }
        }
    }
    
}
