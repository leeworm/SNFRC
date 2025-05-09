using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Health enemy = other.GetComponent<Health>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}
