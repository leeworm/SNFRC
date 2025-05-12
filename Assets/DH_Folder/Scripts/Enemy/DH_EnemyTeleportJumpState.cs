using UnityEngine;

public class DH_EnemyTeleportJumpState : DH_EnemyAirState
{   
    private float timer;
    private Vector2 targetPosition;

    public DH_EnemyTeleportJumpState(DH_Enemy _enemy, DH_EnemyStateMachine stateMachine, string animBoolName)
        : base(_enemy, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        triggerCalled = false;
        enemy.isSubstituting = true;
        enemy.currentJumpCount--;
        Debug.Log("공중 순간이동 상태 진입");
    }

    public override void Update()
    {
        base.Update();       

        if (triggerCalled)
        {
            Debug.Log("TeleportJumpState: triggerCalled, 에어 상태로 진입");
            stateMachine.ChangeState(enemy.airState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        enemy.anim.SetBool("AirAppear", false);
        enemy.rb.gravityScale = enemy.defaultGravityScale;
        enemy.isSubstituting = false;
    }

    private AnimationClip GetCurrentAnimationClip()
    {
        AnimatorClipInfo[] clips = enemy.anim.GetCurrentAnimatorClipInfo(0);
        return clips.Length > 0 ? clips[0].clip : null;
    }

    public void OnVanishAnimationEndtoAirAppear()
    {
        Debug.Log("Vanish 애니메이션 종료, 공중 이동 및 AirAppear 시작");
        enemy.anim.SetBool("Vanish", false); // 바꿔치기 애니메이션 종료
        enemy.anim.SetBool("AirAppear", true);

        float maxHeight = enemy.transform.position.y + 5f;
        enemy.transform.position = new Vector2(enemy.transform.position.x, maxHeight);

        AnimationClip clip = GetCurrentAnimationClip();
        timer = clip.length;
        timer -= Time.deltaTime;

        enemy.rb.gravityScale = 0f;
        enemy.SetZeroVelocity();
    }
}
