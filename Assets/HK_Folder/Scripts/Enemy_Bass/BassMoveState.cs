using UnityEngine;

public class BassMoveState : IEnemyState
{
    private Enemy_Bass bass;
    private float speed = 2.5f;

    public BassMoveState(Enemy_Bass bass)
    {
        this.bass = bass;
    }

    public void Enter()
    {
        bass.animator.SetBool("isMoving",true);
    }

    public void Update()
    {
        if (bass.player == null) return;

        Vector2 direction = (bass.player.position - bass.transform.position).normalized;
        bass.transform.position += (Vector3)(new Vector2(direction.x, 0) * speed * Time.deltaTime);
    }

    public void Exit()
    {
        
    }

    public void AnimationFinishTrigger() { }
}


