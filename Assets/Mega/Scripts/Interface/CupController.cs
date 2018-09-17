using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CupController: MonoBehaviour {

    public Image[] cups;
    public Image[] sprites;
    public RectTransform content;
    public int? buffered;
    public BoxCollider bufferedCollider;
    //public RectTransform fullscreen;
    private float widthImage = 436 * 0.5f;
    private float hightImage = 436 * 0.5f;
    private float deltaImage = 470 * 0.5f;
    private float contentSizeX = 1884 * 2 * 0.5f;//1884

    public int currentI;

    public int maxI;
    public int minI;

    private void OnEnable () {
        buffered = null;
        content.sizeDelta = new Vector2(contentSizeX, 362);
        for(int i = 0; i < cups.Length; i++) {
            cups[i].rectTransform.sizeDelta = new Vector2(widthImage, hightImage);//218
            cups[i].rectTransform.anchoredPosition = new Vector2(i * (deltaImage), 0);//238
        }
    }

    public void ScaleFullscreen (int i) {
        Descaler.inst.currentCupController = this;
        currentI = i;
        Descaler.inst.fullScreenRect.gameObject.SetActive(true);
        Descaler.inst.fotoRect.GetComponent<Image>().sprite = sprites[i].sprite;
        StartCoroutine(ScaleUpFullscreen());
    }

    [EasyButtons.Button]
    public void SetNextImage() {
        Descaler.inst.btnNextFoto.gameObject.SetActive(true);
        Descaler.inst.btnBeforFoto.gameObject.SetActive(true);
        if (currentI == sprites.Length - 1) {
            Descaler.inst.btnNextFoto.gameObject.SetActive(false);
            return;
        }

        currentI++;
        if(currentI == sprites.Length - 1) {
            Descaler.inst.btnNextFoto.gameObject.SetActive(false);
        }

        Descaler.inst.fotoRect.GetComponent<Image>().sprite = sprites[currentI].sprite;
    }

    [EasyButtons.Button]
    public void SetBeforImage () {
        Descaler.inst.btnNextFoto.gameObject.SetActive(true);
        Descaler.inst.btnBeforFoto.gameObject.SetActive(true);
        if(currentI == 0) {
            return;
        }

        currentI--;
        if(currentI == 0) {
            Descaler.inst.btnBeforFoto.gameObject.SetActive(false);
        }

        Descaler.inst.fotoRect.GetComponent<Image>().sprite = sprites[currentI].sprite;
    }

    private IEnumerator ScaleUpFullscreen () {
        Descaler.inst.fotoRect.localScale = Vector2.zero;
        Descaler.inst.fullScreenRect.localScale = Vector2.zero;
        var  time = 0.2f;
        var currentTime = 0f;
        while(currentTime < time) {
            var t = currentTime / time;
            //content.sizeDelta = new Vector2(2028-minus, 362);
            Descaler.inst.fotoRect.localScale = Vector2.Lerp(Vector2.zero, Vector2.one * 0.5f, t);
            Descaler.inst.fullScreenRect.localScale = Vector2.Lerp(Vector2.zero, Vector2.one, t);
            currentTime += Time.deltaTime;
            yield return null;
        }

        Descaler.inst.fotoRect.localScale = Vector2.one * 0.5f;
        Descaler.inst.fullScreenRect.localScale = Vector2.one;
        yield return null;
    }

    public void Scale (int i) {
        Debug.Log(buffered);
        if(buffered == null) {
            buffered = i;
            Debug.Log(buffered);
            StartCoroutine(ScaleUP(i));
        } else {
            if(buffered == i) {
                buffered = null;
                StartCoroutine(ScaleDown(i));
            } else {
                StartCoroutine(ReScale(Convert.ToInt32(buffered), i));
                buffered = i;
            }
        }
    }

    public IEnumerator ReScale (int num1, int num2) {
        float time = 0.5f;
        BoxCollider bc = cups[num1].gameObject.GetComponent<BoxCollider>();
        bufferedCollider = null;//bc;
        while(time > 0) {
            float counted = Mathf.Lerp(widthImage, 362, time / .5f);
            float minus = counted - widthImage;
            //content.sizeDelta = new Vector2(2028-minus, 362);
            cups[num1].rectTransform.sizeDelta = new Vector2(counted, counted);
            bc.size = cups[num1].rectTransform.sizeDelta;
            bc.center = cups[num1].rectTransform.sizeDelta / 2;
            for(int i = num1 + 1; i < cups.Length; i++) {
                cups[i].rectTransform.anchoredPosition = new Vector2(deltaImage * i + minus, 0);//181
            }
            time -= Time.deltaTime;
            yield return null;
        }
        cups[num1].rectTransform.sizeDelta = new Vector2(widthImage, widthImage);

        bc.size = cups[num1].rectTransform.sizeDelta;
        bc.center = cups[num1].rectTransform.sizeDelta / 2;

        for(int i = num1 + 1; i < cups.Length; i++) {
            cups[i].rectTransform.anchoredPosition = new Vector2(deltaImage * i, 0);
        }

        time = 0.5f;
        bc = cups[num2].gameObject.GetComponent<BoxCollider>();
        bufferedCollider = bc;
        while(time > 0) {
            float counted = Mathf.Lerp(362, widthImage, time / .5f);
            float minus = counted - widthImage;

            cups[num2].rectTransform.sizeDelta = new Vector2(counted, counted);

            bc.size = cups[num2].rectTransform.sizeDelta;
            bc.center = cups[num2].rectTransform.sizeDelta / 2;

            //
            bufferedCollider = bc;

            for(int i = num2 + 1; i < cups.Length; i++) {
                cups[i].rectTransform.anchoredPosition = new Vector2(deltaImage * i + minus, 0);
            }
            time -= Time.deltaTime;
            yield return null;
        }
        content.sizeDelta = new Vector2(contentSizeX + 144, 362);
        cups[num2].rectTransform.sizeDelta = new Vector2(362, 362);
        bc.size = cups[num2].rectTransform.sizeDelta;
        bc.center = cups[num2].rectTransform.sizeDelta / 2;
        for(int i = num2 + 1; i < cups.Length; i++) {
            cups[i].rectTransform.anchoredPosition = new Vector2(deltaImage * i + 144, 0);
        }

        yield return null;
    }

    public IEnumerator ScaleDown (int num) {
        float time = 0.5f;
        BoxCollider bc = cups[num].gameObject.GetComponent<BoxCollider>();
        bufferedCollider = null;
        while(time > 0) {
            float counted = Mathf.Lerp(widthImage, 362, time / .5f);
            float minus = counted - widthImage;
            content.sizeDelta = new Vector2(2028 - minus, 362);
            cups[num].rectTransform.sizeDelta = new Vector2(counted, counted);

            bc.size = cups[num].rectTransform.sizeDelta;
            bc.center = cups[num].rectTransform.sizeDelta / 2;

            //
            //bufferedCollider = bc;

            for(int i = num + 1; i < cups.Length; i++) {
                cups[i].rectTransform.anchoredPosition = new Vector2(deltaImage * i + minus, 0);
            }
            time -= Time.deltaTime;
            yield return null;
        }
        content.sizeDelta = new Vector2(contentSizeX, 362);
        cups[num].rectTransform.sizeDelta = new Vector2(widthImage, widthImage);
        bc.size = cups[num].rectTransform.sizeDelta;
        bc.center = cups[num].rectTransform.sizeDelta / 2;

        for(int i = num + 1; i < cups.Length; i++) {
            cups[i].rectTransform.anchoredPosition = new Vector2(deltaImage * i, 0);
        }
        yield return null;
    }

    public IEnumerator ScaleUP (int num) {
        float time = 0.5f;
        BoxCollider bc = cups[num].gameObject.GetComponent<BoxCollider>();
        bufferedCollider = bc;
        while(time > 0) {
            float counted = Mathf.Lerp(362, widthImage, time / .5f);
            float minus = counted - widthImage;
            content.sizeDelta = new Vector2(contentSizeX + minus, 362);
            cups[num].rectTransform.sizeDelta = new Vector2(counted, counted);


            bc.size = cups[num].rectTransform.sizeDelta;
            bc.center = cups[num].rectTransform.sizeDelta / 2;

            //
            bufferedCollider = bc;

            for(int i = num + 1; i < cups.Length; i++) {
                cups[i].rectTransform.anchoredPosition = new Vector2(deltaImage * i + minus, 0);
            }
            time -= Time.deltaTime;
            yield return null;
        }
        content.sizeDelta = new Vector2(contentSizeX + 144, 362);
        cups[num].rectTransform.sizeDelta = new Vector2(362, 362);
        bc = cups[num].gameObject.GetComponent<BoxCollider>();
        bc.size = cups[num].rectTransform.sizeDelta;
        bc.center = cups[num].rectTransform.sizeDelta / 2;

        for(int i = num + 1; i < cups.Length; i++) {
            cups[i].rectTransform.anchoredPosition = new Vector2(deltaImage * i + 144, 0);
        }

        yield return null;
    }
}
