
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class B_PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 6f;

    public Transform groundCheck;
    public LayerMask groundLayer;

    public PhysicsMaterial2D noFrictionMat;
    public PhysicsMaterial2D highFrictionMat;

    public WeaponType currentWeapon = WeaponType.Sword;
    private B_PlayerStateType currentState = B_PlayerStateType.Idle;

    private Rigidbody2D rb;
    private B_PlayerAnimatorController animatorController;
    private Collider2D col;

    private bool isGrounded;
    private float jumpStartTime;
    private float minJumpDuration = 0.2f;

    public B_SkillManager skillManager;   // Inspector 연결
    public Transform firePoint;         // 스킬 발사 위치

    public Transform meleeHitbox;
    public float meleeRange = 1f;
    public LayerMask enemyLayer;
    public WeaponDatabase weaponDatabase;

    private SpriteRenderer spriteRenderer;

    private Dictionary<B_Enemy, float> lastHitTimeMap = new Dictionary<B_Enemy, float>();
    private float meleeHitCooldown = 0.3f; // 동일 적에게 다시 데미지 줄 때까지의 최소 시간
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animatorController = GetComponent<B_PlayerAnimatorController>();
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        

         isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        //  공격 중에는 이동 차단 + 멈춤
        if (currentState == B_PlayerStateType.Attack)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        // 점프 상태일 때 이동 + 공격 허용
        if (currentState == B_PlayerStateType.Jump)
            {
            float moveInput = Input.GetAxisRaw("Horizontal");
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

            if (moveInput != 0)
            {
                Vector3 scale = transform.localScale;
                scale.x = Mathf.Sign(moveInput);
                transform.localScale = scale;
            }

            // 점프 중 공격 허용
            if (Input.GetMouseButtonDown(0))
            {
                if (currentState != B_PlayerStateType.Jump)  // 점프 중에는 속도 안 끊음
                 rb.linearVelocity = Vector2.zero;
                currentState = B_PlayerStateType.Attack;
                PlayCurrentAnimation();
                return;
            }

            // 일정 시간 이후 착지 확인
            if (Time.time - jumpStartTime > minJumpDuration && isGrounded)
            {
                currentState = B_PlayerStateType.Idle;
                PlayCurrentAnimation();
            }

            return;
        }

        // 공격 중에는 입력 차단 (단, 점프 상태 예외 처리됨)
        if (currentState == B_PlayerStateType.Attack)
            return;

        // 무기 전환 (마우스 휠)
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
            currentWeapon = NextWeapon();
        else if (scroll < 0f)
            currentWeapon = PreviousWeapon();

        // 공격
        if (Input.GetMouseButtonDown(0))
        {
            currentState = B_PlayerStateType.Attack;
            PlayCurrentAnimation(); 
            return;
        }

        // 점프
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            currentState = B_PlayerStateType.Jump;
            jumpStartTime = Time.time;
            PlayCurrentAnimation();
            return;
        }

        // 웅크리기
        if (Input.GetKey(KeyCode.LeftControl))
        {
            currentState = B_PlayerStateType.Crouch;
            col.sharedMaterial = highFrictionMat;
        }
        else
        {
            float moveInput = Input.GetAxisRaw("Horizontal");
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

            if (moveInput != 0)
            {
                Vector3 scale = transform.localScale;
                scale.x = Mathf.Sign(moveInput);
                transform.localScale = scale;
            }

            currentState = (moveInput != 0) ? B_PlayerStateType.Move : B_PlayerStateType.Idle;
            col.sharedMaterial = noFrictionMat;
        }

        PlayCurrentAnimation();
    }

        public void TakeDamage(int damage)
    {
        StartCoroutine(HitFlash());
        KnockbackFromEnemy();
        // 체력 시스템 있으면 여기서 HP 깎기
    }

    private IEnumerator HitFlash()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.white;
        }
    }

    private void KnockbackFromEnemy()
    {
        if (rb == null) return;

        // 예: 적이 왼쪽이면 오른쪽으로 밀기
       Collider2D closestEnemy = Physics2D.OverlapCircle(transform.position, 1.5f, enemyLayer);
        if (closestEnemy == null) return;

        float dir = Mathf.Sign(transform.position.x - closestEnemy.transform.position.x);
        Vector2 force = new Vector2(dir * 6f, 2f); // 좌우 + 위로 살짝
        rb.AddForce(force, ForceMode2D.Impulse);
    }


    private void PlayCurrentAnimation()
    {
        animatorController.PlayAnimation(currentState, currentWeapon);
    }

    private WeaponType NextWeapon()
    {
        int next = ((int)currentWeapon + 1) % System.Enum.GetValues(typeof(WeaponType)).Length;
        return (WeaponType)next;
    }

    private WeaponType PreviousWeapon()
    {
        int prev = ((int)currentWeapon - 1 + System.Enum.GetValues(typeof(WeaponType)).Length) % System.Enum.GetValues(typeof(WeaponType)).Length;
        return (WeaponType)prev;
    }

    public void OnAttackAnimationEnd()
    {
        if (!isGrounded)
        {
            currentState = B_PlayerStateType.Jump;
        }
        else
        {
            currentState = B_PlayerStateType.Idle;
        }
        PlayCurrentAnimation();
    }

    public void OnJumpAnimationEnd()
    {

        if (isGrounded)
        {
            currentState = B_PlayerStateType.Idle;
            PlayCurrentAnimation();
        }
    }

    public void SpawnSlash()
    {
       

        if (currentWeapon == WeaponType.Shovel)
        {
            skillManager.UseSkill(currentWeapon, firePoint);
        }
    }

    public void SpawnBoomerang()
    {
        if (currentWeapon == WeaponType.Pick)
        {
            skillManager.UseSkill(currentWeapon, firePoint);
        }
    }

    public void SpawnArrow()
    {
        if (currentWeapon == WeaponType.Bow)
        {
            skillManager.UseSkill(currentWeapon, firePoint);
        }
    }

    public void DealMeleeDamage()
    {
            Vector2 attackCenter = meleeHitbox.position;
        float attackRadius = meleeRange;

        Collider2D[] hits = Physics2D.OverlapCircleAll(attackCenter, attackRadius, enemyLayer);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Hitbox")) continue;

            B_Enemy enemy = hit.GetComponent<B_Enemy>();
            if (enemy != null)
            {
                WeaponData data = weaponDatabase.GetWeaponData(currentWeapon);
                if (data != null)
                {
                    int damage = data.damage;
                    Debug.Log($"[✅ 데미지 호출 성공] {currentWeapon} 의 데미지 = {damage}");
                    enemy.TakeDamage(damage);
                    enemy.KnockbackFrom(transform.position);
                }
                else
                {
                    Debug.LogError($"[❌ 데미지 데이터 없음] {currentWeapon} 에 대한 데이터가 없음!");
                }
            }
        }
    }




}
