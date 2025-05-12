using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameManager : MonoBehaviour
{
    public int ErrorNum = 0; // 오류조각 개수

    //public GameObject[] ErrorEffectPrefabs;

    private static MainGameManager instance;
    public static MainGameManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            // 씬 재시작
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        // if(Input.GetKeyDown(KeyCode.P))
        // {
        //     int currentIndex = SceneManager.GetActiveScene().buildIndex;
        //     int maxIndex = SceneManager.sceneCountInBuildSettings - 1;
            
        //     if (currentIndex < maxIndex)
        //     {
        //         SceneManager.LoadScene(currentIndex + 1);
        //     }
        //     else
        //     {
        //         Debug.Log("마지막 씬입니다.");
        //     }
        // }

    }

    public void GetErrorPiece()
    {
        ErrorNum++;
    }


}
