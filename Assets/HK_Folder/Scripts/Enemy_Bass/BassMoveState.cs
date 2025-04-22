using UnityEngine;

public class BassMoveState : IEnemyState
{
    private Enemy_Bass bass;

    public BassMoveState(Enemy_Bass bass)
    {
        this.bass = bass;
    }

    public void Enter() { }

    public void Update()
    {
        Vector2 direction = bass.player.position - bass.transform.position;
        bass.transform.position += (Vector3)direction.normalized * bass.moveSpeed * Time.deltaTime;

        if (Vector2.Distance(bass.transform.position, bass.player.position) < 4f)
        {
            bass.stateMachine.ChangeState(new BassRapidFireState(bass));
        }
    }
    public void AnimationFinishTrigger()
    {
        // 애니메이션 끝났을 때 실행할 코드
    }
    public void Exit() { }
}
