using UnityEngine;

public enum PhaseState
{
    Phase1,
    PhaseChange,
    Phase2
}

public class Koopa : KH_Enemy
{
    private SpriteRenderer sr;

    [Header("Koopa 정보")]
    public Koopa_HpBar koopaHpBar; // 체력바 스크립트
    [SerializeField] public int healthPoint = 1000; // 체력

    #region States
    public KoopaStateMachine stateMachine { get; private set; }

    public KoopaIdleState idleState { get; private set; }
    public KoopaWalkState walkState { get; private set; }

    public KoopaFireShotState fireShotState { get; private set; }
    public KoopaJumpAttackState jumpAttackState { get; private set; }
    public KoopaRoundFireState roundFireState { get; private set; }
    public KoopaAllDirFireState allDirFireState { get; private set; }

    public KoopaPhaseChangeState phaseChangeState { get; private set; }

    #endregion

    #region Phase 1
    [Header("큰 파이어볼 정보")]
    public Transform playerTransform; // 플레이어 트랜스폼
    public GameObject fireShotPrefab; // 불꽃 발사체 프리팹
    [SerializeField] private float fireShotSpeed = 10f; // 불꽃 발사 속도
    public int fireShotCount = 3;
    public float fireShotDelay = 1f;
    
    [Header("점프 공격 정보")]
    [SerializeField] private float jumpSpeed = 15f; // 점프 속도
    [SerializeField] private float jumpAttackSpeed = 25f; // 점프 속도
    
    [Header("작은 파이어볼 정보")]
    public GameObject smallFireShotPrefab; // 불꽃 발사체 프리팹
    [SerializeField]public float angleIncrement = 15f; // 각도 증가량
    [SerializeField]private float smallFireSpeed = 10f; // 작은 불꽃 발사 속도
    
    [Header("롤링 파이어볼 정보")]
    public GameObject roundFireShotPrefab; // 불꽃 발사체 프리팹
    [SerializeField]private float roundFireSpeed = 10f; // 작은 불꽃 발사 속도
    public int roundFireCount = 2;
    public float roundFireDelay = 1.5f;
    #endregion

    public PhaseState phaseState = PhaseState.Phase1; // 현재 상태

    protected override void Awake()
    {
        base.Awake();

        sr = GetComponentInChildren<SpriteRenderer>();
        
        stateMachine = new KoopaStateMachine();
        idleState = new KoopaIdleState(this, stateMachine, "Idle");
        walkState = new KoopaWalkState(this, stateMachine, "Walk");

        fireShotState = new KoopaFireShotState(this, stateMachine, "Fire");
        jumpAttackState = new KoopaJumpAttackState(this, stateMachine, "Idle");
        roundFireState = new KoopaRoundFireState(this, stateMachine, "Attack");
        allDirFireState = new KoopaAllDirFireState(this, stateMachine, "Idle");

        phaseChangeState = new KoopaPhaseChangeState(this, stateMachine, "Idle");
    }

    protected override void Start()
    {
        base.Start();
        
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        
        stateMachine.currentState.Update();
    }

    protected override void FixedUpdate()
    {
        
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("DamageObject"))
        {
            if(collision.gameObject.GetComponent<KH_Fireball>() != null)
            {
                // 파이어볼 비활성화
                KH_BulletPool.Instance.ReturnBullet(collision.gameObject);
                

                // 체력 감소
                int damage = collision.gameObject.GetComponent<KH_Fireball>().Damage;
                healthPoint -= damage;
                koopaHpBar.GetDamage(damage);
    
            }
        }
    }

    public void ShotFire()
    {
        GameObject fire = Instantiate(fireShotPrefab, wallCheck.position, Quaternion.identity);
        
        if(transform.position.x > playerTransform.position.x) // -
        {
            if(facingRight)
                Flip(); // 방향 전환

             // 왼쪽으로 공격
             fire.transform.Rotate(0, 0, 90);
             fire.GetComponent<Rigidbody2D>().linearVelocity = Vector2.left * fireShotSpeed;
        }
        else if(transform.position.x < playerTransform.position.x) // +
        {
            if(!facingRight)
                Flip(); // 방향 전환

            // 오른쪽으로 공격
            fire.transform.Rotate(0, 0, -90);
            fire.GetComponent<Rigidbody2D>().linearVelocity = Vector2.right * fireShotSpeed;
        }
    }
    #region Phase 1 Fuctions
    public void JumpUp()
    {
        Debug.Log("JumpUp");

        if(transform.position.x > playerTransform.position.x) // -
        {
            if(facingRight)
                Flip(); // 방향 전환
        }
        else if(transform.position.x < playerTransform.position.x) // +
        {
            if(!facingRight)
                Flip(); // 방향 전환
        }
        rb.linearVelocityY = jumpSpeed;
    }

    public void GoPlayerPos()
    {
        transform.position = new Vector2(playerTransform.position.x, transform.position.y);
        KH_GameManager.Instance.SetActive_DamageRange(true);
    }

    public void JumpDown()
    {
        Debug.Log("JumpDown");
        rb.linearVelocityY = -jumpAttackSpeed;
    }

    public void AllDirFire()
    {
        for (float angle = 0; angle < 360; angle += angleIncrement)
        {
            GameObject fire = Instantiate(smallFireShotPrefab, transform.position, Quaternion.identity);
            // 발사 방향 설정
            Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.left;
            fire.transform.Rotate(0, 0, angle); // 발사체 회전

            // 발사 객체에 속도 부여
            Rigidbody2D rigid = fire.GetComponent<Rigidbody2D>();
            if (rigid != null)
            {
                rigid.AddForce(direction * smallFireSpeed, ForceMode2D.Impulse);
            }
        }
        angleIncrement -= 0.5f; // 각도 증가량 증가
    }

    public void RoundFire()
    {
        Debug.Log("RoundFire");

        GameObject fire = Instantiate(roundFireShotPrefab, wallCheck.position, Quaternion.identity);
        
        if(transform.position.x > playerTransform.position.x) // -
        {
            if(facingRight)
                Flip(); // 방향 전환

             fire.GetComponent<Rigidbody2D>().linearVelocity = Vector2.left * roundFireSpeed;
        }
        else if(transform.position.x < playerTransform.position.x) // +
        {
            if(!facingRight)
                Flip(); // 방향 전환

            fire.GetComponent<Rigidbody2D>().linearVelocity = Vector2.right * roundFireSpeed;
        }
    }

    #endregion

    public void GoMidPos()
    {
        // 2페이즈 직전
        transform.position = new Vector2(0, transform.position.y);

        sr.color = Color.red;

        // 2페이즈 난이도 증가
        fireShotCount = 5;
        fireShotDelay = 0.6f;
        fireShotSpeed = 13f;
        
        smallFireSpeed = 13f;

        roundFireShotPrefab.GetComponent<KoopaRollingFire>().SetRotationSpeed(360);
        roundFireCount = 4;
        roundFireDelay = 0.75f;
        roundFireSpeed = 13f;

    }
}