using UnityEngine;

[System.Serializable]
public class WeaponData
{
    public WeaponType type;    // 무기 종류 (enum)
    public int damage;         // 무기별 데미지
    public float attackRange;  // 필요 시 공격 범위
    public float cooldown;     // 필요 시 쿨타임
}
