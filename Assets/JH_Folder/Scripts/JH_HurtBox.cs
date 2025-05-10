using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    // JH_Entity Ŭ���� ���ο� ���ǵ� BodyPart �������� ����ϹǷ� Ÿ���� JH_Entity.BodyPart�� ����մϴ�.
    public JH_Entity.BodyPart bodyPartType = JH_Entity.BodyPart.Body; // �⺻���� Body�� ���� (None�� enum�� �����Ƿ�)
    public JH_Entity ownerEntity; // �� ��Ʈ�ڽ��� ���� ���� Entity ��ũ��Ʈ (Player �Ǵ� Enemy)

    void Awake()
    {
        // ownerEntity�� �ν����Ϳ��� �Ҵ���� �ʾҴٸ�, �θ� ������Ʈ���� JH_Entity�� ã���ϴ�.
        if (ownerEntity == null)
        {
            ownerEntity = GetComponentInParent<JH_Entity>();
        }

        if (ownerEntity == null)
        {
            Debug.LogError(gameObject.name + " ���� �θ� JH_Entity�� ã�� �� �����ϴ�! Hurtbox�� �ùٸ��� �۵����� ���� �� �ֽ��ϴ�.");
            enabled = false; // ownerEntity�� ������ �� ��ũ��Ʈ�� �۵����� �ʵ��� ��Ȱ��ȭ
        }
    }

    // �������� JH_Hitbox ��ũ��Ʈ�� �� ��Ʈ�ڽ��� �浹���� �� ȣ���ϴ� �޼ҵ��Դϴ�.
    // �� �޼ҵ�� �������κ��� ���� ������ ����, �� ��Ʈ�ڽ��� � ���������� ownerEntity���� �����մϴ�.
    public void ProcessHit(float damageAmount)
    {
        if (ownerEntity != null && !ownerEntity.isKnocked) // ownerEntity�� �����ϰ�, KO ���°� �ƴ� ���� ������ ó��
        {
            // ownerEntity�� TakeDamage �޼ҵ带 ȣ���Ͽ�
            // �����ڷκ��� ���� ������(damageAmount)�� �� ��Ʈ�ڽ��� ���� ����(this.bodyPartType)�� �����մϴ�.
            ownerEntity.TakeDamage(damageAmount, this.bodyPartType);
        }
        else if (ownerEntity == null)
        {
            Debug.LogError(gameObject.name + "�� Hurtbox�� ownerEntity�� ������� �ʾҽ��ϴ�.");
        }
    }
}