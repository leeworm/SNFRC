using UnityEngine;

public class JH_PlayerCrouchState : JH_PlayerState
{
    public JH_PlayerCrouchState(JH_Player _Player, JH_PlayerStateMachine _StateMachine, string _AnimBool) : base(_Player, _StateMachine, _AnimBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (yInput >= 0)
        {
            StateMachine.ChangeState(Player.IdleState);
            return;
        }
        if (StateMachine.CurrentState == this)
        {
            int attackIndex = 0; // ���� Ű�� �ش��ϴ� ���� ��ȣ ���� ����

            // �� Ű Ȯ��
            if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S))
            {
                attackIndex = 1; // Q -> ���� 1
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                attackIndex = 2; // E -> ���� 2
            }
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                attackIndex = 3; // Z -> ���� 5
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                attackIndex = 4; // X -> ���� 6
            }

            // ���� attackIndex�� 0���� ũ�� (��, Q,E,A,S,Z,X �� �ϳ��� ��������)
            if (attackIndex > 0)
            {
                Player.LastAttackIndex = attackIndex; // ���� ��ȣ ����
                Player.animator.SetTrigger("AttackTrigger");
                StateMachine.ChangeState(Player.CrouchAttackState); // ���� ���·� ��ȯ
                return; // ���� ���� �� ����
            }

        }


    }
}
