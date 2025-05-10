using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    public float lifeTime = 1f; // 이펙트 지속 시간 (애니메이션 길이에 맞게 조절)

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}
