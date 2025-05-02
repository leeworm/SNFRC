using UnityEngine;

public class PlayerSubstituteState : PlayerState
{
    private float substituteCooldown = 5f;
    private static float lastUsedTime = -999f;

    public PlayerSubstituteState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName) { }

    public override void Enter()
    {
        if (Time.time - lastUsedTime < substituteCooldown)
        {
            Debug.Log("❌ 바꿔치기 쿨타임 중");
            stateMachine.ChangeState(new PlayerCrouchState(player, stateMachine, "Crouch"));
            return;
        }

        Transform enemy = player.GetNearestEnemy(); //적 탐색 함수 사용

        if (enemy == null)
        {
            Debug.Log("❌ 적이 없어 바꿔치기 실패");
            stateMachine.ChangeState(new PlayerCrouchState(player, stateMachine, "Crouch"));
            return;
        }

        // 등 뒤 좌표 계산 (적 방향 반대쪽으로 이동)
        Vector2 enemyPos = enemy.position;
        float offset = 1.5f;
        int dir = enemyPos.x > player.transform.position.x ? -1 : 1;
        Vector2 targetPos = new Vector2(enemyPos.x + offset * dir, player.transform.position.y);

        // 순간이동
        player.transform.position = targetPos;

        // TODO: 이펙트 재생 / 애니메이션 넣기
        player.anim.Play("Substitute");

        lastUsedTime = Time.time;
    }

    public override void Update()
    {
        // 바로 Idle 상태로 전환
        stateMachine.ChangeState(new PlayerIdleState(player, stateMachine, "Idle"));
    }
}
