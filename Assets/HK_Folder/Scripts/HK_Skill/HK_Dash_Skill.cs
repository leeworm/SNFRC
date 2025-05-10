using UnityEngine;

public class HK_Dash_Skill : MonoBehaviour
{
    public float dashDistance = 5f;
    public float dashCooldown = 1f;
    private bool canDash = true;

    // ��ð� �������� Ȯ���ϴ� �޼ҵ�
    public bool CanUseSkill()
    {
        return canDash;
    }

    // ��� ����
    public void Dash(Vector2 direction)
    {
        if (canDash)
        {
            canDash = false;
            // ��� ���� ���� (��: ĳ������ ��ġ�� ��� �Ÿ���ŭ �̵�)
            transform.position += (Vector3)direction * dashDistance;

            // ��� ��Ÿ�� ó��
            Invoke(nameof(ResetDash), dashCooldown);
        }
    }

    // ��� ��Ÿ�� �ʱ�ȭ
    private void ResetDash()
    {
        canDash = true;
    }
}
