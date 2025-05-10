using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Image healthFillImage;
    public JH_Entity targetEntity;

    void Start()
    {
        if (healthFillImage == null)
        {
            Debug.LogError("HealthBarUI: healthFillImage가 할당되지 않았습니다!");
            enabled = false;
            return;
        }

        if (targetEntity == null)
        {
            // 플레이어 태그로 찾는 로직 등은 그대로 유지
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                targetEntity = playerObject.GetComponent<JH_Entity>();
            }
        }

        if (targetEntity != null)
        {
            // targetEntity의 Awake()가 먼저 실행되어 CurrentHP와 MaxHP가 설정된 상태여야 합니다.
            Debug.Log("HealthBarUI Start: " + targetEntity.name + "의 체력 초기화 시도. HP: " + targetEntity.CurrentHP + "/" + targetEntity.MaxHP);

            targetEntity.OnHealthChanged += UpdateHealthBar; // 이벤트 구독
            UpdateHealthBar(targetEntity.CurrentHP, targetEntity.MaxHP); // 현재 체력으로 UI 즉시 업데이트
        }
        else
        {
            Debug.LogWarning("HealthBarUI: targetEntity가 설정되지 않았습니다.");
        }
    }

    public void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        if (healthFillImage == null) return;

        if (maxHealth <= 0)
        {
            healthFillImage.fillAmount = 0;
            // Debug.LogWarning("UpdateHealthBar: MaxHealth가 0 이하이므로 fillAmount를 0으로 설정합니다.");
            return;
        }

        float fillValue = (float)currentHealth / maxHealth;
        healthFillImage.fillAmount = fillValue;
        // Debug.Log("UpdateHealthBar: " + currentHealth + "/" + maxHealth + " = " + fillValue);
    }

    void OnDestroy()
    {
        if (targetEntity != null)
        {
            targetEntity.OnHealthChanged -= UpdateHealthBar;
        }
    }
}
