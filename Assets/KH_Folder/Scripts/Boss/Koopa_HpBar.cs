using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class Koopa_HpBar : MonoBehaviour
{
    UnityEngine.UI.Image img; // 체력바 이미지

    void Start()
    {
        img = GetComponent<UnityEngine.UI.Image>(); // 체력바 이미지 컴포넌트 가져오기
        img.fillAmount = 1f; // 체력바 초기화
    }

    void Update()
    {
        
    }

    public void GetDamage(int damage)
    {
        // 체력바 감소
        img.fillAmount -= (float)damage / 1000f; // 체력바 감소 비율 조정
    }
}
