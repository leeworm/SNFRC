using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log("🎉 게임 종료!");

        // 예시: 씬 전환
        //SceneManager.LoadScene("MiddleScene");

        // 또는 멈추기
        // Time.timeScale = 0;

        // 또는 메시지만 출력
        // ShowGameOverUI();
    }
}
