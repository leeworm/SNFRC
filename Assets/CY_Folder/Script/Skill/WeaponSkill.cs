using UnityEngine;

public abstract class WeaponSkill : MonoBehaviour
{
    public WeaponDatabase weaponDatabase; // ✅ 여기에 추가!

    public abstract void UseSkill(Transform firePoint);
}
