using System.Collections.Generic;
using UnityEngine;

public class KH_BulletPool : MonoBehaviour
{
    public static KH_BulletPool Instance { get; private set; } // 싱글톤 인스턴스

    public GameObject bulletPrefab; // 총알 프리팹
    public int poolSize = 20; // 풀 크기
    private Queue<GameObject> bulletPool;

    void Awake()
    {
        // 싱글톤 설정
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // 중복된 인스턴스 제거
            return;
        }

        bulletPool = new Queue<GameObject>();

        // 풀 초기화
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletPool.Enqueue(bullet);
        }
    }

    public GameObject GetBullet(Transform spawnPoint, int dir)
    {
        if (bulletPool.Count > 0)
        {
            GameObject bullet = bulletPool.Dequeue();
            bullet.transform.position = spawnPoint.position; // 발사 위치 설정
            
            bullet.GetComponent<KH_Fireball>().SetDirX(dir); // 방향 설정

            bullet.SetActive(true);
            return bullet;
        }
        else
        {
            // 풀 크기를 초과하면 새로 생성 (선택 사항)
            GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
            //bullet.GetComponent<KH_Fireball>().SetDirX(dir); // 방향 설정
            return bullet;
        }
    }

    public void ReturnBullet(GameObject bullet)
    {
        KH_EffectManager.Instance.PlayEffect("FireBomb", bullet.transform.position); // 이펙트 재생

        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);
    }
}