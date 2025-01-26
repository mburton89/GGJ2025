using UnityEngine;

public class UnderwaterEffect : MonoBehaviour
{
    public Material underwaterMaterial;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (underwaterMaterial != null)
        {
            Graphics.Blit(src, dest, underwaterMaterial);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }
}
