using UnityEngine;

public class Koopa : KH_Enemy
{
    public Koopa_HpBar koopaHpBar; // 체력바 스크립트

    [Header("Koopa 정보")]
    [SerializeField] private int healthPoint = 1000; // 체력

    #region States
    public KoopaStateMachine stateMachine { get; private set; }

    public KoopaIdleState idleState { get; private set; }
    public KoopaWalkState walkState { get; private set; }

    public KoopaFireShotState fireShotState { get; private set; }
    public KoopaJumpAttackState jumpAttackState { get; private set; }
    public KoopaRoundFireState roundFireState { get; private set; }
    public KoopaAllDirFireState allDirFireState { get; private set; }

    #endregion

    public Transform playerTransform; // 플레이어 트랜스폼

    protected override void Awake()
    {
        base.Awake();
        
        stateMachine = new KoopaStateMachine();
        idleState = new KoopaIdleState(this, stateMachine, "Idle");
        walkState = new KoopaWalkState(this, stateMachine, "Walk");

        fireShotState = new KoopaFireShotState(this, stateMachine, "Fire");
        jumpAttackState = new KoopaJumpAttackState(this, stateMachine, "Idle");
        roundFireState = new KoopaRoundFireState(this, stateMachine, "Attack");
        allDirFireState = new KoopaAllDirFireState(this, stateMachine, "Idle");
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
}
