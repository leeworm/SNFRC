using UnityEngine;

public class BassDashState : IEnemyState
{
    private Enemy_Bass bass;
    private float dashSpeed = 8f;
    private float dashDuration = 0.4f;
    private float dashTimer;
    private bool hasDashed;

    public BassDashState(Enemy_Bass bass)
    {
        this.bass = bass;
    }

    public void Enter()
    {
        dashTimer = dashDuration;
        hasDashed = false;
        bass.animator.Play("Bass_Dash");
    }

    public void Update()
    {
        if (bass.player == null) return;

        dashTimer -= Time.deltaTime;

        if (!hasDashed)
        {
            Vector2 direction = (bass.player.position - bass.transform.position).normalized;
            bass.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(direction.x * dashSpeed, 0);
            hasDashed = true;
        }

        if (dashTimer <= 0)
        {
            bass.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            bass.stateMachine.ChangeState(new BassIdleState(bass));
        }
    }

    public void Exit()
    {
        bass.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
    }

    public void AnimationFinishTrigger() { }
}

