using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MobileBlur : MonoBehaviour
{
    public Material[] uiMaterials;

    [Range(0.01f, 1f)]
    public float blurTextureScale = 0.5f;
    protected RenderTexture blurTexture;

    [Range(0, 2)]
    public int downsample = 1;

    [Range(0.0f, 10.0f)]
    public float blurSize = 3.0f;

    [Range(1, 4)]
    public int blurIterations = 2;

    public enum BlurType
    {
        StandardGauss = 0,
        SgxGauss = 1,
    }
    public BlurType blurType = BlurType.StandardGauss;

    public Shader blurShader = null;
    private Material blurMaterial = null;


    protected void OnDestroy()
    {
        if (blurTexture != null)
        {
            Object.Destroy(blurTexture);
            blurTexture = null;
        }
    }
    protected void OnEnable()
    {
        if (support() == false)
        {
            enabled = false;
            return;
        }

        if (blurTexture == null)
        {
            blurTexture = new RenderTexture(Mathf.FloorToInt(Screen.width * blurTextureScale), Mathf.FloorToInt(Screen.height * blurTextureScale), 1);
            //GetComponent<Camera>().targetTexture = blurTexture;
            foreach (var m in uiMaterials)
                m.SetTexture("_blurTex", blurTexture);
        }

        if (blurMaterial == null)
        {
            if (blurShader == null)
            {
                Debug.LogError("blurShader == null", this);
                return;
            }

            blurMaterial = new Material(blurShader);
            blurMaterial.hideFlags = HideFlags.DontSave;
        }
    }

    protected bool support()
    {
        if (SystemInfo.supportsImageEffects == false)
        {
            Debug.LogWarning("SystemInfo.supportsImageEffects == false", this);
            return false;
        }
        if (SystemInfo.supportsRenderTextures == false)
        {
            Debug.LogWarning("SystemInfo.supportsRenderTextures == false", this);
            return false;
        }
        if (blurShader.isSupported == false)
        {
            Debug.LogWarning("blurShader.isSupported == false", this);
            return false;
        }
        return true;
    }

    public void OnDisable()
    {
        if (blurMaterial)
            DestroyImmediate(blurMaterial);
    }

    public void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        float widthMod = 1.0f / (1.0f * (1 << downsample));

        blurMaterial.SetVector("_Parameter", new Vector4(blurSize * widthMod, -blurSize * widthMod, 0.0f, 0.0f));
        source.filterMode = FilterMode.Bilinear;

        int rtW = source.width >> downsample;
        int rtH = source.height >> downsample;

        // downsample
        RenderTexture rt = RenderTexture.GetTemporary(rtW, rtH, 0, source.format);

        rt.filterMode = FilterMode.Bilinear;
        Graphics.Blit(source, rt, blurMaterial, 0);

        var passOffs = blurType == BlurType.StandardGauss ? 0 : 2;

        for (int i = 0; i < blurIterations; i++)
        {
            float iterationOffs = (i * 1.0f);
            blurMaterial.SetVector("_Parameter", new Vector4(blurSize * widthMod + iterationOffs, -blurSize * widthMod - iterationOffs, 0.0f, 0.0f));

            // vertical blur
            RenderTexture rt2 = RenderTexture.GetTemporary(rtW, rtH, 0, source.format);
            rt2.filterMode = FilterMode.Bilinear;
            Graphics.Blit(rt, rt2, blurMaterial, 1 + passOffs);
            RenderTexture.ReleaseTemporary(rt);
            rt = rt2;

            // horizontal blur
            rt2 = RenderTexture.GetTemporary(rtW, rtH, 0, source.format);
            rt2.filterMode = FilterMode.Bilinear;
            Graphics.Blit(rt, rt2, blurMaterial, 2 + passOffs);
            RenderTexture.ReleaseTemporary(rt);
            rt = rt2;
        }

        Graphics.Blit(rt, blurTexture);
        Graphics.Blit(source, destination);

        RenderTexture.ReleaseTemporary(rt);
    }
}