using UnityEngine;
using UnityEngine.SceneManagement;

public class KH_NextScene : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.gameObject.SetActive(false);

            Invoke("LoadNextScene", 2f);
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene("BossScene_Koopa");
    }
}
