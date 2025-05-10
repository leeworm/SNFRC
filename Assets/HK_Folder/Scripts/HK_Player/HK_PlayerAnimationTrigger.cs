using UnityEngine;

public class HK_PlayerAnimationTrigger : MonoBehaviour
{
    private HK_Player player => GetComponentInParent<HK_Player>();

    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);


        foreach (var hit in colliders)
        {
            if (hit.GetComponent<HK_Enemy>() != null)
                hit.GetComponent<HK_Enemy>().Damage();
        }


    }


  

}


