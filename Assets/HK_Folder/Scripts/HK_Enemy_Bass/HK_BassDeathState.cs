using UnityEngine;

public class HK_BassDeathState : HK_IEnemyState
{
    private HK_Enemy_Bass bass;
    private float timer = 0f;
    private bool isDestroyed = false;
    private float deathDuration = 2.0f; // Die 애니메이션 길이에 맞게 조정

    public HK_BassDeathState(HK_Enemy_Bass bass)
    {
        this.bass = bass;
    }

    public void Enter()
    {
        bass.animator.SetTrigger("Die");

        // 충돌 및 물리 제거
        Collider2D col = bass.GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        Rigidbody2D rb = bass.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        // ✅ 아이템 드랍
        if (bass.errorCodeItemPrefab != null)
        {
            GameObject.Instantiate(bass.errorCodeItemPrefab, bass.transform.position, Quaternion.identity);
        }
    }


    public void Update()
    {
        if (isDestroyed) return;

        timer += Time.deltaTime;
        if (timer >= deathDuration)
        {
            isDestroyed = true;
            Object.Destroy(bass.gameObject);
        }
    }

    public void Exit() { }

    public void AnimationFinishTrigger()
    {
        // 대체로 사용 안 해도 됨, 이벤트 기반일 때만 사용
    }
}
