using UnityEngine;

public class JH_Gigongpo : MonoBehaviour
{
    public float speed = 5f;    //미사일 속도
    public float lifeTime = 1.1f; //미사일 생존 시간
    private Vector2 direction;  //미사일 이동 방향
    private bool hasHit = false; // 충돌 여부를 체크하는 플래그

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    void Update()
    {
        if (!hasHit) // 충돌하지 않았을 때만 이동
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 이미 충돌했다면 무시
        if (hasHit) return;

        JH_Hurtbox hurtBox = other.GetComponent<JH_Hurtbox>();

        if (hurtBox != null)
        {
            if (hurtBox.ownerEntity != null &&
                (hurtBox.ownerEntity.CompareTag("Enemy") || hurtBox.ownerEntity.CompareTag("Player")))
            {
                hasHit = true; // 충돌 플래그 설정


                // 기공포 제거
                Destroy(gameObject);
            }
        }
    }
}