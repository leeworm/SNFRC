using UnityEngine;

public class JH_PlayerManager : MonoBehaviour
{
    public static JH_PlayerManager instance;
    public JH_Player player;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;

    }
}
