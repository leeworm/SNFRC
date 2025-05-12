using UnityEngine;

public class DH_PlayerSubstituteState : DH_PlayerState
{
    private readonly float offsetDistance = 1.5f;

    private Transform targetEnemy;

    public DH_PlayerSubstituteState(DH_Player player, DH_PlayerStateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        player.isBusy = true; // 바꿔치기 상태에서 플레이어가 바쁘게 설정
        player.isSubstituting = true;
    }
    
    public override void Update()
    {
        base.Update();
    }

    // 애니메이션 트리거에서 호출될 함수
    public void OnVanishAnimationEndtoAppear()
    {
        player.anim.SetBool("Vanish", false); // 바꿔치기 애니메이션 종료

        // 적 탐색
        targetEnemy = player.GetNearestEnemy();
        if (targetEnemy == null)
        {
            Debug.Log("❌ 바꿔치기 실패: 적 없음");
            player.anim.SetBool("Appear", true);
            return;
        }
        // 위치 이동
        Vector2 destination = CalculateBehindPosition(targetEnemy);
        player.transform.position = destination;

        // 플레이어가 적을 바라보게 방향 전환
        float toEnemyDir = Mathf.Sign(targetEnemy.position.x - player.transform.position.x);
        player.FlipController(toEnemyDir);

        player.anim.SetBool("Appear", true);
    }

    public void OnAppearAnimationEnd()
    {
        Debug.Log("🟢 바꿔치기 애니메이션 종료");
        player.isBusy = false; // 상태 전이 전에 busy 해제
        player.anim.SetBool("Appear", false);
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
        player.isBusy = false; // 바꿔치기 상태 나가면 플레이어가 바쁘지 않게 설정
        player.isSubstituting = false;
    }

}
