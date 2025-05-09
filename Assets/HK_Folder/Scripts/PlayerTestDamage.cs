using UnityEngine;

public class PlayerTestDamage : MonoBehaviour
{
    public Health health;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            health.TakeDamage(10); // H키 누르면 10 데미지
        }
    }
}