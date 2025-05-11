using UnityEngine;
using UnityEngine.SceneManagement;

public class HK_Portal : MonoBehaviour
{
    public string nextSceneName;
    private bool isActive = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isActive) return;

        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
    void Start()
    {
        DeactivatePortal(); // 기본 비활성화 상태
    }

    public void ActivatePortal()
    {
        isActive = true;
        // 포탈 활성화 이펙트나 애니메이션 추가 가능
        GetComponent<SpriteRenderer>().color = Color.white; // 예시: 색 밝게
    }

    public void DeactivatePortal()
    {
        isActive = false;
        GetComponent<SpriteRenderer>().color = Color.gray; // 비활성 상태
    }
}
