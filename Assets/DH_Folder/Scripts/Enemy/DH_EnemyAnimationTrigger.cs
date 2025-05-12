using System.Collections.Generic;
using UnityEngine;

public class DH_EnemyAnimationTrigger : MonoBehaviour
{
    [System.Serializable]
    public class EffectEntry
    {
        public string effectName;
        public GameObject prefab;
    }

    [Header("Effect 설정")]
    public List<EffectEntry> effectEntries;

    private Dictionary<string, GameObject> effectDict;
    private GameObject spawnedEffect;

    private DH_Enemy enemy => GetComponentInParent<DH_Enemy>();

    private void Awake()
    {
        effectDict = new Dictionary<string, GameObject>();

        foreach (var entry in effectEntries)
        {
            if (!effectDict.ContainsKey(entry.effectName))
                effectDict.Add(entry.effectName, entry.prefab);
        }
    }

    // 애니메이션 이벤트에서 호출될 함수들

    public void AnimationTrigger()
    {
        enemy.AnimationTrigger(); // 상태의 AnimationFinishTrigger() 호출
    }

    public void SpawnEffect(string effectName)
    {
        if (!effectDict.ContainsKey(effectName))
        {
            Debug.LogWarning($"[EnemyEffect] '{effectName}' 이펙트가 등록되어 있지 않음");
            return;
        }

        GameObject prefab = effectDict[effectName];
        spawnedEffect = Instantiate(prefab, enemy.transform.position, Quaternion.identity);
        spawnedEffect.transform.SetParent(null);
    }

    public void DespawnEffect()
    {
        if (spawnedEffect != null)
            Destroy(spawnedEffect);
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
