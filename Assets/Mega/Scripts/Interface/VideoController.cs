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
        kids,
        commercional
    }

    public static VideoController inst;

    public MeshRenderer meshRender;
    public VideoPlayer videoPlayer;
    public AudioSource audioSource;
   
    public AudioClip audioCliptoBigMega;
    public VideoClip videoCliptoBigMega;
    public AudioClip audioClipchangePlate;
    public VideoClip videoClipchangePlate;

    public AudioClip audioClipkids;
    public VideoClip videoClipkids;

    public AudioClip audioClipcommercional;
    public VideoClip videoClipcommercional;

    public Coroutine fadeCoroutine;
    public Vector3 startScale;

    public void Awake() {
        inst = this;
        startScale = transform.localScale;
    }

    public void Start() {
        videoPlayer.Stop();
        videoPlayer.SetDirectAudioVolume(0, 0);
        meshRender.material.color = new Color(1, 1, 1, 0);
    }

    public void SetSourse(TypeVideo typeVideo) {
        switch (typeVideo) {
            case TypeVideo.toBigMega:
                videoPlayer.clip = videoCliptoBigMega;
                audioSource.clip = audioCliptoBigMega;
                break;
            case TypeVideo.changePlate:
                videoPlayer.clip = videoClipchangePlate;
                audioSource.clip = audioClipchangePlate;
                break;
            case TypeVideo.kids:
                videoPlayer.clip = videoClipkids;
                audioSource.clip = audioClipkids;
                break;
            case TypeVideo.commercional:
                videoPlayer.clip = videoClipcommercional;
                audioSource.clip = audioClipcommercional;
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
        videoPlayer.SetDirectAudioVolume(0, 0);
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
