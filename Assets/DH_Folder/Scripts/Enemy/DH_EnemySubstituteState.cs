using UnityEngine;

public class DH_EnemySubstituteState : DH_EnemyState
{
    private readonly float offsetDistance = 1.5f;

    private Transform targetEnemy;

    public DH_EnemySubstituteState(DH_Enemy _enemy, DH_EnemyStateMachine stateMachine, string animBoolName)
        : base(_enemy, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        enemy.isBusy = true; // 바꿔치기 상태에서 플레이어가 바쁘게 설정
        enemy.isSubstituting = true;
    }
    
    public override void Update()
    {
        base.Update();
    }

    // 애니메이션 트리거에서 호출될 함수
    public void OnVanishAnimationEndtoAppear()
    {
        enemy.anim.SetBool("Vanish", false); // 바꿔치기 애니메이션 종료

        // 적 탐색
        targetEnemy = enemy.GetNearestEnemy();
        if (targetEnemy == null)
        {
            Debug.Log("❌ 바꿔치기 실패: 적 없음");
            enemy.anim.SetBool("Appear", true);
            return;
        }
        // 위치 이동
        Vector2 destination = CalculateBehindPosition(targetEnemy);
        enemy.transform.position = destination;

        // 플레이어가 적을 바라보게 방향 전환
        float toEnemyDir = Mathf.Sign(targetEnemy.position.x - enemy.transform.position.x);
        enemy.FlipController(toEnemyDir);

        enemy.anim.SetBool("Appear", true);
    }

    public void OnAppearAnimationEnd()
    {
        Debug.Log("🟢 바꿔치기 애니메이션 종료");
        enemy.isBusy = false; // 상태 전이 전에 busy 해제
        enemy.anim.SetBool("Appear", false);
        stateMachine.ChangeState(enemy.idleState);
        return;
    }

    private Vector2 CalculateBehindPosition(Transform enemy)
    {
        // 적의 위치와 방향에 따라 플레이어의 위치를 계산
        float dir = Mathf.Sign(enemy.position.x - enemy.transform.position.x);
        return new Vector2(enemy.position.x + dir * offsetDistance, enemy.transform.position.y);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.isBusy = false; // 바꿔치기 상태 나가면 플레이어가 바쁘지 않게 설정
        enemy.isSubstituting = false;
    }

}
