using UnityEngine;

public class PaletteAnimator : MonoBehaviour
{
    public Material targetMaterial;
    public Texture2D[] paletteFrames;
    public float frameRate = 12f;

    private int currentFrame = 0;
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 1f / frameRate)
        {
            currentFrame = (currentFrame + 1) % paletteFrames.Length;
            targetMaterial.SetTexture("_PaletteTex", paletteFrames[currentFrame]);
            timer = 0f;
        }
    }
}
