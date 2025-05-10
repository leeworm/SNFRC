using UnityEngine;

public class SlashProjectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 2f;
    private float direction = 1f;

    public WeaponType type = WeaponType.Shovel;
    public WeaponDatabase weaponDatabase; // ✅ 변수 선언

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void SetDirection(float dir)
    {
        direction = dir;
    }

    private void Update()
    {
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
    }

   private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            B_Enemy enemy = other.GetComponent<B_Enemy>();
            if (enemy != null)
            {
                int damage = weaponDatabase.GetWeaponData(type).damage;
                enemy.TakeDamage(damage);

                // ✅ 삽일 경우 스턴 적용
                if (type == WeaponType.Shovel)
                    enemy.Stun(1f); // 1초간 스턴
            }
        }

        if (other.CompareTag("Ground") || other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }

}
