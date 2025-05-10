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
            Debug.LogError("HealthBarUI: healthFillImage�� �Ҵ���� �ʾҽ��ϴ�!");
            enabled = false;
            return;
        }

        if (targetEntity == null)
        {
            // �÷��̾� �±׷� ã�� ���� ���� �״�� ����
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                targetEntity = playerObject.GetComponent<JH_Entity>();
            }
        }

        if (targetEntity != null)
        {
            // targetEntity�� Awake()�� ���� ����Ǿ� CurrentHP�� MaxHP�� ������ ���¿��� �մϴ�.
            Debug.Log("HealthBarUI Start: " + targetEntity.name + "�� ü�� �ʱ�ȭ �õ�. HP: " + targetEntity.CurrentHP + "/" + targetEntity.MaxHP);

            targetEntity.OnHealthChanged += UpdateHealthBar; // �̺�Ʈ ����
            UpdateHealthBar(targetEntity.CurrentHP, targetEntity.MaxHP); // ���� ü������ UI ��� ������Ʈ
        }
        else
        {
            Debug.LogWarning("HealthBarUI: targetEntity�� �������� �ʾҽ��ϴ�.");
        }
    }

    public void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        if (healthFillImage == null) return;

        if (maxHealth <= 0)
        {
            healthFillImage.fillAmount = 0;
            // Debug.LogWarning("UpdateHealthBar: MaxHealth�� 0 �����̹Ƿ� fillAmount�� 0���� �����մϴ�.");
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
