using UnityEngine;
using UnityEngine.SceneManagement;

public class DH_Portal : MonoBehaviour
{
    public string targetSceneName; // 전환할 씬 이름 (Build Settings에 등록되어 있어야 함)

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(targetSceneName);
        }
    }
}
