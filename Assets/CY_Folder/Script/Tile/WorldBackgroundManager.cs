using UnityEngine;
using System.Collections.Generic;

public class WorldBackgroundManager : MonoBehaviour
{
    [Header("Sprite만 교체할 배경 목록")]
    public List<BackgroundSpriteSwapEntry> spriteSwaps;

    public void SwitchToNether()
    {
        foreach (var entry in spriteSwaps)
        {
            if (entry.targetRenderer != null && entry.netherSprite != null)
            {
                entry.targetRenderer.sprite = entry.netherSprite;
            }
        }
    }
}
