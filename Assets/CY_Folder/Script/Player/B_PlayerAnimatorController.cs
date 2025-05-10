using UnityEngine;

public class B_PlayerAnimatorController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayAnimation(B_PlayerStateType state, WeaponType weapon)
    {
        // 무기 이름이 None이면 생략
        string weaponName = weapon == WeaponType.None ? "" : weapon.ToString();
        
        // 상태 이름 형식: Steve_Sword_Idle, Steve_Jump 등
        string stateName = $"Steve_{weaponName}_{state}".Replace("__", "_").TrimEnd('_');

        // Idle, Move, Crouch는 Play 방식
        if (state == B_PlayerStateType.Idle || state == B_PlayerStateType.Move || state == B_PlayerStateType.Crouch)
        {
            animator.Play(stateName);
            return;
        }

        // Attack, Jump는 트리거 방식
        // 트리거 이름 형식: Attack_Sword, Jump_Pick, Jump (무기 없을 때)
        string triggerName = weapon == WeaponType.None ? $"{state}" : $"{state}_{weapon}";

        // 모든 기존 트리거 리셋
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            if (param.type == AnimatorControllerParameterType.Trigger)
                animator.ResetTrigger(param.name);
        }

        // 트리거 존재 여부 확인 후 실행
        bool hasTrigger = false;
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            if (param.name == triggerName && param.type == AnimatorControllerParameterType.Trigger)
            {
                hasTrigger = true;
                break;
            }
        }

        if (hasTrigger)
        {
            animator.SetTrigger(triggerName);
        }
        else
        {
            Debug.LogWarning($"❌ Animator에 트리거 '{triggerName}' 없음");
        }
    }
}
