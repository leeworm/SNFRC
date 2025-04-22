using UnityEngine;

public class ProtoManMoveState : IEnemyState
{
    private Enemy_ProtoMan protoMan;

    public ProtoManMoveState(Enemy_ProtoMan protoMan)
    {
        this.protoMan = protoMan;
    }

    public void Enter() { }

    public void Update()
    {
        Vector2 direction = protoMan.player.position - protoMan.transform.position;
        protoMan.transform.position += (Vector3)direction.normalized * protoMan.moveSpeed * Time.deltaTime;

        if (Vector2.Distance(protoMan.player.position, protoMan.transform.position) < 3f)
        {
            protoMan.stateMachine.ChangeState(new ProtoManAttackState(protoMan));
        }
    }

    public void Exit() { }
    public void AnimationFinishTrigger()
    {
        // �ִϸ��̼� ������ �� ������ �ڵ�
    }
}
