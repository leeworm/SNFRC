using UnityEngine;
using UnityEngine.UI;

public class B_HeartUI : MonoBehaviour
{
    public Sprite fullHeart;     // 빨간 하트
    public Sprite emptyHeart;    // 검정 하트
    public Image[] hearts;       // 10개 하트 슬롯
    private int maxHearts = 10;

    public void UpdateHearts(int currentHearts)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHearts)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;
        }
    }
}