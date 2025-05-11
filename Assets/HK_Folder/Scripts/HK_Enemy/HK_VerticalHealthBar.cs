// HK_VerticalHealthBar.cs
using UnityEngine;
using UnityEngine.UI;

public class HK_VerticalHealthBar : MonoBehaviour
{
    public HK_Health targetHealth;     // 플레이어 혹은 배스의 Health 스크립트를 참조
    public Slider slider;              // UI 슬라이더

    void Start()
    {
        if (targetHealth != null && slider != null)
        {
            slider.value = targetHealth.GetHealthPercent(); // 체력 비율로 슬라이더 값을 설정
        }
    }

    // 주기적으로 체력을 업데이트 하는 메서드 (필요에 따라 호출)
    void Update()
    {
        if (targetHealth != null && slider != null)
        {
            slider.value = targetHealth.GetHealthPercent(); // 체력 비율로 슬라이더 값 업데이트
        }
    }
}
