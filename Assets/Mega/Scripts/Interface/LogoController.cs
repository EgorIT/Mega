using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Mega.Scripts;
using UnityEngine;
using UnityEngine.Video;

public class LogoController : MonoBehaviour {
    public static LogoController inst;
    public SpriteRenderer mr;
    //public VideoPlayer vp;

    public Coroutine fadeCoroutine;

    public Vector3 startScale;

    

    public void Awake () {
        inst = this;
        startScale = transform.localScale;
    }

    public void Update () {

    }

    public void FadeOff (float timeFade) {
        if(fadeCoroutine != null) {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(Fade(0, timeFade));
    }

    public void FadeOn (float timeFade) {
        if(fadeCoroutine != null) {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(Fade(1, timeFade));
    }

    private IEnumerator Fade (float finalAlfa, float timeFade) {
        float time = 0;
        float currentAlfa = mr.material.color.a;
        Color ourColor = new Color(1, 1, 1, currentAlfa);
        while(time < timeFade) {
            ourColor.a = Mathf.Lerp(currentAlfa, finalAlfa, time / timeFade);
            mr.material.color = ourColor;
            time += Time.deltaTime;
            yield return null;
        }
        mr.material.color = new Color(1, 1, 1, finalAlfa);
        if(Math.Abs(finalAlfa) < 0.001f) {
        }
        yield return null;
    }
}
