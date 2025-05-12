using System.Collections.Generic;
using UnityEngine;

public class DH_EnemyAnimationTrigger : MonoBehaviour
{
    [SerializeField] private Vector3 spawnOffset;

    [System.Serializable]
    public class EffectEntry
    {
        public string effectName;
        public GameObject prefab;
        public int initialPoolSize = 10;
    }

    public List<EffectEntry> effectEntries;

    private Dictionary<string, Queue<GameObject>> poolDict = new();
    private Dictionary<string, GameObject> prefabLookup = new();

    private DH_Enemy enemy => GetComponentInParent<DH_Enemy>();

    void Awake()
    {
        foreach (var entry in effectEntries)
        {
            if (string.IsNullOrEmpty(entry.effectName) || entry.prefab == null)
                continue;

            prefabLookup[entry.effectName] = entry.prefab;

            var queue = new Queue<GameObject>();
            for (int i = 0; i < entry.initialPoolSize; i++)
            {
                var obj = Instantiate(entry.prefab);
                obj.SetActive(false);
                queue.Enqueue(obj);
            }
            poolDict[entry.effectName] = queue;
        }
    }

    // 애니메이션 이벤트에서 호출될 함수들

    public void AnimationTrigger()
    {
        enemy.AnimationTrigger(); // 상태의 AnimationFinishTrigger() 호출
    }

    public void SpawnEffect(string effectName)
    {
        if (!poolDict.ContainsKey(effectName))
        {
            Debug.LogWarning($"[EffectPool] '{effectName}' 이펙트가 등록되어 있지 않습니다.");
            return;
        }

        GameObject obj = (poolDict[effectName].Count > 0) ? poolDict[effectName].Dequeue() : Instantiate(prefabLookup[effectName]);
        obj.transform.position = enemy.effectSpawnPoint.position + spawnOffset;
        obj.SetActive(true);

        var effect = obj.GetComponent<DH_EffectObject>();
        if (effect != null)
            effect.Initialize(effectName, ReturnEffectToPool);
        else
            Debug.LogWarning($"[EffectPool] '{effectName}' 이펙트에 EffectObject 컴포넌트가 없습니다.");
    }

    private void ReturnEffectToPool(string effectName, GameObject obj)
    {
        obj.SetActive(false);
        poolDict[effectName].Enqueue(obj);
    }

    public void EnableHitbox(string hitboxName)
    {
        enemy.ActivateHitbox(hitboxName);
    }

    public void DisableHitbox(string hitboxName)
    {
        enemy.DeactivateHitbox(hitboxName);
    }
}
