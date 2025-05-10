using UnityEngine;

public class BowSkill : WeaponSkill
{
    [SerializeField] private GameObject arrowPrefab;

    public override void UseSkill(Transform firePoint)
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mouseWorldPos - firePoint.position).normalized;

        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, Quaternion.identity);

        ArrowProjectile proj = arrow.GetComponent<ArrowProjectile>();
        if (proj != null)
        {
            proj.Launch(direction);
            proj.type = WeaponType.Bow; // ✅ 무기 타입 전달!
        }
    }
}
