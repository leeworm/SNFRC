using UnityEngine;

public class Turtle : KH_Enemy
{
    [SerializeField] private float spinSpeed = 8f;

    public GameObject spinCol; // 스핀 콜라이더

    protected override void Awake()
    {
        base.Awake();
        
    }

    protected override void Start()
    {
        base.Start();

        spinCol.SetActive(false); // 스핀 콜라이더 비활성화
    }

    protected override void Update()
    {
        base.Update();
        
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public void Spin(Transform playerTransform)
    {
        // 스핀 애니메이션 재생
        anim.SetBool("Spin", true);

        whatIsWall = LayerMask.GetMask("Wall"); // 벽 체크만 존재
        spinCol.SetActive(true); // 스핀 콜라이더 활성화

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
            {
                Debug.Log("Spin!");
                Spin(collision.gameObject.transform);
            }
        }
    }
}
