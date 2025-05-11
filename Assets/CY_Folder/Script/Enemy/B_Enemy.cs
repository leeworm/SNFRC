
using UnityEngine;
using System.Collections;

public abstract class B_Enemy : MonoBehaviour
{
    public int maxHP = 10;
    protected int currentHP;
    protected Transform player;

    protected int moveDirection = -1; // 좌우 이동용

    [Header("공통 이동 설정")]
    public float moveSpeed = 2f;
    public Transform groundCheck;
    public Transform wallCheck;
    public LayerMask groundLayer;
    public float checkDistance = 0.2f;

    protected SpriteRenderer spriteRenderer;
    protected Rigidbody2D rb;
    protected Animator animator;

    protected bool isStunned = false;
    private float stunEndTime = 0f;
    protected bool isDead = false;
    public bool IsDead => isDead;

    private float lastTurnTime = 0f;
    private float turnCooldown = 0.5f;

    [Header("공격 설정")]
    public int contactDamage = 1;

    [Header("사운드 설정")]
    public AudioClip hitSound;
    public AudioClip deathSound;

    protected virtual void Start()
    {
        currentHP = maxHP;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (rb != null)
        {
            rb.linearDamping = 15f;
            rb.angularDamping = 999f;
            rb.freezeRotation = true;
        }
    }

    public virtual void TakeDamage(int damage)
    {
        if (isDead) return;

        if (hitSound != null)
        B_AudioManager.Instance.PlaySFX(hitSound);

        currentHP -= damage;

        if (currentHP <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(HitFlash());
            KnockbackFrom(transform.position);
        }
    }

    protected IEnumerator HitFlash()
    {
        if (spriteRenderer != null && !isDead)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.white;
        }
    }

    public void KnockbackFrom(Vector2 attackerPos, float knockPower = 6f, float stunDuration = 0.3f)
    {
        if (rb == null) return;

        float dir = Mathf.Sign(transform.position.x - attackerPos.x);
        rb.AddForce(new Vector2(dir * knockPower, 2f), ForceMode2D.Impulse);

        isStunned = true;
        stunEndTime = Time.time + stunDuration;
    }

    protected virtual void Die()
    {
        if (isDead) return;
        isDead = true;

        if (deathSound != null)
        B_AudioManager.Instance.PlaySFX(deathSound);

        StartCoroutine(DeathSequence());
        
    }

    protected void FacePlayer()
    {
        if (player == null) return;

        Vector3 scale = transform.localScale;
        if (player.position.x < transform.position.x)
            scale.x = Mathf.Abs(scale.x);  // 왼쪽
        else
            scale.x = -Mathf.Abs(scale.x); // 오른쪽

        transform.localScale = scale;
    }

    protected void Patrol()
    {
        if (groundCheck == null || wallCheck == null || rb == null) return;

        bool noGround = !Physics2D.Raycast(groundCheck.position, Vector2.down, checkDistance, groundLayer);
        bool wallHit = Physics2D.Raycast(wallCheck.position, Vector2.right * moveDirection, checkDistance, groundLayer);

        if (noGround || wallHit)
            moveDirection *= -1;

        rb.linearVelocity = new Vector2(moveDirection * moveSpeed, rb.linearVelocity.y);

        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * -moveDirection;
        transform.localScale = scale;

        if ((noGround || wallHit) && Time.time - lastTurnTime > turnCooldown)
        {
            moveDirection *= -1;
            FacePlayer();
            lastTurnTime = Time.time;
        }
    }

    protected virtual void Update()
    {
        if (isDead) return;

        if (isStunned)
        {
            if (Time.time >= stunEndTime)
                isStunned = false;
            return;
        }

        RunAI();
    }

    public void Stun(float duration)
    {
        isStunned = true;
        stunEndTime = Time.time + duration;

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    protected abstract void RunAI();

    private void OnDrawGizmosSelected()
    {
        if (wallCheck != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(wallCheck.position, wallCheck.position + Vector3.right * moveDirection * checkDistance);
        }

        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * checkDistance);
        }
    }

   protected IEnumerator DeathSequence()
    {
        Debug.Log($"[⚰️ DeathSequence 시작] {name}");

        // 붉은색 유지
        if (spriteRenderer != null)
            animator.enabled = false; // 애니메이션 중단
            spriteRenderer.color = new Color(1f, 0.2f, 0.2f); // 붉은색 고정

        // 충돌 제거
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
            col.enabled = true;
            

        // 중력 설정
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.gravityScale = 3f;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }

        // 부드럽게 쓰러지기 - 로컬 회전 Z축 90도
        Quaternion startRot = transform.rotation;
        Quaternion endRot = Quaternion.Euler(0, 0, transform.localScale.x > 0 ? -90f : 90f);
        float rotTime = 0f;
        float rotDuration = 0.4f;

        while (rotTime < rotDuration)
        {
            rotTime += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(startRot, endRot, rotTime / rotDuration);
            yield return null;
        }

        transform.rotation = endRot;

        gameObject.layer = LayerMask.NameToLayer("Item");

        yield return new WaitForSeconds(0.8f);
        Destroy(gameObject);
    }

}
