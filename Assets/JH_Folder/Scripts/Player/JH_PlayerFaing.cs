using UnityEngine;

public class JH_PlayerFaing : MonoBehaviour
{
    public Transform opponentTransform; // ���� �� ����. Inspector���� �Ҵ��ϰų� �ڵ�� ã���ϴ�.
    private JH_Entity entityScript;     // �÷��̾� �ڽ��� JH_Entity ��ũ��Ʈ

    // (���� ����) ���� ��ȯ �ΰ��� ������ �Ӱ谪
    private float facingThreshold = 0.05f;

    void Start()
    {
        entityScript = GetComponent<JH_Entity>();
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
        // ������ ���ų�, entityScript�� ���ų�, �÷��̾ KO �����̸� �������� ����
        if (opponentTransform == null || entityScript == null || entityScript.isKnocked)
        {
            return;
        }

        // 1. ������� ���� �Ÿ� ���
        float distanceToOpponentX = opponentTransform.position.x - transform.position.x;

        // 2. ���ο� �ٶ� ���� ���� (�⺻���� ���� ����)
        int newFacingDirection = entityScript.facingDir;

        if (distanceToOpponentX > facingThreshold) // ������ �����ʿ� �ִٸ�
        {
            newFacingDirection = 1; // ������ (facingDir = 1)
        }
        else if (distanceToOpponentX < -facingThreshold) // ������ ���ʿ� �ִٸ�
        {
            newFacingDirection = -1; // ���� (facingDir = -1)
        }

        // 3. ������ ������ �ٲ�� �� ���� SetFacingDirection ȣ��
        if (newFacingDirection != entityScript.facingDir)
        {
            entityScript.SetFacingDirection(newFacingDirection);
        }
    }
}
