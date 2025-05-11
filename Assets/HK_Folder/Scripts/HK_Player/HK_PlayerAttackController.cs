using UnityEngine;

public class HK_PlayerAttackController : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject basicShotPrefab;
    public GameObject chargeShotLv2Prefab;
    public GameObject chargeShotLv3Prefab;
    public Transform firePoint;

    [Header("Charge Timing")]
    public float chargeLevel2Time = 0.5f;
    public float chargeLevel3Time = 1.2f;

    [Header("Stats")]
    public float shotSpeed = 10f;

    private float holdTime;
    private bool isCharging;

    private Animator anim;
    private HK_Player player;
    private Rigidbody2D rb;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        player = GetComponent<HK_Player>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleChargeAttack();
        UpdateAnimParams();
    }

    void HandleChargeAttack()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            holdTime = 0;
            isCharging = true;
        }

        if (Input.GetKey(KeyCode.X) && isCharging)
        {
            holdTime += Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.X) && isCharging)
        {
            FireBasedOnCharge(holdTime);
            isCharging = false;
        }
    }

    void FireBasedOnCharge(float timeHeld)
    {
        GameObject projectile = null;

        if (timeHeld < chargeLevel2Time)
            projectile = Instantiate(basicShotPrefab, firePoint.position, Quaternion.identity);
        else if (timeHeld < chargeLevel3Time)
            projectile = Instantiate(chargeShotLv2Prefab, firePoint.position, Quaternion.identity);
        else
            projectile = Instantiate(chargeShotLv3Prefab, firePoint.position, Quaternion.identity);

        float dir = player.facingDir; // -1 또는 1
        Vector2 direction = new Vector2(dir, 0);

        // 총알의 이동 방향 설정
        HK_BulletController bullet = projectile.GetComponent<HK_BulletController>();
        if (bullet != null)
            bullet.SetDirection(direction);

        // 총알의 스케일도 방향에 맞게 반전
        Vector3 scale = projectile.transform.localScale;
        scale.x = Mathf.Abs(scale.x) * dir;
        projectile.transform.localScale = scale;

        anim.SetTrigger("Attack");
    }



    void UpdateAnimParams()
    {
        bool isGrounded = player.IsGroundDetected();
        bool isMoving = Mathf.Abs(rb.linearVelocityX) > 0.1f;
        bool isJumping = !isGrounded && rb.linearVelocityY > 0.1f;

        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isJumping", isJumping);
    }
}
