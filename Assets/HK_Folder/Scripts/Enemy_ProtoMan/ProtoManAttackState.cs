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
        // 애니메이션 먼저 재생
        protoMan.animator.Play("ProtoMan_Attack1");

        // 발사체 생성
        GameObject shot = GameObject.Instantiate(protoMan.chargeShotPrefab, protoMan.firePoint.position, Quaternion.identity);
        Vector2 direction = protoMan.player.position - protoMan.firePoint.position;
        shot.GetComponent<Rigidbody2D>().linearVelocity = direction.normalized * 10f;

        // 상태 전환은 애니메이션 끝날 때! (AnimationFinishTrigger 에서)
    }

    public void Update() { }

    public void Exit() { }

    public void AnimationFinishTrigger()
    {
        protoMan.stateMachine.ChangeState(new ProtoManIdleState(protoMan));
    }
}