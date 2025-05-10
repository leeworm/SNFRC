using UnityEngine;

public class HK_PlayerTestDamage : MonoBehaviour
{
    public HK_Health health;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            health.TakeDamage(10); // HŰ ������ 10 ������
        }
    }
}