using UnityEngine;

public class DH_PlayerAirState : DH_PlayerState
{
    private float currentXVelocity;

    public DH_PlayerAirState(DH_Player _player, DH_PlayerStateMachine _stateMachine, string _animBoolName)
        : base(_player, _stateMachine, _animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        // 1. X속도 깔끔히 정리: 일정 이하로 작으면 0으로 고정
        float rawX = rb.linearVelocity.x;
        float fixedX = Mathf.Abs(rawX) < 0.05f ? 0f : rawX;
        currentXVelocity = fixedX;
        //Debug.Log($"💥 AirState 진입 시점 - rb.linearVelocity.x: {rb.linearVelocity.x}, currentXVelocity: {currentXVelocity}");
        if (Mathf.Abs(currentXVelocity) > 0.1f)
            player.FlipController(currentXVelocity);
        player.SetVelocity(currentXVelocity, rb.linearVelocityY);
        player.commandDetectorEnabled = false;
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (player.isBlocking
                || player.isAttackingAir
                || player.isSubstituting
                || player.isBusy)
                return;
            
            stateMachine.ChangeState(player.airAttackState);
            return;
        }

        if (Input.GetKeyDown(KeyCode.X)
            && player.currentJumpCount > 0 
            && !player.isBlocking
            && !player.isSubstituting)
        {
            Debug.Log("에어에서 점프로 전이");
            stateMachine.ChangeState(player.jumpState);
            return;
        }

        if (Input.GetKey(KeyCode.S) || player.isBlocking)
        {
            stateMachine.ChangeState(player.airDefenseState);
            return;
        }

        if (player.isGrounded)
        {
            if (player.isSubstituting)
                return;
            stateMachine.ChangeState(player.landState);
            return;
        }

        // 방향키 입력 체크 후 수평 속도 적용
        if (xInput != 0)
        {
            // 방향키 누르고 있으면 이전 속도 유지
            player.SetVelocity(currentXVelocity, rb.linearVelocity.y);
        }
        else
        {
            // 입력 없으면 뚝 멈추기 (부동소수점 오차 방지)
            player.SetVelocity(0f, rb.linearVelocity.y);
        }


        player.SetVelocity(currentXVelocity, rb.linearVelocityY);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
