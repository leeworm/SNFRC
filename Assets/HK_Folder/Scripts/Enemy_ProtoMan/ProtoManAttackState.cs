using UnityEngine;

public class ProtoManAttackState : IEnemyState
{
    private Enemy_ProtoMan protoMan;
    private bool hasShot = false;

    public ProtoManAttackState(Enemy_ProtoMan protoMan)
    {
        this.protoMan = protoMan;
    }

    public void Enter()
    {
        hasShot = false;
        protoMan.animator.Play("Attack1");
    }

    public void Update()
    {
        // �ִϸ��̼� �̺�Ʈ�� �߻��ϰų� ���ǿ� ���� �� ���� ����
        if (!hasShot)
        {
            Shoot();
            hasShot = true;
        }
    }

    public void Exit() { }

    public void AnimationFinishTrigger()
    {
        protoMan.stateMachine.ChangeState(new ProtoManIdleState(protoMan));
    }

    private void Shoot()
    {
        if (protoMan.chargeShotPrefab != null && protoMan.firePoint != null)
        {
            GameObject bullet = Object.Instantiate(protoMan.chargeShotPrefab, protoMan.firePoint.position, Quaternion.identity);
            Vector2 dir = (protoMan.player.position - protoMan.firePoint.position).normalized;
            bullet.GetComponent<Rigidbody2D>().linearVelocity = dir * 10f;
        }
    }
}
