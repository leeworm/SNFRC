using System.Collections;
using UnityEngine;

public class KH_Entity : MonoBehaviour
{
    public enum WallCheckDirection
    {
        Left,
        Right
    }

    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    #endregion
    
    protected bool isKnocked = false;

    [Header("총돌 정보")]
    [SerializeField] protected Transform groundChek;
    [SerializeField] protected float groundCheckDistance = 0.2f;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance = 0.2f;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected LayerMask whatIsWall;
    
    [SerializeField] private WallCheckDirection wallCheckDir;
    private Vector2 wallCheckDirVector => wallCheckDir == WallCheckDirection.Left ? Vector2.left : Vector2.right;

    
    public int facingDir { get; private set; } = 1;
    protected bool facingRight = true;

    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    protected virtual void Start()
    {
        
    }
    protected virtual void Update()
    {
        
    }

    #region 충돌
    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundChek.position, Vector2.down, groundCheckDistance, whatIsGround);
    
    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, wallCheckDirVector * facingDir, wallCheckDistance, whatIsWall);

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundChek.position, new Vector3(groundChek.position.x, groundChek.position.y - groundCheckDistance));

        // 벽 체크 방향에 따른 끝점 계산
        Vector3 wallCheckEnd = wallCheck.position + (Vector3)(wallCheckDirVector * wallCheckDistance);

        // 벽 체크 라인 그리기
        Gizmos.DrawLine(wallCheck.position, wallCheckEnd);
    }
    #endregion

    #region 플립
    public virtual void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }


    public virtual void FlipController(float _x)
    {
        if (_x > 0 && !facingRight)
            Flip();
        else if (_x< 0 && facingRight)
            Flip();

    }
    #endregion
    
    #region 속력
    public void SetZeroVelocity() => rb.linearVelocity = new Vector2(0, 0);

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        if (isKnocked) return;

        rb.linearVelocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }
    #endregion
}
