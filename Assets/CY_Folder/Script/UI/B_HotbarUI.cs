using UnityEngine;
using UnityEngine.UI;

public class B_HotbarUI : MonoBehaviour
{
    public Image[] slotBorders; // 6개의 슬롯 외곽선 이미지
    public Color normalColor =  new Color(1f, 1f, 1f, 1f); // 회색 테두리
    public Color selectedColor = Color.white; // 선택된 슬롯: 흰색

    private int currentIndex = 0;
    private int totalSlots;

    void Start()
    {
        totalSlots = slotBorders.Length;
        UpdateSlotVisuals();
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
        {
            currentIndex = (currentIndex + 1) % totalSlots;
            UpdateSlotVisuals();
        }
        else if (scroll < 0f)
        {
            currentIndex = (currentIndex - 1 + totalSlots) % totalSlots;
            UpdateSlotVisuals();
        }
    }

    void UpdateSlotVisuals()
    {
        for (int i = 0; i < slotBorders.Length; i++)
        {
            slotBorders[i].color = (i == currentIndex) ? selectedColor : normalColor;
        }
    }
}
