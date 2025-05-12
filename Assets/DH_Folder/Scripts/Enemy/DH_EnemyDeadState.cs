using System.Collections;
using UnityEngine;

public class DH_EnemyDeadState : DH_EnemyState
{
    public DH_EnemyDeadState(DH_Enemy enemy, DH_EnemyStateMachine stateMachine, string animBoolName)
        : base(enemy, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();

        enemy.SetZeroVelocity();
        enemy.isDead = true;

        // 아이템 드랍
        if (enemy.dropItemPrefab != null)
        {
            Object.Instantiate(enemy.dropItemPrefab, enemy.dropSpawnPoint.position, Quaternion.identity);
        }

        enemy.StartCoroutine(DestroyAfterDelay(2f)); // 2초 후 제거
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Object.Destroy(enemy.gameObject);
    }
}
