using UnityEngine;

public class PlayerSubstituteState : PlayerState
{
    private readonly float offsetDistance = 1.5f;

    private Transform targetEnemy;

    public PlayerSubstituteState(Player player, PlayerStateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();

        // 적 탐색
        targetEnemy = player.GetNearestEnemy();
        if (targetEnemy == null)
        {
            Debug.Log("❌ 바꿔치기 실패: 적 없음");
            stateMachine.ChangeState(player.idleState);
            return;
        }
    }

    // 애니메이션 트리거에서 호출될 함수
    public void OnVanishAnimationEnd()
    {
        player.anim.SetBool("Substitute_Venish", false); // 바꿔치기 애니메이션 종료
        // 위치 이동
        Vector2 destination = CalculateBehindPosition(targetEnemy);
        player.transform.position = destination;

        // 플레이어가 적을 바라보게 방향 전환
        float toEnemyDir = Mathf.Sign(targetEnemy.position.x - player.transform.position.x);
        player.FlipController(toEnemyDir);

        player.anim.SetBool("Substitute_Appear", true);
    }

    public void OnAppearAnimationEnd()
    {        
        player.anim.SetBool("Substitute_Appear", false); // 바꿔치기 애니메이션 종료
        stateMachine.ChangeState(player.idleState);
        return;
    }

    private Vector2 CalculateBehindPosition(Transform enemy)
    {
        // 적의 위치와 방향에 따라 플레이어의 위치를 계산
        float dir = Mathf.Sign(enemy.position.x - player.transform.position.x);
        return new Vector2(enemy.position.x + dir * offsetDistance, player.transform.position.y);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
