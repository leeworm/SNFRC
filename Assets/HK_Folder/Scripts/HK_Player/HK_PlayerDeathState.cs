using UnityEngine;
using UnityEngine.SceneManagement;

public class HK_PlayerDeathState : HK_PlayerState
{
    private float deathTimer = 2f; // ��� �� ��� �ð�

    public HK_PlayerDeathState(HK_Player player, HK_PlayerStateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();

        player.anim.Play("Player_Dead");
        player.SetVelocity(0, 0);

        player.inputEnabled = false; // �Է� ���� (�ʿ� ��)
        if (player.rb) player.rb.simulated = false; // Rigidbody ���� (����ȿ�� ����)
        if (player.GetComponent<Collider2D>())
            player.GetComponent<Collider2D>().enabled = false; // �浹 ���� (���û���)
    }

    public override void Update()
    {
        base.Update();

        deathTimer -= Time.deltaTime;
        if (deathTimer <= 0f)
        {
            SceneManager.LoadScene("GameOverScene"); // GameOver �� ��ȯ (�̸� �ٲ㵵 ��)
        }
    }
}
