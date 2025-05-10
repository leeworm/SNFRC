// VerticalHealthBar.cs
using UnityEngine;
using UnityEngine.UI;

public class VerticalHealthBar : MonoBehaviour
{
    public Health targetHealth;     // 연결할 대상
    public Slider slider;           // UI 슬라이더


    void Start()
    {
        if (targetHealth != null && slider != null)
        {
            slider.value = targetHealth.GetHealthPercent();
        }
    }
}
