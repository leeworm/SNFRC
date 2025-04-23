using UnityEngine;

public class Enemy_Bass : EnemyBase
{
    public GameObject rapidShotPrefab;

    [HideInInspector] public int jumpCount = 0;
    public int maxJumps = 2;

    protected override void Start()
    {
        base.Start();
        moveSpeed = 3.5f; // 필요 시 별도 세팅
        stateMachine.ChangeState(new BassIdleState(this));
    }
}
