using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySpawnData
{
    public GameObject enemyPrefab; // 적 프리팹
    public Transform spawnPoint;  // 적이 생성될 위치
    [HideInInspector] public bool isSpawned = false; // 적이 생성되었는지 여부
}

public class EnemySpawnTrigger : MonoBehaviour
{
    [SerializeField] private List<EnemySpawnData> enemySpawnData; // 적 생성 데이터 배열

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (var data in enemySpawnData)
            {
                if (!data.isSpawned) // 이미 생성된 적은 건너뜀
                {
                    Instantiate(data.enemyPrefab, data.spawnPoint.position, data.spawnPoint.rotation);
                    data.isSpawned = true; // 생성 상태 업데이트
                }
            }
        }
    }
}