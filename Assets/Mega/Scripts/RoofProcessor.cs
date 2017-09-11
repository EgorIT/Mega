using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering;

public class RoofProcessor : MonoBehaviour {
    public static RoofProcessor inst;
    private List<Material> materials = new List<Material>();

    public void Awake() {
        inst = this;
    }

    private enum BlendMode {
        Opaque,
        Cutout,
        Fade,
        Transparent
    }

    public void DoTransparent () {
        StartCoroutine(TransparentCoroutine());
    }

    private IEnumerator TransparentCoroutine () {
        foreach(Material material in materials) {
            //Debug.Log(material.GetFloat("_Mode"));
            if(material.GetFloat("_Mode") == 0f) {
                material.SetFloat("_Mode", 2f);
                Switcher(material, BlendMode.Fade);
                //material.renderQueue = 3000;
            }
        }

        float time = 0;
        float time2 = .3f;
        while(time < time2) {
            float temp = Mathf.Lerp(1, 0, time / time2);

            foreach(Material material in materials) {
                Color mColor = material.color;
                Color newColor = new Color(mColor.r, mColor.g, mColor.b, temp);
                //Color newColor = new Color(mColor.r,mColor.g, mColor.b,temp);
                material.SetColor("_Color", newColor);
                //material.EnableKeyword();
            }
            /*for (int i = 0; i <= line.anchoredPosition.x/180; i++)
			{
				timeButtons[i].SetPressed();
			}
			for (int i = (int)line.anchoredPosition.x/180; i < 9; i++)
			{
				timeButtons[i].SetNormal();
			}*/
            time += Time.deltaTime;
            yield return null;
        }

        foreach(Material material in materials) {
            Color mColor = material.color;
            Color newColor = new Color(mColor.r, mColor.g, mColor.b, 0);
            //Color newColor = new Color(mColor.r,mColor.g, mColor.b,temp);
            material.SetColor("_Color", newColor);
        }
        yield return null;
    }

    public void DoStandard () {
        StartCoroutine(NonTransparentCoroutine());
    }


    private IEnumerator NonTransparentCoroutine () {


        float time = 0;
        float time2 = .3f;
        while(time < time2) {
            float temp = Mathf.Lerp(0, 1, time / time2);

            foreach(Material material in materials) {
                Color mColor = material.color;
                Color newColor = new Color(mColor.r, mColor.g, mColor.b, temp);
                //Color newColor = new Color(mColor.r,mColor.g, mColor.b,temp);
                material.SetColor("_Color", newColor);
            }
            /*for (int i = 0; i <= line.anchoredPosition.x/180; i++)
			{
				timeButtons[i].SetPressed();
			}
			for (int i = (int)line.anchoredPosition.x/180; i < 9; i++)
			{
				timeButtons[i].SetNormal();
			}*/
            time += Time.deltaTime;
            yield return null;
        }

        foreach(Material material in materials) {
            Color mColor = material.color;
            Color newColor = new Color(mColor.r, mColor.g, mColor.b, 1);
            //Color newColor = new Color(mColor.r,mColor.g, mColor.b,temp);
            material.SetColor("_Color", newColor);
        }

        foreach(Material material in materials) {
            if(material.GetFloat("_Mode") == 2f) {
                material.SetFloat("_Mode", 0f);
                Switcher(material, BlendMode.Opaque);
                //material.renderQueue = 2000;
            }
        }
        yield return null;
    }
    // Use this for initialization
    void Start () {
        List<MeshRenderer> mrl = new List<MeshRenderer>();
        GameObject go = GameObject.FindWithTag("Roof");
        mrl = go.GetComponentsInChildren<MeshRenderer>().ToList();

        mrl.ForEach(x => {
            x.materials.ToList().ForEach(y => {
                materials.Add(y);
                //Debug.Log(y.name+" "+y.GetInstanceID());
            });

        });

    }

    private void Switcher (Material material, BlendMode blendMode) {
        switch(blendMode) {
            case BlendMode.Opaque:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt("_ZWrite", 1);
                material.DisableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = -1;
                break;
            case BlendMode.Cutout:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt("_ZWrite", 1);
                material.EnableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 2450;
                break;
            case BlendMode.Fade:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt("_ZWrite", 0);
                material.DisableKeyword("_ALPHATEST_ON");
                material.EnableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 3000;
                break;
            case BlendMode.Transparent:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt("_ZWrite", 0);
                material.DisableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 3000;
                break;
        }
    }

    /*void Update () {
        if(Input.GetKeyDown("p")) {
            DoTransparent();
        }

        if(Input.GetKeyDown("o")) {
            DoStandard();
        }
    }*/
}
