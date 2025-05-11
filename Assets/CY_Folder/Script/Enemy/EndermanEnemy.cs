using UnityEngine;
using System.Collections;

public class EndermanEnemy : B_Enemy
{
    public float detectionDistance = 6f;
    public float teleportCooldown = 3f;
    private float lastTeleportTime;
    private float teleportEndTime = 0f;

    public AudioClip teleportSound;
    public AudioClip Stare;

    private Coroutine contactDamageCoroutine;
    private AudioSource localAudioSource;

    protected override void Start()
    {
        base.Start();

        // 이 오브젝트에 AudioSource가 없으면 자동으로 생성
        localAudioSource = GetComponent<AudioSource>();
        if (localAudioSource == null)
            localAudioSource = gameObject.AddComponent<AudioSource>();
    }

    protected override void RunAI()
    {
        if (isDead || player == null) return;
        if (Time.time < teleportEndTime) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        Vector2 facingDir = transform.localScale.x > 0 ? Vector2.left : Vector2.right;

        float yDistance = Mathf.Abs(player.position.y - transform.position.y);
        bool isFacingPlayer = Vector2.Dot(facingDir, directionToPlayer) > 0.5f;
        bool isBehind = Vector2.Dot(facingDir, directionToPlayer) < -0.5f;
        bool canSeePlayer = !Physics2D.Linecast(transform.position, player.position, groundLayer);

        if (isBehind && distanceToPlayer <= 3f)
        {
            FacePlayer();
            moveDirection = (int)Mathf.Sign(player.position.x - transform.position.x);
        }

        if (distanceToPlayer <= detectionDistance && isFacingPlayer && canSeePlayer && yDistance < 1f && Time.time >= lastTeleportTime + teleportCooldown)
        {
            lastTeleportTime = Time.time;
            teleportEndTime = Time.time + 1f;
            StartCoroutine(TeleportToPlayer());
        }
        else
        {
            Patrol();
            if (animator != null && !canSeePlayer)
                animator.Play("EnderMan");
        }
    }

    private IEnumerator TeleportToPlayer()
    {
        if (animator != null)
            animator.SetTrigger("Teleport");

        // Stare 재생 (시야 마주칠 때 긴 사운드)
        if (Stare != null && localAudioSource != null)
        {
            localAudioSource.clip = Stare;
            localAudioSource.Play();
        }

        yield return new WaitForSeconds(0.2f);

        Vector2 offset = (player.transform.localScale.x > 0 ? Vector2.left : Vector2.right) * 2f;
        Vector2 targetPosition = new Vector2(player.position.x + offset.x, transform.position.y);

        RaycastHit2D hit = Physics2D.Raycast(targetPosition, Vector2.down, 1f, groundLayer);
        if (hit.collider != null)
        {
            transform.position = targetPosition;

            if (teleportSound != null && localAudioSource != null)
                localAudioSource.PlayOneShot(teleportSound);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead || !other.CompareTag("Player")) return;

        B_PlayerHealth playerHealth = other.GetComponent<B_PlayerHealth>();
        if (playerHealth != null)
        {
            contactDamageCoroutine = StartCoroutine(DealContactDamage(playerHealth));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && contactDamageCoroutine != null)
        {
            StopCoroutine(contactDamageCoroutine);
            contactDamageCoroutine = null;
        }
    }

    private IEnumerator DealContactDamage(B_PlayerHealth player)
    {
        while (true)
        {
            if (player != null)
                player.TakeDamage(contactDamage, transform.position);

            yield return new WaitForSeconds(1f);
        }
    }

    protected override void Die()
    {
        // 사운드 중지
        if (localAudioSource != null && localAudioSource.isPlaying)
            localAudioSource.Stop();

        base.Die();
    }
}
