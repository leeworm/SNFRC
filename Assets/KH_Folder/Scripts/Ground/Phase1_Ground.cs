using UnityEngine;

public class Phase1_Ground : MonoBehaviour
{
    [SerializeField] private GameObject piecingBrickPrefab;

    [SerializeField] private int startXpos;
    [SerializeField] private int distanceXpos;
    [SerializeField] private int EndXpos;

    [SerializeField] private int startYpos;
    [SerializeField] private int distanceYpos;
    [SerializeField] private int EndYpos;

    void OnDestroy()
    {
        if (gameObject.scene.isLoaded)
        {
            for(int i = startXpos; i <= EndXpos; i += distanceXpos)
            {
                for(int j = startYpos; j <= EndYpos; j += distanceYpos)
                    Instantiate(piecingBrickPrefab, new Vector3(i, j, 0), Quaternion.identity);
            }
        }

        
    }
}
