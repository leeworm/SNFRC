using System.Collections.Generic;
using UnityEngine;

public class DH_PlayerAnimationTrigger : MonoBehaviour
{
    [SerializeField] private Vector3 spawnOffset; // 인스펙터에서 직접 조정 가능

    [System.Serializable]
    public class EffectEntry
    {
        public string effectName;
        public GameObject prefab;
    }
    public List<EffectEntry> effectEntries; // 이펙트 이름과 프리팹을 매핑하는 리스트

    private Dictionary<string, GameObject> effectDict;
    private GameObject spawnedEffect;

    private DH_Player player => GetComponentInParent<DH_Player>();

    void Awake()
    {
        effectDict = new Dictionary<string, GameObject>();
        foreach (var entry in effectEntries)
        {
            if (!effectDict.ContainsKey(entry.effectName))
                effectDict.Add(entry.effectName, entry.prefab);
        }
    }

    private void AnimationTrigger()
    {
        Debug.Log("AnimationTrigger invoked for: " + player.currentState);
        player.AnimationTrigger();
    }

    public void SpawnEffect(string effectName)
    {
        if (!effectDict.ContainsKey(effectName))
        {
            Debug.LogWarning($"[Effect] '{effectName}' 이펙트가 등록되어 있지 않습니다.");
            return;
        }
        GameObject prefab = effectDict[effectName];
        spawnedEffect = Instantiate(prefab, player.effectSpawnPoint.position, Quaternion.identity);
        spawnedEffect.transform.SetParent(null);
    }

    public void DespawnEffect()
    {
        if (spawnedEffect != null)
            Destroy(spawnedEffect);
    }

    // 공격 시 발동
    public void ActivateHitbox() => player.ActivateHitbox();
    public void DeactivateHitbox() => player.DeactivateHitbox();

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
    public void OnSubstituteVanishEnd()
    {
        if (player.currentState is DH_PlayerSubstituteState sub)
            sub.OnVanishAnimationEnd();
    }

    public void OnSubstituteAppearEnd()
    {
        if (player.currentState is DH_PlayerSubstituteState sub)
            sub.OnAppearAnimationEnd();
    }
}
