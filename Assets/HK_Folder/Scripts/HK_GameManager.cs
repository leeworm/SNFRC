using UnityEngine;

public class HK_GameManager : MonoBehaviour
{
    public GameObject potalPrefab;
    public Vector3 potalPos;

    private static HK_GameManager instance;
    public static HK_GameManager Instance
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
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void CreatePotal()
    {
        Instantiate(potalPrefab, potalPos, Quaternion.identity);
    }
}
