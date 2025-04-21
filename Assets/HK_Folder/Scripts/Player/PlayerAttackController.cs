using UnityEngine;

public class PlayerAttackController : MonoBehaviour
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
    private Player player;
    private Rigidbody2D rb;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        player = GetComponent<Player>();
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

        float dir = player.facingDir;
        projectile.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(dir * shotSpeed, 0f);
        projectile.transform.localScale = new Vector3(dir, 1, 1);

        anim.SetTrigger("Attack");
    }

    void UpdateAnimParams()
    {
        bool isGrounded = player.IsGroundDetected(); // grounded 대신 함수로 호출
        bool isMoving = Mathf.Abs(rb.linearVelocityX) > 0.1f;
        bool isJumping = !isGrounded && rb.linearVelocityY > 0.1f;

        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isJumping", isJumping);
    }
}