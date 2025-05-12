using UnityEngine;
using static DH_CommandDetector;

public class DH_PlayerIdleState : DH_PlayerGroundedState
{
    private float commandBufferTime = 0.15f;
    private float commandBufferTimer;
    private bool bufferingInput = false;

    public DH_PlayerIdleState(DH_Player _player, DH_PlayerStateMachine _stateMachine, string _animBoolName)
        : base(_player, _stateMachine, _animBoolName) { }    

    public override void Enter()
    {
        base.Enter();
        player.isBusy = false;
        player.isIdle = true;
        player.commandDetectorEnabled = true;
        bufferingInput = false;
        commandBufferTimer = 0f;
        player.SetVelocity(0, rb.linearVelocity.y);
    }

    public override void Update()
    {
        base.Update();

        // 공격 입력 처리 확인
        if (HandleAttackInput())
        {
            Debug.Log("HandleAttackInput triggered an attack state transition.");
            return;
        }

        //if (xInput == player.facingDir) // && player.IsWallDetected())
        //{ Debug.Log("대시 막힘"); return; }

        var commandType = player.CommandDetector.CheckCommand(player.facingDir, enabled: player.commandDetectorEnabled);

        if (commandType == DashType.Forward)
        {
            stateMachine.ChangeState(new DH_PlayerDashState(player, stateMachine, "Dash", player.facingDir));
            return;
        }
        else if (commandType == DashType.Backward)
        {
            stateMachine.ChangeState(new DH_PlayerBackstepState(player, stateMachine, "Backstep", -player.facingDir));
            return;
        }

        // 방향키 입력 감지 시, 커맨드 유예 타이머 시작
        if (xInput != 0 && !player.isBusy)
        {
            if (!bufferingInput)
            {
                bufferingInput = true;
                commandBufferTimer = commandBufferTime;
                return; // 첫 프레임에는 넘어가지 않음
            }

            commandBufferTimer -= Time.deltaTime;

            if (commandBufferTimer <= 0f)
            {
                stateMachine.ChangeState(player.moveState); // 유예 시간 끝났고 Dash도 없으니 Move
                return;
            }
        }
        else
        {
            // 방향키 뗐으면 리셋
            bufferingInput = false;
        }
    }


    public override void Exit()
    {
        base.Exit();
        player.isIdle = false;
    }

}
