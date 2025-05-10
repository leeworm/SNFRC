using UnityEngine;

public class ShovelSkill : WeaponSkill
{
    public GameObject slashPrefab;

    public override void UseSkill(Transform firePoint)
    {
        // 방향 계산: 오른쪽이면 1, 왼쪽이면 -1
        float direction = Mathf.Sign(firePoint.parent.localScale.x);

        // 프리팹 생성
        GameObject slash = Instantiate(slashPrefab, firePoint.position, Quaternion.identity);

        // 발사체 설정
        SlashProjectile proj = slash.GetComponent<SlashProjectile>();
        if (proj != null)
        {
            proj.SetDirection(direction);              // 이동 방향 설정
            proj.type = WeaponType.Shovel;             // 무기 타입 넘기기 (데미지용)
        }

        // 화살표/스프라이트 시각 방향 반전
        Vector3 scale = slash.transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direction;
        slash.transform.localScale = scale;
    }
}
