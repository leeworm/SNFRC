﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Windows;
using static UnityEditor.PlayerSettings;

public class DH_Entity : MonoBehaviour
{
    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    //public EntityFX fx { get; private set; }
    public SpriteRenderer sr { get; private set; }
    //public CharacterStats stats { get; private set; }
    public CapsuleCollider2D cd { get; private set; }
    public BoxCollider2D bd { get; private set; }
    #endregion

    [Header("Damage info")]
    public int maxHealth = 100;
    public int currentHealth;
    public GameObject hitEffectPrefab; // 피격 이펙트 프리팹
    public GameObject blockEffectPrefab; // 방어 이펙트 프리팹
    

    [Header("Knockback info")]
    [SerializeField] protected Vector2 knockbackDirection;
    [SerializeField] protected float knockbackDuration;
    protected bool isKnocked;

    [Header("Collision info")]
    public Transform attackCheck;
    public float attackCheckRadius;

    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;

    [Header("Rigidbody2D info")]
    [HideInInspector] public float defaultGravityScale = 10f;

    public int facingDir { get; private set; } = 1; // 객체의 방향 (1: 오른쪽, -1: 왼쪽)
    public bool facingRight = true; // 객체가 오른쪽을 보고 있는지 여부

    public System.Action onFlipped;

    [HideInInspector] public float lastXVelocity;
    [HideInInspector] public float lastYVelocity;

    #region Bool Variables
    public bool isBusy = false;
    public bool isGrounded = false;
    public bool isIdle = false;
    public bool isAttacking = false;
    public bool isAttackingAir = false;
    public bool isMoving = false;
    //public bool isWall = false;
    public bool isDashing = false;
    public bool isSubstituting = false;
    public bool isJumping = false;
    public bool isLanding = false;
    public bool isBlocking = false;
    public bool isDead = false;
    public bool commandDetectorEnabled = false;
    public bool isHurting = false;
    public bool isKnockdown = false;
    #endregion
    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        //fx = GetComponentInChildren<EntityFX>();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        //stats = GetComponent<CharacterStats>();
        cd = GetComponent<CapsuleCollider2D>();
        bd = GetComponent<BoxCollider2D>();
    }

    protected virtual void Update()
    {
        isGrounded = IsGroundDetected();
    }

    //public virtual void DamageEffect()
    //{
    //    fx.StartCoroutine("FlashFX");
    //    StartCoroutine("HitKnockBack");
    //}

    protected virtual IEnumerator HitKnockBack()
    {
        isKnocked = true;
        rb.linearVelocity = new Vector2(knockbackDirection.x * -facingDir, knockbackDirection.y);
        yield return new WaitForSeconds(knockbackDuration);
        isKnocked = false;
        rb.linearVelocity = new Vector2(0, 0);
    }

    #region Collision
    public bool IsGroundDetected()
    {
        Vector2 origin = groundCheck.position;
        Vector2 size = new Vector2(0.5f, 0.1f); // 박스 크기 조절
        RaycastHit2D hit = Physics2D.BoxCast(origin, size, 0f, Vector2.down, groundCheckDistance, whatIsGround);
        return hit.collider != null;
    }

    // => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    //public bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);

    protected virtual void OnDrawGizmos()
    {
        //Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }
    #endregion

    #region Flip
    public virtual void Flip()
    {
        facingDir = facingDir * -1; // 방향 반전
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);

        onFlipped?.Invoke(); // null 체크를 포함한 안전한 호출방식
    }

    public virtual void FlipController(float _x)
    {
        if (_x == 0)
            return;

        if (_x > 0 && !facingRight)
            Flip();
        else if (_x < 0 && facingRight)
            Flip();
    }
    #endregion

    #region Speed
    public void SetZeroVelocity()
    {
        SetVelocity(0, 0);
    }

    public virtual void SetVelocity(float _xVelocity, float _yVelocity)
    {
        // Rigidbody2D의 속력 설정
        rb.linearVelocity = new Vector2(_xVelocity, _yVelocity);
        lastXVelocity = _xVelocity;
        FlipController(_xVelocity);
    }
    #endregion

    public void MakeTransparent(bool _transparent)
    {
        if (_transparent)
            sr.color = Color.clear;
        else
            sr.color = Color.white;
    }

    public virtual void Die()
    {

    }

    public virtual void TakeDamage(int damage, Vector2 hitDirection)
    {
        if (isDead || isHurting || isKnockdown)
            return;

        if (IsBlocking())
        {
            return;
        }

        currentHealth -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage.");

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
            return;
        }

        knockbackDirection = hitDirection; // 방향 저장      
        rb.linearVelocity = hitDirection;
    }

       
    
    public virtual void ApplyKnockback(Vector2 knockback)
    {
        rb.linearVelocity = knockback;
    }

    public virtual bool IsBlocking()
    {
        return isBlocking;
    }

    public virtual void ShowHitEffect(Vector3 position)
    {
        if (hitEffectPrefab != null)
            DH_EffectPoolManager.Instance.SpawnEffect("HitEffect", position);

    }

    public virtual void ShowBlockEffect(Vector3 position)
    {
        if (blockEffectPrefab != null)
            DH_EffectPoolManager.Instance.SpawnEffect("BlockEffect", position);
    }   
}
