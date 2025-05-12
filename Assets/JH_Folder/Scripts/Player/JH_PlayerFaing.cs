using UnityEngine;

public class JH_PlayerFaing : MonoBehaviour
{
    public Transform opponentTransform; // ���� �� ����. Inspector���� �Ҵ��ϰų� �ڵ�� ã���ϴ�.
    private JH_Entity entityScript;     // �÷��̾� �ڽ��� JH_Entity ��ũ��Ʈ

    // (���� ����) ���� ��ȯ �ΰ��� ������ �Ӱ谪
    private float facingThreshold = 0.05f;

    void Start()
    {
        entityScript = GetComponentInParent<JH_Entity>();
        if (entityScript == null)
        {
            Debug.LogError("JH_Entity ��ũ��Ʈ�� ã�� �� �����ϴ�!", gameObject);
            enabled = false; // JH_Entity ���̴� �� ������ �ǹ� ����
            return;
        }

        // opponentTransform�� Inspector���� �Ҵ���� �ʾҴٸ�,
        // "Enemy" �Ǵ� "Player2" �±� ������ ������ ã�� �Ҵ��� �� �ֽ��ϴ�.
        if (opponentTransform == null)
        {
            // ����: ������ "Enemy" �±׸� ������ ���� ���
            GameObject opponentObject = GameObject.FindGameObjectWithTag("Enemy");
            if (opponentObject != null)
            {
                opponentTransform = opponentObject.transform;
            }
            
        }
    }

    void Update()
    {
        AutoFaceOpponent();
    }

    void AutoFaceOpponent()
    {
        if (opponentTransform == null || entityScript == null)
            return;

        float direction = opponentTransform.position.x - transform.position.x;

        // ���� �Ÿ� �̻� ���̰� ���� ���� ��ȯ (������ ����)
        if (Mathf.Abs(direction) > facingThreshold)
        {
            int facing = direction > 0 ? 1 : -1;
            entityScript.SetFacingDirection(facing);
        }
    }
}
