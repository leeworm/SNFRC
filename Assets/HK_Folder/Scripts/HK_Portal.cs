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
        DeactivatePortal(); // �⺻ ��Ȱ��ȭ ����
    }

    public void ActivatePortal()
    {
        isActive = true;
        // ��Ż Ȱ��ȭ ����Ʈ�� �ִϸ��̼� �߰� ����
        GetComponent<SpriteRenderer>().color = Color.white; // ����: �� ���
    }

    public void DeactivatePortal()
    {
        isActive = false;
        GetComponent<SpriteRenderer>().color = Color.gray; // ��Ȱ�� ����
    }
}
