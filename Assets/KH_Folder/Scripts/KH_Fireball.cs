using UnityEngine;
using DG.Tweening;

public class KH_Fireball : KH_Entity
{
    [Header("파이어볼 정보")]
    [SerializeField]private float _xVelocity = 10f; // x축 속도
    [SerializeField]private float _yVelocity = -10f; // y축 속도
    [SerializeField]public int Damage = 5;

    private int _facingDir; // 방향 (1: 오른쪽, -1: 왼쪽)

    void OnEnable()
    {
        if (rb == null)
        {
            Debug.LogError("OnEnable : Rigidbody가 없습니다!");
        }
        SetVelocity(_xVelocity * _facingDir, _yVelocity);
    }

    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();

        SetVelocity(_xVelocity * _facingDir, _yVelocity);
    }
    protected override void Update()
    {
        if (IsWallDetected())
        {
            KH_BulletPool.Instance.ReturnBullet(gameObject);
        }
        
        if(IsGroundDetected())
        {
            SetVelocity(_xVelocity * _facingDir, _yVelocity);
            Debug.Log("fireball velocity: " + rb.linearVelocity);
        }
    }

    public void SetDirX(int dirX)
    {
        _facingDir = dirX; // 방향에 따라 속도 조정
    }

}
