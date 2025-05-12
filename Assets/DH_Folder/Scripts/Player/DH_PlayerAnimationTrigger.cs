using System.Collections.Generic;
using UnityEngine;

public class DH_PlayerAnimationTrigger : MonoBehaviour
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

    private DH_Player player => GetComponentInParent<DH_Player>();

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

    private void AnimationTrigger()
    {
        Debug.Log("AnimationTrigger invoked for: " + player.currentState);
        player.AnimationTrigger();
    }

    public void SpawnEffect(string effectName)
    {
        if (!poolDict.ContainsKey(effectName))
        {
            Debug.LogWarning($"[EffectPool] '{effectName}' 이펙트가 등록되어 있지 않습니다.");
            return;
        }

        GameObject obj = (poolDict[effectName].Count > 0) ? poolDict[effectName].Dequeue() : Instantiate(prefabLookup[effectName]);
        obj.transform.position = player.effectSpawnPoint.position + spawnOffset;
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

    // 공격 시 발동
    public void ActivateHitbox(string hitboxName) => player.ActivateHitbox(hitboxName);
    public void DeactivateHitbox(string hitboxName) => player.DeactivateHitbox(hitboxName);

    // 콤보 처리
    public virtual void EnableComboWindow()
    {
        if (player.currentState is DH_PlayerPrimaryAttackState attackState)
            attackState.EnableComboWindow();
    }

    public virtual void DisableComboWindow()
    {
        if (player.currentState is DH_PlayerPrimaryAttackState attackState)
            attackState.DisableComboWindow();
    }

    // 공격 종료
    public virtual void OnAttackComplete()
    {
        if (player.currentState is DH_PlayerPrimaryAttackState attackState)
            attackState.OnAttackComplete();
    }

    public void EnableSubstitutionWindow()
    {
        if (player.currentState is DH_PlayerCrouchState crouchState)
        {
            crouchState.OpenSubstitutionWindow();
        }
    }

    public void DisableSubstitutionWindow()
    {
        if (player.currentState is DH_PlayerCrouchState crouchState)
        {
            crouchState.CloseSubstitutionWindow();
        }
    }
    public void OnVanishAnimationEndtoAppear()
    {
        if (player.currentState is DH_PlayerSubstituteState sub)
            sub.OnVanishAnimationEndtoAppear();
    }

    public void OnVanishAnimationEndtoAirAppear()
    {
        if (player.currentState is DH_PlayerTeleportJumpState teleportJumpState)
            teleportJumpState.OnVanishAnimationEndtoAirAppear();
    }

    public void OnSubstituteAppearEnd()
    {
        if (player.currentState is DH_PlayerSubstituteState sub)
            sub.OnAppearAnimationEnd();
    }
}
