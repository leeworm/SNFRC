using UnityEngine;

public class JH_PlayerJumpState : JH_PlayerState
{
    public JH_PlayerJumpState(JH_Player _Player, JH_PlayerStateMachine _StateMachine, string _AnimBool) : base(_Player, _StateMachine, _AnimBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Player.InitiateJumpGracePeriod();
        rb.linearVelocity = new Vector2(rb.linearVelocityX, Player.JumpForce);
    }
    public override void Exit()
    {
        base.Exit();

    }

    public override void Update()
    {
        base.Update();

        if (StateMachine.CurrentState == this)
        {
            int attackIndex = 0; // ���� Ű�� �ش��ϴ� ���� ��ȣ ���� ����

            // �� Ű Ȯ��
            if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S))
            {
                attackIndex = 1; // Q A S -> ���� 1 
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                attackIndex = 2; // E -> ���� 2
            }
            else if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X))
            {
                attackIndex = 3; // Z X -> ���� 3
            }

            // ���� attackIndex�� 0���� ũ�� (��, Q,E,A,S,Z,X �� �ϳ��� ��������)
            if (attackIndex > 0)
            {
                Player.LastAttackIndex = attackIndex; // ���� ��ȣ ����
                Player.animator.SetTrigger("AttackTrigger");
                StateMachine.ChangeState(Player.JumpAttackState); // ���� ���·� ��ȯ
                return; // ���� ���� �� ����
            }

            //if (rb.linearVelocityY < 0)
            //{
            //    StateMachine.ChangeState(Player.AirState);
            //}
            //else if (StateMachine.CurrentState == Player.JumpAttackState)
            //{
            //    return;
            //}

            if (Player.IsGroundDetected())
                StateMachine.ChangeState(Player.IdleState);
        }

        

    }
   
}
