using UnityEngine;

public class EffectDestroyer : MonoBehaviour
{
    public bool UseDestroyTime = false; // 파괴 시간 사용 여부
    public float destroyTime = 0f; // 파괴될 시간

    private void Start()
    {
        if(UseDestroyTime)
        {
            Destroy(gameObject, destroyTime); // destroyTime 초 후에 오브젝트 파괴
        }
    }

    public void DestroyEffect()
    {
        Destroy(gameObject);
    }
}