using System.Collections.Generic;
using UnityEngine;

public class JH_Hitbox : MonoBehaviour
{
    public int damage = 10; // int 타입 데미지
    public string targetTag = "Enemy"; // 공격 대상 태그

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

        // 1. 충돌한 'other' 오브젝트에서 Hurtbox 컴포넌트를 찾습니다.
        JH_Hurtbox targetHurtbox = other.GetComponent<JH_Hurtbox>();

        if (targetHurtbox != null) // Hurtbox를 찾았다면 (즉, 특정 부위에 맞았다면)
        {
            // 2. Hurtbox가 가지고 있는 ownerEntity (JH_Entity를 구현한 Player 또는 Enemy)를 통해 TakeDamage 호출
            if (targetHurtbox.ownerEntity != null)
            {
                // 3. 태그 검사는 Hurtbox의 ownerEntity의 태그를 대상으로 할 수 있습니다.
                if (targetHurtbox.ownerEntity.CompareTag(targetTag))
                {
                    // ownerEntity의 TakeDamage 메소드 호출, 현재 히트박스의 damage와 감지된 bodyPartType 전달
                    Debug.Log($"JH_Hitbox ({gameObject.name}): ProcessHit 호출 시도 -> {targetHurtbox.gameObject.name}");
                    targetHurtbox.ownerEntity.TakeDamage(this.damage, targetHurtbox.bodyPartType);

                    hitTargetsThisActivation.Add(other); // 이 콜라이더(특정 허트박스)를 맞춘 것으로 기록


                }
            }
        }
        
    }
}