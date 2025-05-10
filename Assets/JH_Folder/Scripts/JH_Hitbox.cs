using System.Collections.Generic;
using UnityEngine;

public class JH_Hitbox : MonoBehaviour
{
    public int damage = 10; // int Ÿ�� ������
    public string targetTag = "Enemy"; // ���� ��� �±�

    private HashSet<Collider2D> hitTargetsThisActivation;

    void OnEnable()
    {
        if (hitTargetsThisActivation == null)
        {
            hitTargetsThisActivation = new HashSet<Collider2D>();
        }
        hitTargetsThisActivation.Clear();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (hitTargetsThisActivation.Contains(other))
        {
            return;
        }

        // 1. �浹�� 'other' ������Ʈ���� Hurtbox ������Ʈ�� ã���ϴ�.
        Hurtbox targetHurtbox = other.GetComponent<Hurtbox>();

        if (targetHurtbox != null) // Hurtbox�� ã�Ҵٸ� (��, Ư�� ������ �¾Ҵٸ�)
        {
            // 2. Hurtbox�� ������ �ִ� ownerEntity (JH_Entity�� ������ Player �Ǵ� Enemy)�� ���� TakeDamage ȣ��
            if (targetHurtbox.ownerEntity != null)
            {
                // 3. �±� �˻�� Hurtbox�� ownerEntity�� �±׸� ������� �� �� �ֽ��ϴ�.
                if (targetHurtbox.ownerEntity.CompareTag(targetTag))
                {
                    Debug.Log(gameObject.name + "�� " + other.name + " (" + targetHurtbox.bodyPartType + ") ���� ����!");

                    // ownerEntity�� TakeDamage �޼ҵ� ȣ��, ���� ��Ʈ�ڽ��� damage�� ������ bodyPartType ����
                    targetHurtbox.ownerEntity.TakeDamage(this.damage, targetHurtbox.bodyPartType);

                    hitTargetsThisActivation.Add(other); // �� �ݶ��̴�(Ư�� ��Ʈ�ڽ�)�� ���� ������ ���

                    // ���⿡ Ÿ�� ����Ʈ ����, ���� ��� ���� �ڵ� �߰�
                }
            }
        }
        
    }
}