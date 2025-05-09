using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathState : PlayerState
{
    private float deathTimer = 2f; // 사망 후 대기 시간

    public PlayerDeathState(Player player, PlayerStateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();

        player.anim.Play("Player_Dead");
        player.SetVelocity(0, 0);

        player.inputEnabled = false; // 입력 차단 (필요 시)
        if (player.rb) player.rb.simulated = false; // Rigidbody 정지 (물리효과 제거)
        if (player.GetComponent<Collider2D>())
            player.GetComponent<Collider2D>().enabled = false; // 충돌 제거 (선택사항)
    }

    public override void Update()
    {
        base.Update();

        deathTimer -= Time.deltaTime;
        if (deathTimer <= 0f)
        {
            SceneManager.LoadScene("GameOverScene"); // GameOver 씬 전환 (이름 바꿔도 됨)
        }
    }
}
