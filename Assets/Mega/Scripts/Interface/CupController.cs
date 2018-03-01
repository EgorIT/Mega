using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CupController : MonoBehaviour {

    public Image[] cups;
    public RectTransform content;
    public int? buffered;
    public BoxCollider bufferedCollider;

    private void OnEnable () {
        buffered = null;
        content.sizeDelta = new Vector2(1884, 362);
        for(int i = 0; i < cups.Length; i++) {
            cups[i].rectTransform.sizeDelta = new Vector2(218, 218);
            cups[i].rectTransform.anchoredPosition = new Vector2(i * (238), -181);
        }
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
            float counted = Mathf.Lerp(218, 362, time / .5f);
            float minus = counted - 218;
            //content.sizeDelta = new Vector2(2028-minus, 362);
            cups[num1].rectTransform.sizeDelta = new Vector2(counted, counted);
            bc.size = cups[num1].rectTransform.sizeDelta;
            bc.center = cups[num1].rectTransform.sizeDelta / 2;
            for(int i = num1 + 1; i < cups.Length; i++) {
                cups[i].rectTransform.anchoredPosition = new Vector2(238 * i + minus, -181);
            }
            time -= Time.deltaTime;
            yield return null;
        }
        //content.sizeDelta = new Vector2(1884, 362);
        cups[num1].rectTransform.sizeDelta = new Vector2(218, 218);

        bc.size = cups[num1].rectTransform.sizeDelta;
        bc.center = cups[num1].rectTransform.sizeDelta / 2;

        for(int i = num1 + 1; i < cups.Length; i++) {
            cups[i].rectTransform.anchoredPosition = new Vector2(238 * i, -181);
        }

        time = 0.5f;
        bc = cups[num2].gameObject.GetComponent<BoxCollider>();
        bufferedCollider = bc;
        while(time > 0) {
            float counted = Mathf.Lerp(362, 218, time / .5f);
            float minus = counted - 218;
            //content.sizeDelta = new Vector2(1884+minus, 362);

            cups[num2].rectTransform.sizeDelta = new Vector2(counted, counted);

            bc.size = cups[num2].rectTransform.sizeDelta;
            bc.center = cups[num2].rectTransform.sizeDelta / 2;

            //
            bufferedCollider = bc;

            for(int i = num2 + 1; i < cups.Length; i++) {
                cups[i].rectTransform.anchoredPosition = new Vector2(238 * i + minus, -181);
            }
            time -= Time.deltaTime;
            yield return null;
        }
        content.sizeDelta = new Vector2(1884 + 144, 362);
        cups[num2].rectTransform.sizeDelta = new Vector2(362, 362);
        bc.size = cups[num2].rectTransform.sizeDelta;
        bc.center = cups[num2].rectTransform.sizeDelta / 2;
        for(int i = num2 + 1; i < cups.Length; i++) {
            cups[i].rectTransform.anchoredPosition = new Vector2(238 * i + 144, -181);
        }

        yield return null;
    }

    public IEnumerator ScaleDown (int num) {
        float time = 0.5f;
        BoxCollider bc = cups[num].gameObject.GetComponent<BoxCollider>();
        bufferedCollider = null;
        while(time > 0) {
            float counted = Mathf.Lerp(218, 362, time / .5f);
            float minus = counted - 218;
            content.sizeDelta = new Vector2(2028 - minus, 362);
            cups[num].rectTransform.sizeDelta = new Vector2(counted, counted);

            bc.size = cups[num].rectTransform.sizeDelta;
            bc.center = cups[num].rectTransform.sizeDelta / 2;

            //
            //bufferedCollider = bc;

            for(int i = num + 1; i < cups.Length; i++) {
                cups[i].rectTransform.anchoredPosition = new Vector2(238 * i + minus, -181);
            }
            time -= Time.deltaTime;
            yield return null;
        }
        content.sizeDelta = new Vector2(1884, 362);
        cups[num].rectTransform.sizeDelta = new Vector2(218, 218);
        bc.size = cups[num].rectTransform.sizeDelta;
        bc.center = cups[num].rectTransform.sizeDelta / 2;

        for(int i = num + 1; i < cups.Length; i++) {
            cups[i].rectTransform.anchoredPosition = new Vector2(238 * i, -181);
        }
        yield return null;
    }

    public IEnumerator ScaleUP (int num) {
        float time = 0.5f;
        BoxCollider bc = cups[num].gameObject.GetComponent<BoxCollider>();
        bufferedCollider = bc;
        while(time > 0) {
            float counted = Mathf.Lerp(362, 218, time / .5f);
            float minus = counted - 218;
            content.sizeDelta = new Vector2(1884 + minus, 362);
            cups[num].rectTransform.sizeDelta = new Vector2(counted, counted);


            bc.size = cups[num].rectTransform.sizeDelta;
            bc.center = cups[num].rectTransform.sizeDelta / 2;

            //
            bufferedCollider = bc;

            for(int i = num + 1; i < cups.Length; i++) {
                cups[i].rectTransform.anchoredPosition = new Vector2(238 * i + minus, -181);
            }
            time -= Time.deltaTime;
            yield return null;
        }
        content.sizeDelta = new Vector2(1884 + 144, 362);
        //content.sizeDelta = new Vector2(1884+154, 362);
        cups[num].rectTransform.sizeDelta = new Vector2(362, 362);
        bc = cups[num].gameObject.GetComponent<BoxCollider>();
        bc.size = cups[num].rectTransform.sizeDelta;
        bc.center = cups[num].rectTransform.sizeDelta / 2;

        for(int i = num + 1; i < cups.Length; i++) {
            cups[i].rectTransform.anchoredPosition = new Vector2(238 * i + 144, -181);
        }

        yield return null;
    }

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }
}
