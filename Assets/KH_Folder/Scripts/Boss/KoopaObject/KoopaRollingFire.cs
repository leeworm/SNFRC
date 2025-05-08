using UnityEngine;

public class KoopaRollingFire : MonoBehaviour
{
    [Header("회전 속도")]
    [SerializeField] private float rotationSpeed = 10f; // 회전 속도

    void Start()
    {

    }
    void Update()
    {
        transform.Rotate(0,0, rotationSpeed * Time.deltaTime);
    }

}
