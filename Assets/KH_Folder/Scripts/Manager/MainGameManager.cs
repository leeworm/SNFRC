using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    public int ErrorNum = 0; // 오류조각 개수

    public GameObject[] ErrorEffectPrefabs;

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
        for(int i = 0; i <= ErrorNum; i++)
        {
            ErrorEffectPrefabs[i].SetActive(true);
        }
    }

    public void GetErrorPiece()
    {
        ErrorNum++;
    }


}
