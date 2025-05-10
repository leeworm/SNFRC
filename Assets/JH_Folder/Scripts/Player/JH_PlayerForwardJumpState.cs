using UnityEngine;

public class JH_PlayerForwardJumpState : JH_PlayerState
{
    private float initialXVelocity;
    private bool Jumped;

    public JH_PlayerForwardJumpState(JH_Player _Player, JH_PlayerStateMachine _StateMachine, string _AnimBool)
        : base(_Player, _StateMachine, _AnimBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        // ���� �Է� �������� �ʱ� �ӵ� ����
        initialXVelocity = Player.MoveSpeed * Player.facingDir;

        Player.InitiateJumpGracePeriod();

        // ���� ���� ����
        rb.linearVelocity = new Vector2(initialXVelocity, Player.JumpForce);
        Jumped = false;
      
    }

    public override void Update()
    {
        base.Update();

        rb.linearVelocity = new Vector2(initialXVelocity, rb.linearVelocity.y);

        if (!Jumped && rb.linearVelocity.y > 0.01f)
        {
            Jumped = true;
        }

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
                StateMachine.ChangeState(Player.FBJumpAttackState); // ���� ���·� ��ȯ
                return; // ���� ���� �� ����
            }

            if (Player.IsGroundDetected() && rb.linearVelocity.y <= 0.05f)
                StateMachine.ChangeState(Player.IdleState);



            //if (Jumped && rb.linearVelocity.y <= 0.01f)
            //{
            //    StateMachine.ChangeState(Player.AirState);
            //}
        }
    }

    public override void Exit()
    {
        base.Exit();

    }
}
