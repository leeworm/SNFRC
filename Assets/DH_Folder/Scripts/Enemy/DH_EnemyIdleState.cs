using UnityEngine;
using static DH_CommandDetector;

public class DH_EnemyIdleState : DH_EnemyGroundedState
{
    private float commandBufferTime = 0.15f;
    private float commandBufferTimer;
    private bool bufferingInput = false;

    public DH_EnemyIdleState(DH_Enemy _enemy, DH_EnemyStateMachine _stateMachine, string _animBoolName)
        : base(_enemy, _stateMachine, _animBoolName) { }    

    public override void Enter()
    {
        base.Enter();
        enemy.isBusy = false;
        enemy.isIdle = true;
        enemy.commandDetectorEnabled = true;
        bufferingInput = false;
        commandBufferTimer = 0f;
        enemy.SetVelocity(0, rb.linearVelocity.y);
    }

    public override void Update()
    {
        base.Update();

        //if (xInput == enemy.facingDir) // && enemy.IsWallDetected())
        //{ Debug.Log("대시 막힘"); return; }

        var commandType = enemy.CommandDetector.CheckCommand(enemy.facingDir, enabled: enemy.commandDetectorEnabled);

        if (commandType == DashType.Forward)
        {
            stateMachine.ChangeState(new DH_EnemyDashState(enemy, stateMachine, "Dash", enemy.facingDir));
            return;
        }
        else if (commandType == DashType.Backward)
        {
            stateMachine.ChangeState(new DH_EnemyBackstepState(enemy, stateMachine, "Backstep", -enemy.facingDir));
            return;
        }

        // 방향키 입력 감지 시, 커맨드 유예 타이머 시작
        if (xInput != 0 && !enemy.isBusy)
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
                stateMachine.ChangeState(enemy.moveState); // 유예 시간 끝났고 Dash도 없으니 Move
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
        enemy.isIdle = false;
    }

}
