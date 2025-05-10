using UnityEngine;

public class JH_PlayerMoveState : JH_PlayerState
{
    public JH_PlayerMoveState(JH_Player _player, JH_PlayerStateMachine _stateMachine, string _AnimBool) : base(_player, _stateMachine, _AnimBool)
    {
    }
   
    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        Player.SetVelocity(xInput * Player.MoveSpeed, rb.linearVelocityY);

        if(xInput == 0)
        {
            StateMachine.ChangeState(Player.IdleState);
        }


        if (Player.IsGroundDetected())
        {
            int attackIndex = 0; // 눌린 키에 해당하는 공격 번호 저장 변수

            // 각 키 확인
            if (Input.GetKeyDown(KeyCode.Q))
            {
                attackIndex = 1; // Q -> 공격 1
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                attackIndex = 2; // E -> 공격 2
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                attackIndex = 3; // A -> 공격 3
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                attackIndex = 4; // S -> 공격 4
            }
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                attackIndex = 5; // Z -> 공격 5
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                attackIndex = 6; // X -> 공격 6
            }

            // 만약 attackIndex가 0보다 크면 (즉, Q,E,A,S,Z,X 중 하나가 눌렸으면)
            if (attackIndex > 0)
            {
                Player.LastAttackIndex = attackIndex; // 공격 번호 저장
                Player.animator.SetTrigger("AttackTrigger");
                StateMachine.ChangeState(Player.GroundedAttackState); // 공격 상태로 전환
                return; // 상태 변경 후 종료
            }

        }
    }

    public override void Exit()
    {
        base.Exit();
    }

}
