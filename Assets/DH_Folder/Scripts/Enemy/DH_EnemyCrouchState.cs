using UnityEngine;

public class DH_EnemyCrouchState : DH_EnemyGroundedState
{
    private bool substitutionWindowOpen = false;

    public DH_EnemyCrouchState(DH_Enemy _enemy, DH_EnemyStateMachine _stateMachine, string _animBoolName)
        : base(_enemy, _stateMachine, _animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        enemy.isBusy = true;

        enemy.SetVelocity(0, rb.linearVelocity.y);
        Vector2 newSize = new Vector2(enemy.originalColliderSize.x, enemy.originalColliderSize.y * 0.5f);
        enemy.col.size = newSize;

        Vector2 newOffset = new Vector2(
        enemy.originalColliderOffset.x,
        enemy.originalColliderOffset.y - (enemy.originalColliderSize.y - newSize.y) / 2f);
        enemy.col.offset = newOffset;
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            stateMachine.ChangeState(enemy.idleState);
            return;
        }

        if (Input.GetKeyDown(KeyCode.X) && substitutionWindowOpen && enemy.canSubstitute())
        {
            stateMachine.ChangeState(enemy.substituteState);
            return;
        }
    }
    public override void Exit()
    {
        base.Exit();
        enemy.isBusy = false;
        enemy.col.size = enemy.originalColliderSize;
        enemy.col.offset = enemy.originalColliderOffset;
    }

    public void OpenSubstitutionWindow()
    {
        substitutionWindowOpen = true;
    }

    public void CloseSubstitutionWindow()
    {
        substitutionWindowOpen = false;
    }

}
