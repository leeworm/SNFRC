using UnityEngine;

public class MushRoom : KH_Entity
{
    [SerializeField]private float moveSpeed = 2f;

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
    }

    private void Move()
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
}
