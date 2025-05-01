using UnityEngine;

public class PaletteController : MonoBehaviour
{
    public Material paletteMaterial;
    public int paletteIndex = 0;

    void Update()
    {
        paletteMaterial.SetFloat("_PaletteIndex", paletteIndex);
    }
}
