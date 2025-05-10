using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickBoomerang : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 targetPos;
    private Transform player;
    private float travelTime = 0.3f;
    private float waitTime = 0.4f;
    private float returnTime = 0.3f;
    private float elapsed;

    public WeaponType type;
    public WeaponDatabase weaponDatabase;

    // ✅ 여기에 빠졌던 변수 추가!
    private HashSet<B_Enemy> affectedEnemies = new HashSet<B_Enemy>();

    private enum Phase { Out, Wait, Back }
    private Phase currentPhase = Phase.Out;

    public void Initialize(float direction, Transform playerTransform)
    {
        startPos = transform.position;
        targetPos = startPos + new Vector3(5f * direction, 0f, 0f);
        player = playerTransform;
    }

    private void Update()
    {
        elapsed += Time.deltaTime;

        switch (currentPhase)
        {
            case Phase.Out:
                transform.position = Vector3.Lerp(startPos, targetPos, elapsed / travelTime);
                if (elapsed >= travelTime)
                {
                    elapsed = 0f;
                    currentPhase = Phase.Wait;
                }
                break;

            case Phase.Wait:
                if (elapsed >= waitTime)
                {
                    elapsed = 0f;
                    currentPhase = Phase.Back;
                }
                break;

            case Phase.Back:
                transform.position = Vector3.Lerp(targetPos, player.position, elapsed / returnTime);
                if (elapsed >= returnTime)
                {
                    Destroy(gameObject);
                }
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            B_Enemy enemy = other.GetComponent<B_Enemy>();
            if (enemy != null && !affectedEnemies.Contains(enemy))
            {
                affectedEnemies.Add(enemy);
                StartCoroutine(DamageOverTime(enemy));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            B_Enemy enemy = other.GetComponent<B_Enemy>();
            if (enemy != null && affectedEnemies.Contains(enemy))
            {
                affectedEnemies.Remove(enemy);
                // DamageOverTime 코루틴은 while 조건이 false가 되어 자동 종료됨
            }
        }
    }

    private IEnumerator DamageOverTime(B_Enemy enemy)
    {
        while (affectedEnemies.Contains(enemy))
        {
            if (weaponDatabase != null)
            {
                int damage = weaponDatabase.GetWeaponData(type).damage;
                enemy.TakeDamage(damage);
                Debug.Log($" 곡괭이 지속 데미지: {damage}");
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}
