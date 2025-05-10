using UnityEngine;

public class HK_PlayerManager : MonoBehaviour
{
    public static HK_PlayerManager instance;
    public HK_Player player;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }
}
