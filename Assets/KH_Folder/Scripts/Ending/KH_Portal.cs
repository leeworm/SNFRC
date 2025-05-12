using UnityEngine;

public class KH_Portal : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            // 씬 이동
        }
    }
}
