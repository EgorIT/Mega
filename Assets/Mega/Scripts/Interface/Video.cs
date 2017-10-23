using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Mega.Scripts;
using UnityEngine;
using UnityEngine.Video;

public class Video : MonoBehaviour {
    public static Video inst;
    public MeshRenderer mr;
    public VideoPlayer vp;
    public AudioSource ap;
    public Coroutine fadeCoroutine;
    public Vector3 startScale;
    private int framerate;

    public void Awake() {
        inst = this;
        startScale = transform.localScale;
        framerate = Application.targetFrameRate;
    }

    public void Start() {
        vp.Stop();
        mr.material.color = new Color(1, 1, 1, 0);
    }

    public void FadeOff() {
        if (fadeCoroutine != null) {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(Fade(0));
        ap.Stop();
        //Application.targetFrameRate = framerate;
    }

    public void FadeOn () {
        vp.frame = 1;
        ap.time = 0;
        vp.Play();
        ap.Play();
        if(fadeCoroutine != null) {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(Fade(1));
        //Application.targetFrameRate = (int)GetComponent<UnityEngine.Video.VideoPlayer>().frameRate;
    }

    private IEnumerator Fade (float finalAlfa) {
        float time = 0;
        float currentAlfa = mr.material.color.a;
        Color ourColor = new Color(1, 1, 1, currentAlfa);
        while(time < 1f) {
            ourColor.a = Mathf.Lerp(currentAlfa, finalAlfa, time / 1f);
            mr.material.color = ourColor;
            time += Time.deltaTime;
            yield return null;
        }
        mr.material.color = new Color(1, 1, 1, finalAlfa);
        if (Math.Abs(finalAlfa) < 0.001f) {
            vp.Stop();
        }
        yield return null;
    }
}
