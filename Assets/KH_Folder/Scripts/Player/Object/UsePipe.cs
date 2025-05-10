using UnityEngine;
using UnityEngine.UI;

public class UsePipe : MonoBehaviour
{
    public Image durationBar;
    public float duration = 10f; // 지속시간

    private void Update()
    {
        durationBar.fillAmount -= Time.deltaTime / duration; // 10초 동안 줄어듭니다.
        if (durationBar.fillAmount <= 0)
        {
            Destroy(gameObject); // 바가 다 차면 오브젝트를 삭제합니다.
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("EnemyBullet"))
        {
            Destroy(collision.gameObject);
        }
    }
}
