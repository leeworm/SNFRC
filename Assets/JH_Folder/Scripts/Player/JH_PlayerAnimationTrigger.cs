using UnityEngine;

public class JH_PlayerAnimationTrigger : MonoBehaviour
{
    public JH_Player mainPlayerScript;

    void Awake()
    {
        // ���� Inspector���� �Ҵ����� �ʾҴٸ� �θ𿡼� ã�ƺ��ϴ�.
        if (mainPlayerScript == null)
        {
            mainPlayerScript = GetComponentInParent<JH_Player>();
        }

        if (mainPlayerScript == null)
        {
            Debug.LogError("PlayerAnimationEventReceiver: �θ𿡼� JH_Player ��ũ��Ʈ�� ã�� �� �����ϴ�!", this.gameObject);
        }
    }

    // �ִϸ��̼� �̺�Ʈ���� ȣ��� public �Լ�
    public void AnimationTrigger()
    {
        if (mainPlayerScript != null)
        {
            // ���� ������ mainPlayerScript�� �Լ��� ȣ���Ͽ� ó��
            mainPlayerScript.AnimationTrigger();
            // Debug.Log("Animation Event Received by Receiver Script"); // ����׿�
        }
        else
        {
            Debug.LogError("AnimationTrigger ȣ��Ǿ����� mainPlayerScript�� �����ϴ�!", this.gameObject);
        }
    }
}
