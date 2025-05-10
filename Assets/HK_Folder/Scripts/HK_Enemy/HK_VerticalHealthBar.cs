// HK_VerticalHealthBar.cs
using UnityEngine;
using UnityEngine.UI;

public class HK_VerticalHealthBar : MonoBehaviour
{
    public HK_Health targetHealth;     // ������ ���
    public Slider slider;           // UI �����̴�


    void Start()
    {
        if (targetHealth != null && slider != null)
        {
            slider.value = targetHealth.GetHealthPercent();
        }
    }
}
