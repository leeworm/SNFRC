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
        JH_Hurtbox targetHurtbox = other.GetComponent<JH_Hurtbox>();

        if (targetHurtbox != null) // Hurtbox�� ã�Ҵٸ� (��, Ư�� ������ �¾Ҵٸ�)
        {
            // 2. Hurtbox�� ������ �ִ� ownerEntity (JH_Entity�� ������ Player �Ǵ� Enemy)�� ���� TakeDamage ȣ��
            if (targetHurtbox.ownerEntity != null)
            {
                // 3. �±� �˻�� Hurtbox�� ownerEntity�� �±׸� ������� �� �� �ֽ��ϴ�.
                if (targetHurtbox.ownerEntity.CompareTag(targetTag))
                {
                    // ownerEntity�� TakeDamage �޼ҵ� ȣ��, ���� ��Ʈ�ڽ��� damage�� ������ bodyPartType ����
                    Debug.Log($"JH_Hitbox ({gameObject.name}): ProcessHit ȣ�� �õ� -> {targetHurtbox.gameObject.name}");
                    targetHurtbox.ownerEntity.TakeDamage(this.damage, targetHurtbox.bodyPartType);

                    hitTargetsThisActivation.Add(other); // �� �ݶ��̴�(Ư�� ��Ʈ�ڽ�)�� ���� ������ ���


                }
            }
        }
        
    }
}