using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HexagonExample : MonoBehaviour
{
    public float s = 0.5f;
    IEnumerator Start()
    {
        var image = GetComponent<Image>();
        if (image != null)
        {
            while (true)
            {
                yield return this;
                image.color = Color.HSVToRGB(Mathf.PingPong(Time.time * 0.1f, 1f), s, 1f);
            }
        }

#if NGUI
        var imageN = GetComponent<UISprite>();
        if (imageN != null)
        {
            while (true)
            {
                yield return this;
                imageN.color = Color.HSVToRGB(Mathf.PingPong(Time.time * 0.1f, 1f), s, 1f);
            }
        }
#endif
    }
}