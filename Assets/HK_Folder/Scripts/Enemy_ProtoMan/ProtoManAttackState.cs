using UnityEngine;


public class ProtoManAttackState : IEnemyState
{
    private Enemy_ProtoMan protoMan;

    public ProtoManAttackState(Enemy_ProtoMan protoMan)
    {
        this.protoMan = protoMan;
    }

    public void Enter()
    {
        // �ִϸ��̼� ���� ���
        protoMan.animator.Play("ProtoMan_Attack1");

        // �߻�ü ����
        GameObject shot = GameObject.Instantiate(protoMan.chargeShotPrefab, protoMan.firePoint.position, Quaternion.identity);
        Vector2 direction = protoMan.player.position - protoMan.firePoint.position;
        shot.GetComponent<Rigidbody2D>().linearVelocity = direction.normalized * 10f;

        // ���� ��ȯ�� �ִϸ��̼� ���� ��! (AnimationFinishTrigger ����)
    }

    public void Update() { }

    public void Exit() { }

    public void AnimationFinishTrigger()
    {
        protoMan.stateMachine.ChangeState(new ProtoManIdleState(protoMan));
    }
}