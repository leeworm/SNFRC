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
            int attackIndex = 0; // ���� Ű�� �ش��ϴ� ���� ��ȣ ���� ����

            // �� Ű Ȯ��
            if (Input.GetKeyDown(KeyCode.Q))
            {
                attackIndex = 1; // Q -> ���� 1
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                attackIndex = 2; // E -> ���� 2
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                attackIndex = 3; // A -> ���� 3
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                attackIndex = 4; // S -> ���� 4
            }
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                attackIndex = 5; // Z -> ���� 5
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                attackIndex = 6; // X -> ���� 6
            }

            // ���� attackIndex�� 0���� ũ�� (��, Q,E,A,S,Z,X �� �ϳ��� ��������)
            if (attackIndex > 0)
            {
                Player.LastAttackIndex = attackIndex; // ���� ��ȣ ����
                Player.animator.SetTrigger("AttackTrigger");
                StateMachine.ChangeState(Player.GroundedAttackState); // ���� ���·� ��ȯ
                return; // ���� ���� �� ����
            }

        }
    }

    public override void Exit()
    {
        base.Exit();
    }

}
