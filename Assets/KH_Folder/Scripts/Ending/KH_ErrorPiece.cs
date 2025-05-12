using UnityEngine;

public class KH_ErrorPiece : MonoBehaviour
{
    public GameObject portalPrefab;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            MainGameManager.Instance.GetErrorPiece();

            Instantiate(portalPrefab, new Vector3(0,-196,0), Quaternion.identity);

            Destroy(this.gameObject);
        }
    }
}
