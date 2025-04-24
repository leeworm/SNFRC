using UnityEngine;

public class Turtle : KH_Enemy
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

        if (!isDeath)
        {
            Move();
            Debug.Log("Velocity: " + rb.linearVelocity);
        }
    }
    
    public void Spin(Transform playerTransform)
    {
        // 스핀 애니메이션 재생
        animator.SetBool("Spin", true);

        moveSpeed = 10;
        FlipController(playerTransform.position.x);
        
        // isDeath를 false로 설정하여 이동 가능하게 함
        isDeath = false;
    } 
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Spin(collision.gameObject.transform);
        }
    }
}
