using UnityEngine;
using static CommandDetector;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
        : base(_player, _stateMachine, _animBoolName)
    {
    }

    private float commandBufferTime = 0.15f;
    private float commandBufferTimer;
    private bool bufferingInput = false;

    public override void Enter()
    {
        base.Enter();
        player.SetZeroVelocity();
        bufferingInput = false;
        commandBufferTimer = 0f;
    }

    public override void Update()
    {
        base.Update();

        if (xInput == player.facingDir && player.IsWallDetected())
            return;

        var dashType = player.CommandDetector.CheckDashCommand(player.facingDir);

        if (dashType == DashType.Forward)
        {
            stateMachine.ChangeState(new PlayerDashState(player, stateMachine, "Dash", player.facingDir));
            return;
        }
        else if (dashType == DashType.Backward)
        {
            stateMachine.ChangeState(new PlayerBackstepState(player, stateMachine, "Backstep", -player.facingDir));
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
    }

}
