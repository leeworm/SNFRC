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

    [Header("Sound")]
    public AudioClip fireClipBasic;
    public AudioClip fireClipChargeLv2;
    public AudioClip fireClipChargeLv3;
    public AudioClip chargeLoopClip;

    [Header("Sound Settings")]
    public float fireVolume = 0.6f;
    public float chargeVolume = 0.3f;

    private float holdTime;
    private bool isCharging;

    private Animator anim;
    private HK_Player player;
    private Rigidbody2D rb;

    private AudioSource sfxSource;         // 효과음 전용
    private AudioSource chargeLoopSource;  // 루프 사운드 전용

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        player = GetComponent<HK_Player>();
        rb = GetComponent<Rigidbody2D>();

        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.volume = fireVolume;

        chargeLoopSource = gameObject.AddComponent<AudioSource>();
        chargeLoopSource.loop = true;
        chargeLoopSource.playOnAwake = false;
        chargeLoopSource.volume = chargeVolume;


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

            // 차지 사운드 시작
            if (chargeLoopClip != null)
            {
                chargeLoopSource.clip = chargeLoopClip;
                chargeLoopSource.Play();
            }
        }

        if (Input.GetKey(KeyCode.X) && isCharging)
        {
            holdTime += Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.X) && isCharging)
        {
            FireBasedOnCharge(holdTime);
            isCharging = false;

            // 차지 사운드 정지
            if (chargeLoopSource.isPlaying)
            {
                chargeLoopSource.Stop();
                chargeLoopSource.clip = null;
            }
        }
    }

    void FireBasedOnCharge(float timeHeld)
    {
        GameObject projectile = null;
        AudioClip selectedClip = null;

        if (timeHeld < chargeLevel2Time)
        {
            projectile = Instantiate(basicShotPrefab, firePoint.position, Quaternion.identity);
            selectedClip = fireClipBasic;
        }
        else if (timeHeld < chargeLevel3Time)
        {
            projectile = Instantiate(chargeShotLv2Prefab, firePoint.position, Quaternion.identity);
            selectedClip = fireClipChargeLv2;
        }
        else
        {
            projectile = Instantiate(chargeShotLv3Prefab, firePoint.position, Quaternion.identity);
            selectedClip = fireClipChargeLv3;
        }

        float dir = player.facingDir;
        Vector2 direction = new Vector2(dir, 0);

        HK_BulletController bullet = projectile.GetComponent<HK_BulletController>();
        if (bullet != null)
            bullet.SetDirection(direction);

        Vector3 scale = projectile.transform.localScale;
        scale.x = Mathf.Abs(scale.x) * dir;
        projectile.transform.localScale = scale;

        anim.SetTrigger("Attack");

        // 발사 사운드 재생
        if (selectedClip != null)
            sfxSource.PlayOneShot(selectedClip);
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
