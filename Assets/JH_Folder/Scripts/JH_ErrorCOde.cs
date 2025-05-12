using UnityEngine;

public class JH_ErrorCOde : MonoBehaviour
{
    public GameObject portalPrefab;  // ������ ���� ������
    private static GameObject portal; // ������ ���� ���� ���� (�ߺ� ���� ����)

    [Header("���� ���� ��ġ")]
    public Vector2 mapCenter = new Vector2(0f, 0f);  
    public float portalHeight = 1f;  

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {   
            MainGameManager.Instance.GetErrorPiece();

            if (portal == null)
            {
                Camera mainCamera = Camera.main;
                if (mainCamera != null)
                {
                    float height = mainCamera.orthographicSize * 2f; 
                    float width = height * mainCamera.aspect;       

                    float xPos = 0f; 
                    float yPos = (height * 0.3f) - mainCamera.orthographicSize; 

                    Vector3 portalPosition = new Vector3(xPos, yPos, 0f);
                    portal = Instantiate(portalPrefab, portalPosition, Quaternion.identity);

                }
            }

            Destroy(gameObject);
        }
    }
}