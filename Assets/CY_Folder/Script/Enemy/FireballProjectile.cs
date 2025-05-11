using UnityEngine;

public class FireballProjectile : MonoBehaviour
{
    public int damage = 3;
    public float lifeTime = 2f;
    public GameObject hitEffect;
    

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어에 닿았을 때 데미지
        if (other.CompareTag("Player"))
        {
            B_PlayerHealth player = other.GetComponent<B_PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damage, transform.position);
            }

            Explode();
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            // 벽에 닿으면 폭발 후 사라짐
            Explode();
        }
    }

    private void Explode()
    {
        if (hitEffect != null)
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
