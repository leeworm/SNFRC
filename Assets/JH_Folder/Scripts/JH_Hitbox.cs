using System.Collections.Generic;
using UnityEngine;

public class JH_Hitbox : MonoBehaviour
{
    public float damage = 10f;
    public string targetTag = "Enemy";

    private HashSet<Collider2D> hitTargetsThisActivation; //중복타격방지
    void OnEnable()
    {
        if (hitTargetsThisActivation == null)
        {
            hitTargetsThisActivation = new HashSet<Collider2D>();
        }
        hitTargetsThisActivation.Clear(); // 활성화될 때마다 맞은 대상 목록 초기화
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 이미 이번 공격 활성화 중에 맞은 대상이면 무시
        if (hitTargetsThisActivation.Contains(other))
        {
            return;
        }

        // 설정한 타겟 태그/레이어와 일치하는지 확인
        if (other.CompareTag(targetTag)) // 또는 (targetLayer.value & (1 << other.gameObject.layer)) > 0
        {
            Debug.Log(gameObject.name + "가 " + other.name + "에게 적중!");

            // 상대방(other)에게 데미지를 주는 로직
            // 예: other.GetComponent<EnemyHealth>()?.TakeDamage(damage);
            // 또는 IDamageable 인터페이스 사용
            IDamageable damageableTarget = other.GetComponent<IDamageable>();
            if (damageableTarget != null)
            {
                damageableTarget.TakeDamage(damage);
                hitTargetsThisActivation.Add(other); // 맞은 대상 목록에 추가

                // 여기에 타격 이펙트 생성, 사운드 재생 등의 코드 추가
            }
        }
    }
}

public interface IDamageable
{
    void TakeDamage(float amount);
}
