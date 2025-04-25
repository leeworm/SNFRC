using UnityEngine;

public class Turtle : KH_Enemy
{
    [SerializeField] private float spinSpeed = 8f;

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
        
    }

    protected override void FixedUpdate()
    {
        base.Move();
    }

    public void Spin(Transform playerTransform)
    {
        // 스핀 애니메이션 재생
        anim.SetBool("Spin", true);

        if(transform.position.x > playerTransform.position.x)
        {
            // 플레이어가 왼쪽에 있을 때
            Flip();
        }

        moveSpeed = spinSpeed;
    } 
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(isDeath)
                Spin(collision.gameObject.transform);
        }
    }
}
