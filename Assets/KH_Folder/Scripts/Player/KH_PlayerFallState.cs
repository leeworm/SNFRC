using UnityEngine;

public class KH_PlayerFallState : KH_PlayerState
{
    public KH_PlayerFallState(KH_Player _player, KH_PlayerStateMachine _stateMachine, string _animBoolName) 
    : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

    }

    public override void Update()
    {
        base.Update();

        // 점프 중에 땅에 닿으면 점프 취소
        if (player.IsGroundDetected() && stateTimer <= 0)
            stateMachine.ChangeState(player.idleState);

        CheckEnemyBelow(); // 적 밟기 체크
    }

    public override void Exit()
    {
        base.Exit();
    }

    void CheckEnemyBelow()
    {
        RaycastHit2D hit = Physics2D.Raycast(player.enemyChek.position, Vector2.down, player.enemyCheckDistance, player.whatIsEnemy);
        if (hit.collider != null)
        {
            hit.collider.gameObject.GetComponent<KH_Enemy>().Death(); // 적 죽이기

            Bounce(); // 마리오 점프
        }
    }

    void Bounce()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 7f); // 위로 튕기기
    }
}
