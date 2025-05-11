using UnityEngine;

public class JH_PlayerGigongState : JH_PlayerState
{
    private bool hasShot = false;
    private AudioSource audioSource;
    public AudioClip gigongSound; // Inspector에서 기합 사운드 할당

    public JH_PlayerGigongState(JH_Player _Player, JH_PlayerStateMachine _StateMachine, string _AnimBool)
        : base(_Player, _StateMachine, _AnimBool)
    {
        // Player의 AudioSource 컴포넌트 가져오기
        audioSource = Player.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = Player.gameObject.AddComponent<AudioSource>();
        }
    }

    public override void Enter()
    {
        base.Enter();
        Player.SetVelocity(0, 0);
        hasShot = false;
        Player.animator.SetTrigger("Gigong");
    }

    public override void Exit()
    {
        base.Exit();
        hasShot = false;
    }

    public override void Update()
    {
        base.Update();

        if (!hasShot && Player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f)
        {
            shoot();
            hasShot = true;
        }
    }

    // 애니메이션 이벤트에서 호출할 메서드
    public void PlayGigongSound()
    {
        if (audioSource != null && gigongSound != null)
        {
            audioSource.PlayOneShot(gigongSound);
        }
    }

    void shoot()
    {
        if (Player.Missile != null)
        {
            // 스폰 위치는 현재 방향을 기준으로
            Vector3 spawnPosition = Player.transform.position + new Vector3(Player.facingDir, 0.5f, 0);

            // 기공포 생성 시 회전값 적용
            Quaternion rotation = Quaternion.Euler(0, Player.facingDir == -1 ? 180f : 0f, 0);
            GameObject missile = Object.Instantiate(Player.Missile, spawnPosition, rotation);

            Rigidbody2D missileRb = missile.GetComponent<Rigidbody2D>();
            if (missileRb != null)
            {
                float missileSpeed = 7f;
                missileRb.linearVelocity = new Vector2(Player.facingDir * missileSpeed, 0);
            }
        }
    }
}