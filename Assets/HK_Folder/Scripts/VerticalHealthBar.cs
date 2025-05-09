// VerticalHealthBar.cs
using UnityEngine;
using UnityEngine.UI;

public class VerticalHealthBar : MonoBehaviour
{
    public Health targetHealth;     // ������ ���
    public Slider slider;           // UI �����̴�

    void Update()
    {
        if (targetHealth != null && slider != null)
        {
            slider.value = targetHealth.GetHealthPercent();
        }
    }
}
