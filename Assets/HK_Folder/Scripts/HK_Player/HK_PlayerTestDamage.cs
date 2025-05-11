using UnityEngine;

public class HK_PlayerTestDamage : MonoBehaviour
{
    public HK_Health health;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            // 플레이어 위치를 공격 위치로 전달
            health.TakeDamage(10, transform.position); // H키를 누르면 10의 피해를 줍니다
        }
    }
}
