using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Mega.Scripts;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour {
    public enum TypeVideo {
        toBigMega,
        changePlate,
    }

    public static VideoController inst;

    public MeshRenderer meshRender;
    public VideoPlayer videoPlayer;
    public AudioSource audioSource;
   
    public AudioClip audioClip1;
    public VideoClip videoClip1;
    public AudioClip audioClip2;
    public VideoClip videoClip2;

    public Coroutine fadeCoroutine;
    public Vector3 startScale;

    public void Awake() {
        inst = this;
        startScale = transform.localScale;
    }

    public void Start() {
        videoPlayer.Stop();
        meshRender.material.color = new Color(1, 1, 1, 0);
    }

    public void SetSourse(TypeVideo typeVideo) {
        switch (typeVideo) {
            case TypeVideo.toBigMega:
                videoPlayer.clip = videoClip1;
                audioSource.clip = audioClip1;
                break;
            case TypeVideo.changePlate:
                videoPlayer.clip = videoClip2;
                audioSource.clip = audioClip2;
                break;
            default:
                throw new ArgumentOutOfRangeException("typeVideo", typeVideo, null);
        }
    }

    public void FadeOff(float timefade) {
        if (fadeCoroutine != null) {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(Fade(0, timefade));
        audioSource.Stop();
    }

    public void FadeOn (float timefade) {
        videoPlayer.frame = 1;
        audioSource.time = 0;
        videoPlayer.Play();
        audioSource.Play();
        if(fadeCoroutine != null) {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(Fade(1, timefade));
    }

    private IEnumerator Fade (float finalAlfa, float timeFade) {
        float time = 0;
        float currentAlfa = meshRender.material.color.a;
        Color ourColor = new Color(1, 1, 1, currentAlfa);
        while(time < timeFade) {
            ourColor.a = Mathf.Lerp(currentAlfa, finalAlfa, time / timeFade);
            meshRender.material.color = ourColor;
            time += Time.deltaTime;
            yield return null;
        }
        meshRender.material.color = new Color(1, 1, 1, finalAlfa);
        if (Math.Abs(finalAlfa) < 0.001f) {
            videoPlayer.Stop();
        }
        yield return null;
    }
}
