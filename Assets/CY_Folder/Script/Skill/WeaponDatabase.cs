using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/WeaponDatabase")]
public class WeaponDatabase : ScriptableObject
{
    public WeaponData[] weapons;

    public WeaponData GetWeaponData(WeaponType type)
    {
        foreach (var weapon in weapons)
        {
            if (weapon.type == type)
                return weapon;
        }
        return null;
    }
}
