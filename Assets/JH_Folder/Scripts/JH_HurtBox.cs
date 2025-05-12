using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    // JH_Entity 클래스 내부에 정의된 BodyPart 열거형을 사용하므로 타입을 JH_Entity.BodyPart로 명시합니다.
    public JH_Entity.BodyPart bodyPartType = JH_Entity.BodyPart.Body; // 기본값을 Body로 설정 (None이 enum에 없으므로)
    public JH_Entity ownerEntity; // 이 허트박스가 속한 메인 Entity 스크립트 (Player 또는 Enemy)

    void Awake()
    {
        // ownerEntity가 인스펙터에서 할당되지 않았다면, 부모 오브젝트에서 JH_Entity를 찾습니다.
        if (ownerEntity == null)
        {
            ownerEntity = GetComponentInParent<JH_Entity>();
        }

        if (ownerEntity == null)
        {
            Debug.LogError(gameObject.name + " 에서 부모 JH_Entity를 찾을 수 없습니다! Hurtbox가 올바르게 작동하지 않을 수 있습니다.");
            enabled = false; // ownerEntity가 없으면 이 스크립트는 작동하지 않도록 비활성화
        }
    }

    // 공격자의 JH_Hitbox 스크립트가 이 허트박스와 충돌했을 때 호출하는 메소드입니다.
    // 이 메소드는 공격으로부터 받은 데미지 값과, 이 허트박스가 어떤 부위인지를 ownerEntity에게 전달합니다.
    public void ProcessHit(float damageAmount)
    {
        if (ownerEntity != null && !ownerEntity.isKnocked) // ownerEntity가 존재하고, KO 상태가 아닐 때만 데미지 처리
        {
            // ownerEntity의 TakeDamage 메소드를 호출하여
            // 공격자로부터 받은 데미지(damageAmount)와 이 허트박스의 부위 정보(this.bodyPartType)를 전달합니다.
            ownerEntity.TakeDamage(damageAmount, this.bodyPartType);
        }
        else if (ownerEntity == null)
        {
            Debug.LogError(gameObject.name + "의 Hurtbox에 ownerEntity가 연결되지 않았습니다.");
        }
    }
}