using System.Collections.Generic;
using UnityEngine;

public class DH_EffectPoolManager : MonoBehaviour
{
    public static DH_EffectPoolManager Instance { get; private set; }

    [System.Serializable]
    public class EffectEntry
    {
        public string effectName;
        public GameObject prefab;
        public int initialPoolSize = 10;
    }

    [Header("이펙트 프리팹 등록")]
    public List<EffectEntry> effectEntries;

    private Dictionary<string, Queue<GameObject>> poolDict = new();
    private Dictionary<string, GameObject> prefabDict = new();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        //DontDestroyOnLoad(gameObject); // 씬 전환 시 유지

        foreach (var entry in effectEntries)
        {
            if (string.IsNullOrEmpty(entry.effectName) || entry.prefab == null)
                continue;

            prefabDict[entry.effectName] = entry.prefab;
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

    public GameObject SpawnEffect(string effectName, Vector3 position)
    {
        if (!poolDict.ContainsKey(effectName))
        {
            Debug.LogWarning($"[EffectPool] '{effectName}' 이펙트가 등록되지 않았습니다.");
            return null;
        }

        GameObject obj = (poolDict[effectName].Count > 0) ? poolDict[effectName].Dequeue() : Instantiate(prefabDict[effectName]);

        obj.transform.position = position;
        obj.SetActive(true);

        var effect = obj.GetComponent<DH_EffectObject>();
        if (effect != null)
            effect.Initialize(effectName, ReturnToPool);

        return obj;
    }

    public void ReturnToPool(string effectName, GameObject obj)
    {
        obj.SetActive(false);
        if (poolDict.ContainsKey(effectName))
            poolDict[effectName].Enqueue(obj);
    }
}
