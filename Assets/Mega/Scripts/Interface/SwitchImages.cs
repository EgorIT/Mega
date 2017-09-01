using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SwitchImages : MonoBehaviour {


    public List<Image> images;
    public List<RectTransform> frames;
    public Mask mask;
    public Table table;
    private bool big = false;
    private int bigNow = 0;

    void Update () {
        if(Input.GetKeyDown("space")) {
            Switch();
        }
    }

    public void Switch () {
        float delay = 0;
        foreach(Image image in images) {
            StartCoroutine(ChangeColor(image, delay));
            delay += 0.01f;
        }
    }

    private IEnumerator ChangeColor (Image img, float delay) {
        yield return new WaitForSeconds(delay);
        float time = 0f;
        float time2 = 0.1f;
        Color current = img.color;
        while(time < time2) {

            img.color = new Color(current.r, current.g, current.b, Mathf.Lerp(1, 0, time / time2));
            Debug.Log(img.color);
            Debug.Log(Mathf.Lerp(255, 0, time / time2));
            time += Time.deltaTime;
            yield return null;
        }
        img.color = new Color(current.r, current.g, current.b, 0);

        current = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        //Debug.Log(current);
        //img.color = new Color(current.r,current.g,current.b, 0);
        time = 0;
        while(time < time2) {

            img.color = new Color(current.r, current.g, current.b, Mathf.Lerp(0, 1, time / time2));
            Debug.Log(img.color);
            Debug.Log(Mathf.Lerp(255, 0, time / time2));
            time += Time.deltaTime;
            yield return null;
        }
        img.color = new Color(current.r, current.g, current.b, 1);
        yield return null;
    }

    private void Start () {
        foreach(Image image in images) {
            frames.Add(image.GetComponent<RectTransform>());
        }
    }

    public void MoveImages (int num) {
        /*foreach (RectTransform rectTransform in frames)
		{
			rectTransform.GetComponent<Button>().interactable = false;
		}*/
        if(num == 6) {
            mask.enabled = false;
        } else {
            mask.enabled = true;
        }
        if(!big) {
            StartCoroutine(ScaleUp(frames[num]));
            for(int i = num + 1; i < frames.Count; i++) {
                StartCoroutine(MoveRight(frames[i]));
            }
            bigNow = num;
            big = true;
        } else {
            StartCoroutine(RefreshMoves(num));/*
			StartCoroutine(ScaleDown(frames[bigNow]));
			for (int i = bigNow + 1; i < frames.Count; i++)
			{
				StartCoroutine(MoveLeft(frames[i]));
			}
			big = false;*/
        }
        
        table.Countdown();
    }

    private IEnumerator RefreshMoves (int num) {
        Coroutine sd = StartCoroutine(ScaleDown(frames[bigNow]));
        Coroutine ml = sd;//kostyl
        for(int i = bigNow + 1; i < frames.Count; i++) {
            ml = StartCoroutine(MoveLeft(frames[i]));
        }
        big = false;
        yield return sd;
        yield return ml;
        if(num != bigNow)
            MoveImages(num);
    }


    public IEnumerator ScaleUp (RectTransform rt) {
        float time = 0f;
        float time2 = 0.1f;
        Vector2 targetScale = new Vector2(3f, 3f);

        while(time < time2) {
            rt.localScale = Vector2.Lerp(Vector2.one, targetScale, time / time2);
            //Debug.Log(Mathf.Lerp(255, 0, time / time2));
            time += Time.deltaTime;
            yield return null;
        }
        rt.localScale = targetScale;
        yield return null;
    }

    public IEnumerator ScaleDown (RectTransform rt) {
        float time = 0f;
        float time2 = 0.1f;
        Vector2 targetScale = new Vector2(3f, 3f);

        while(time < time2) {
            rt.localScale = Vector2.Lerp(targetScale, Vector2.one, time / time2);
            //Debug.Log(Mathf.Lerp(255, 0, time / time2));
            time += Time.deltaTime;
            yield return null;
        }
        rt.localScale = Vector2.one;
        yield return null;
    }

    public IEnumerator MoveRight (RectTransform rt) {
        float time = 0f;
        float time2 = 0.1f;
        Vector2 startPos = rt.localPosition;
        Vector2 targetPos = startPos + new Vector2(400, 0);

        while(time < time2) {
            rt.localPosition = Vector2.Lerp(startPos, targetPos, time / time2);
            //Debug.Log(Mathf.Lerp(255,0, time/time2));
            time += Time.deltaTime;
            yield return null;
        }
        rt.localPosition = targetPos;
        yield return null;
    }

    public IEnumerator MoveLeft (RectTransform rt) {
        float time = 0f;
        float time2 = 0.1f;
        Vector2 startPos = rt.localPosition;
        Vector2 targetPos = startPos - new Vector2(400, 0);

        while(time < time2) {
            rt.localPosition = Vector2.Lerp(startPos, targetPos, time / time2);
            //Debug.Log(Mathf.Lerp(255, 0, time / time2));
            time += Time.deltaTime;
            yield return null;
        }
        rt.localPosition = targetPos;
        yield return null;
    }
}
