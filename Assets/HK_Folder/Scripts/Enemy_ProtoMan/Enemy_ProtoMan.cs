using UnityEngine;

public class Enemy_ProtoMan : EnemyBase
{
    public GameObject chargeShotPrefab;

    protected override void Start()
    {
        base.Start();
        stateMachine.ChangeState(new ProtoManIdleState(this));
    }
}