using UnityEngine;
using UnityEngine.SceneManagement;

public class JH_Portal : MonoBehaviour
{
    public string nextSceneName;
    public float transitionDelay = 1f;  

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // 플레이어가 포털에 닿으면 다음 씬으로 전환
            StartCoroutine(LoadNextScene());
        }
    }

    private System.Collections.IEnumerator LoadNextScene()
    {
        // 필요한 경우 페이드 효과나 전환 효과 추가
        yield return new WaitForSeconds(transitionDelay);

        // 다음 씬 로드
        SceneManager.LoadScene(nextSceneName);
    }
    
}