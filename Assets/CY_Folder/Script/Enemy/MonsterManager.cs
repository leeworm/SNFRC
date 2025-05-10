using UnityEngine;
using System.Collections.Generic;

public class MonsterManager : MonoBehaviour
{
    [Header("기존 몬스터들 제거용 태그")]
    public string enemyTag = "Enemy";

    [Header("스폰 위치 및 프리팹")]
    public List<Transform> spawnPoints;         // 네더 몬스터 생성 위치
    public List<GameObject> netherMonsterPrefabs;

    public void ReplaceMonsters()
    {
        // 기존 몬스터 제거
        GameObject[] existing = GameObject.FindGameObjectsWithTag(enemyTag);
        foreach (var enemy in existing)
        {
            Destroy(enemy);
        }

        // 새로운 몬스터 스폰
        for (int i = 0; i < spawnPoints.Count && i < netherMonsterPrefabs.Count; i++)
        {
            Instantiate(netherMonsterPrefabs[i], spawnPoints[i].position, Quaternion.identity);
        }
    }
}
