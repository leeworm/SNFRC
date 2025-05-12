using UnityEngine;

public class DH_PlayerTeleportJumpState : DH_PlayerAirState
{   
    private float timer;
    private Vector2 targetPosition;

    public DH_PlayerTeleportJumpState(DH_Player player, DH_PlayerStateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        triggerCalled = false;
        player.isSubstituting = true;
        player.currentJumpCount--;
        Debug.Log("공중 순간이동 상태 진입");
    }

    public override void Update()
    {
        base.Update();       

        if (triggerCalled)
        {
            Debug.Log("TeleportJumpState: triggerCalled, 에어 상태로 진입");
            stateMachine.ChangeState(player.airState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.anim.SetBool("AirAppear", false);
        player.rb.gravityScale = player.defaultGravityScale;
        player.isSubstituting = false;
    }

    private AnimationClip GetCurrentAnimationClip()
    {
        AnimatorClipInfo[] clips = player.anim.GetCurrentAnimatorClipInfo(0);
        return clips.Length > 0 ? clips[0].clip : null;
    }

    public void OnVanishAnimationEndtoAirAppear()
    {
        Debug.Log("Vanish 애니메이션 종료, 공중 이동 및 AirAppear 시작");
        player.anim.SetBool("Vanish", false); // 바꿔치기 애니메이션 종료
        player.anim.SetBool("AirAppear", true);

        float maxHeight = player.transform.position.y + 5f;
        player.transform.position = new Vector2(player.transform.position.x, maxHeight);

        AnimationClip clip = GetCurrentAnimationClip();
        timer = clip.length;
        timer -= Time.deltaTime;

        player.rb.gravityScale = 0f;
        player.SetZeroVelocity();
    }
}
