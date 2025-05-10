using UnityEngine;

public class HK_ProtoManChargeShotState : HK_IEnemyState
{
    private HK_Enemy_ProtoMan protoMan;
    private bool hasFired = false;
    private Transform player;

    public HK_ProtoManChargeShotState(HK_Enemy_ProtoMan protoMan)
    {
        this.protoMan = protoMan;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    public void Enter()
    {
        hasFired = false;
        protoMan.animator.Play("ProtoMan_ChargeShot", 0);
    }

    public void Update()
    {
        if (!hasFired)
        {
            FireChargeShot();
            hasFired = true;
        }
    }

    public void Exit() { }

    public void AnimationFinishTrigger()
    {
        protoMan.stateMachine.ChangeState(new HK_ProtoManIdleState(protoMan));
    }

    private void FireChargeShot()
    {
        if (protoMan.chargeShotPrefab != null && protoMan.firePoint != null && player != null)
        {
            GameObject bullet = Object.Instantiate(
                protoMan.chargeShotPrefab,
                protoMan.firePoint.position,
                Quaternion.identity
            );

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = (player.position - protoMan.firePoint.position).normalized;
                rb.linearVelocity = direction * 14f; // �������� �� �� ������
            }
        }
    }
}
