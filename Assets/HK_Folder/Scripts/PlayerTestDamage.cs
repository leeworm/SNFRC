using UnityEngine;

public class PlayerTestDamage : MonoBehaviour
{
    public Health health;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            health.TakeDamage(10); // HŰ ������ 10 ������
        }
    }
}