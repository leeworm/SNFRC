using UnityEngine;
using UnityEngine.SceneManagement;

public class MiddleSceneManager : MonoBehaviour
{
    public GameObject[] ErrorEffectPrefabs;

    void Start()
    {
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(MainGameManager.Instance.ErrorNum >= 6)
            {
                SceneManager.LoadScene("EndingScene");
                return;
            }

            if(MainGameManager.Instance.ErrorNum == 0)
            {
                SceneManager.LoadScene(1);
                return;
            }
            SceneManager.LoadScene(MainGameManager.Instance.ErrorNum);
        }

        for(int i = 0; i < MainGameManager.Instance.ErrorNum; i++)
        {
            ErrorEffectPrefabs[i].SetActive(true);
        }
        
    }
}
