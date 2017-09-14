using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Mega.Scripts;
using UnityEngine;
using UnityEngine.Video;

public class Video1 : MonoBehaviour {
    public static Video1 inst;
    public SpriteRenderer mr;
    //public VideoPlayer vp;
    private int num;

    public Coroutine fadeCoroutine;

    public Vector3 startScale;

    public void Awake() {
        inst = this;
        startScale = transform.localScale;
        //Debug.Log(vp.frameCount);
        /*vp.frame = 5;
        vp.Play();
        vp.Pause();*/
    }

    private void Update()
    {
        if (num>105)
        {
            num = 0;
        }
        //vp.frame = num;
        num++;
    }

    public void FadeOff() {
        //vp.Play();
        if (fadeCoroutine != null) {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(Fade(0));
    }

    public void FadeOn () {
        //vp.Play();
        if(fadeCoroutine != null) {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(Fade(1));
    }

    private IEnumerator Fade (float finalAlfa) {
        float time = 0;
        float currentAlfa = mr.material.color.a;
        Color ourColor = new Color(1, 1, 1, currentAlfa);
        while(time < 1f) {
            //transform.localScale = (startScale * MegaCameraController.inst.listCamerasOrto[0].orthographicSize) / 50;
            ourColor.a = Mathf.Lerp(currentAlfa, finalAlfa, time / 1f);
            mr.material.color = ourColor;
            time += Time.deltaTime;
            yield return null;
        }
        //transform.localScale = startScale;
        mr.material.color = new Color(1, 1, 1, finalAlfa);
        if (Math.Abs(finalAlfa) < 0.001f) {
            //vp.Stop();
        }
        yield return null;
    }
}
