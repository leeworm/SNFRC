using UnityEngine;

public class DH_ItemPickup : MonoBehaviour
{
    public GameObject portalPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector3 spawn = new Vector3(collision.transform.position.x, collision.transform.position.y, 0);
            Instantiate(portalPrefab, spawn, Quaternion.identity);
            gameObject.SetActive(false); // 또는 Destroy(gameObject);
        }
    }
}
