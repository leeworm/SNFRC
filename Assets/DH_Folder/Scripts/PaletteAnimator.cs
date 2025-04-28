// File: Assets/Scripts/PaletteAnimator.cs
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class PaletteAnimator : MonoBehaviour
{
    [SerializeField] private Material material;
    [SerializeField] private float frameRate = 6f; // Frames per second
    [SerializeField] private int paletteRows = 12; // Total rows in palette texture

    private float timer = 0f;
    private int currentFrame = 0;

    void Start()
    {
        if (material == null)
        {
            Renderer renderer = GetComponent<Renderer>();
            material = renderer?.material;

            if (material == null)
            {
                Debug.LogWarning("PaletteAnimator: No material assigned and no renderer found.");
                enabled = false;
                return;
            }
        }

        material.SetFloat("_FrameIndex", currentFrame);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1f / frameRate)
        {
            timer -= 1f / frameRate;
            currentFrame = (currentFrame + 1) % paletteRows;
            material.SetFloat("_FrameIndex", currentFrame);
        }
    }
}