using UnityEngine;

public class DH_ItemPickup : MonoBehaviour
{
    public DH_Player player;
    public GameObject portalPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector3 spawn = new Vector3(player.transform.position.x - 8f, 0, 0);
            Instantiate(portalPrefab, spawn, Quaternion.identity);
            gameObject.SetActive(false); // or Destroy(gameObject);
        }
    }
}
