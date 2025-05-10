using UnityEngine;

public class KoopaRollingFire_small : MonoBehaviour
{
    public float speed = 0f; // 이동 속도

    void Start()
    {
        
    }

    void Update()
    {
        transform.position +=  new Vector3(0, speed * Time.deltaTime, 0); 
    }
}
