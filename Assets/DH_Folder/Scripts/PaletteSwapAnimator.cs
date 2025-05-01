using UnityEngine;

public class PaletteSwapAnimator : MonoBehaviour
{
    public Material paletteSwapMaterial; // Shader가 적용된 머티리얼
    public float frameRate = 12f;         // 초당 몇 줄 바뀔지
    private float timer = 0f;
    private int currentRow = 0;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1f / frameRate)
        {
            timer = 0f;
            currentRow = (currentRow + 1) % 12;
            paletteSwapMaterial.SetFloat("_SwapIndex", currentRow);
        }
    }
}
