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
            // �÷��̾ ���п� ������ ���� ������ ��ȯ
            StartCoroutine(LoadNextScene());
        }

        if (other.CompareTag("Player"))
        {
            // �÷��̾ ���п� ������ ���� ������ ��ȯ
            StartCoroutine(LoadNextScene());
        }
    }

    private System.Collections.IEnumerator LoadNextScene()
    {
        // �ʿ��� ��� ���̵� ȿ���� ��ȯ ȿ�� �߰�
        yield return new WaitForSeconds(transitionDelay);

        // ���� �� �ε�
        SceneManager.LoadScene(nextSceneName);
    }
    
}