using UnityEngine;

public class Brick_Mini : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 1.5f); // 0.5초 후에 오브젝트 삭제
    }
}
