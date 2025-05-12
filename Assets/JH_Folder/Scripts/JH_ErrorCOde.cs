using UnityEngine;

public class JH_ErrorCOde : MonoBehaviour
{
    public GameObject portalPrefab;  // 생성할 포털 프리팹
    private static GameObject portal; // 생성된 포털 참조 저장 (중복 생성 방지)

    [Header("포털 생성 위치")]
    public Vector2 mapCenter = new Vector2(0f, 0f);  
    public float portalHeight = 1f;  

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
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