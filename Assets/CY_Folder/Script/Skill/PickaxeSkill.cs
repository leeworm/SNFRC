using UnityEngine;

public class PickaxeSkill : WeaponSkill
{
    [SerializeField] private GameObject pickPrefab;

    public override void UseSkill(Transform firePoint)
    {
        if (pickPrefab == null) return;

        float direction = Mathf.Sign(firePoint.parent.localScale.x);

        GameObject pick = Instantiate(pickPrefab, firePoint.position, Quaternion.identity);
        PickBoomerang boomerang = pick.GetComponent<PickBoomerang>();
        if (boomerang != null)
        {
            boomerang.Initialize(direction, firePoint.parent);
            boomerang.type = WeaponType.Pick; // ✅ 무기 타입 전달!
            boomerang.weaponDatabase = weaponDatabase;
        }

        // 부메랑 오브젝트 시각 방향 정리
        Vector3 scale = pick.transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direction;
        pick.transform.localScale = scale;
    }
}
