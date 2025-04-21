using UnityEngine;

public class Dash_Skill : MonoBehaviour
{
    public float dashDistance = 5f;
    public float dashCooldown = 1f;
    private bool canDash = true;

    // 대시가 가능한지 확인하는 메소드
    public bool CanUseSkill()
    {
        return canDash;
    }

    // 대시 실행
    public void Dash(Vector2 direction)
    {
        if (canDash)
        {
            canDash = false;
            // 대시 실행 로직 (예: 캐릭터의 위치를 대시 거리만큼 이동)
            transform.position += (Vector3)direction * dashDistance;

            // 대시 쿨타임 처리
            Invoke(nameof(ResetDash), dashCooldown);
        }
    }

    // 대시 쿨타임 초기화
    private void ResetDash()
    {
        canDash = true;
    }
}
