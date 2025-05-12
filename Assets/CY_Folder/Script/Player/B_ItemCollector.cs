using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    public static bool hasEnderItem = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            hasEnderItem = true;
            Destroy(gameObject);
        }
    }
}
