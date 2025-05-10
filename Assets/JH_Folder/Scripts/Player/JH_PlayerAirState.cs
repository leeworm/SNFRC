using UnityEngine;

public class JH_PlayerAirState : JH_PlayerState
{
    public JH_PlayerAirState(JH_Player _Player, JH_PlayerStateMachine _StateMachine, string _AnimBool) : base(_Player, _StateMachine, _AnimBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Air State Entered");
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Air State Exited");
    }

    public override void Update()
    {
        base.Update();

        if (StateMachine.CurrentState == this)
        {
            int attackIndex = 0; // 눌린 키에 해당하는 공격 번호 저장 변수

            // 각 키 확인
            if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S))
            {
                attackIndex = 1; // Q A S-> 공격 1 
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                attackIndex = 2; // E -> 공격 2
            }
            else if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X))
            {
                attackIndex = 3; // Z X -> 공격 3
            }
            
            // 만약 attackIndex가 0보다 크면 (즉, Q,E,A,S,Z,X 중 하나가 눌렸으면)
            if (attackIndex > 0)
            {
                Player.LastAttackIndex = attackIndex; // 공격 번호 저장
                Player.animator.SetTrigger("AttackTrigger");
                StateMachine.ChangeState(Player.JumpAttackState); // 공격 상태로 전환
                return; // 상태 변경 후 종료
            }

        }

        if (Player.IsGroundDetected())
            StateMachine.ChangeState(Player.IdleState);
    }
}

