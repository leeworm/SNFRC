using UnityEngine;

public class Test_Move : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float moveSpeed = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(-moveSpeed, rb.linearVelocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.name);
        
        // 충돌 시 속도 유지
        rb.linearVelocity = new Vector2(-moveSpeed, rb.linearVelocity.y);
    }
}
