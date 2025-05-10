using UnityEngine;

public class PiecingBrick : MonoBehaviour
{
    [SerializeField] private GameObject[] breakEffectPrefab; // 파괴 효과 프리팹

    private const float adjustPos = 0.25f; // 파괴 효과의 Y축 위치 조정 값

    [SerializeField] private Vector2[] brickVelocityOffsets;

    void Start()
    {
        // 파괴 효과의 위치 오프셋 배열
        Vector3[] offsets = new Vector3[]
        {
            new Vector3(-adjustPos, adjustPos, 0),  // 왼쪽 위
            new Vector3(adjustPos, adjustPos, 0),   // 오른쪽 위
            new Vector3(-adjustPos, -adjustPos, 0), // 왼쪽 아래
            new Vector3(adjustPos, -adjustPos, 0)   // 오른쪽 아래
        };
        // 루프를 사용하여 파괴 효과 생성
        for (int i = 0; i < breakEffectPrefab.Length; i++)
        {
            Vector3 spawnPosition = transform.position + offsets[i];
            GameObject effect = Instantiate(breakEffectPrefab[i], spawnPosition, Quaternion.Euler(0, 0, 45));
            // Rigidbody2D를 가져오거나 추가
            Rigidbody2D rb = effect.GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                rb = effect.AddComponent<Rigidbody2D>();
            }
            rb.linearVelocity = brickVelocityOffsets[i]; // 포물선 운동을 위한 초기 속도 설정
        }
        
        Destroy(this.gameObject, 2.0f);
    }

    void Update()
    {
        
    }
}
