using UnityEngine;

public class PhaseChangeTrigger : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // PhaseChange 메서드 호출
            KH_GameManager.Instance.StartPhase2();
        }
    }

}
