using UnityEngine;
using System.Collections.Generic;

public class B_InitialMonsterSpawner : MonoBehaviour
{
    [Header("오버월드 몬스터 스폰 위치 및 프리팹")]
    public List<Transform> spawnPoints;
    public List<GameObject> overworldMonsterPrefabs;

    void Start()
    {
        SpawnOverworldMonsters();
    }

    void SpawnOverworldMonsters()
    {
        for (int i = 0; i < spawnPoints.Count && i < overworldMonsterPrefabs.Count; i++)
        {
            Instantiate(overworldMonsterPrefabs[i], spawnPoints[i].position, Quaternion.identity);
        }
    }
}
