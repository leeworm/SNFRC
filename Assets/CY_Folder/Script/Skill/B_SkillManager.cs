using UnityEngine;

public class B_SkillManager : MonoBehaviour
{
    public ShovelSkill shovelSkill;
    public PickaxeSkill pickaxeSkill;
    public BowSkill bowSkill;

    public void UseSkill(WeaponType weapon, Transform firePoint)
    {
        switch (weapon)
        {
            case WeaponType.Shovel:
                shovelSkill.UseSkill(firePoint);
                break;
            case WeaponType.Pick:
                pickaxeSkill.UseSkill(firePoint);
                break;
            case WeaponType.Bow:
                bowSkill.UseSkill(firePoint);
                break;
            default:
                Debug.Log("스킬 없음");
                break;
        }
    }
}
