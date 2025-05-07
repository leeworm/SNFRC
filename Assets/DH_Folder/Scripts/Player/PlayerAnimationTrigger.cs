using UnityEngine;

public class PlayerAnimationTrigger : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }
    // 공격 시 발동
    public void ActivateHitbox() => player.ActivateHitbox();
    public void DeactivateHitbox() => player.DeactivateHitbox();

    // 콤보 처리
    public void EnableComboWindow()
    {
        if (player.currentState is PlayerPrimaryAttackState attackState)
            attackState.EnableComboWindow();
    }

    public void DisableComboWindow()
    {
        if (player.currentState is PlayerPrimaryAttackState attackState)
            attackState.DisableComboWindow();
    }

    // 공격 종료
    public void OnAttackComplete()
    {
        if (player.currentState is PlayerPrimaryAttackState attackState)
            attackState.OnAttackComplete();

        //if (player.attackState is PlayerAirAttackState airAttackState)
        //  airAttackState.OnAttackComplete();
    }

}
