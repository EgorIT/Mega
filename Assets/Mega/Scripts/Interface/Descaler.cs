using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Descaler: MonoBehaviour {
    public static Descaler inst;

    public RectTransform fullScreenRect;
    public RectTransform fotoRect;

    public Button btnNextFoto;
    public Button btnBeforFoto;
    public CupController currentCupController;
    public Coroutine coroScale;

    public void Awake() {
        inst = this;
    }

    public void Start() {
        fullScreenRect.localScale = Vector2.zero;
        fotoRect.localScale = Vector2.zero;
        fullScreenRect.gameObject.SetActive(false);
        btnNextFoto.onClick.AddListener(() => { currentCupController.SetNextImage();});
        btnBeforFoto.onClick.AddListener(() => { currentCupController.SetBeforImage(); });
    }

    public void StopCoroScale() {
        if (coroScale != null) {
            StopCoroutine(coroScale);
            coroScale = null;
        }
    }

    public void ScaleFullscreen () {
        StopCoroScale();
        //fullscreen.GetComponent<Image>().sprite = sprites[i].sprite;
        StartCoroutine(ScaleToZero());
    }

    private IEnumerator ScaleToOne () {
        fullScreenRect.localScale = Vector2.one;
        fotoRect.localScale = Vector2.one;
        float time = 0.2f;
        float currentTime = 0f;
        while(currentTime < time) {
            fullScreenRect.localScale = Vector2.Lerp(Vector2.one * 0.5f, Vector2.zero, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        }
        fullScreenRect.localScale = Vector2.zero;
        fotoRect.localScale = Vector2.zero;
        yield return null;
        fullScreenRect.gameObject.SetActive(false);
    }

    private IEnumerator ScaleToZero () {
        fullScreenRect.localScale = Vector2.one;
        fotoRect.localScale = Vector2.one;
        float time = 0.2f;
        float currentTime = 0f;
        while(currentTime < time) {
            fullScreenRect.localScale = Vector2.Lerp(Vector2.one * 0.5f, Vector2.zero, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        }
        fullScreenRect.localScale = Vector2.zero;
        fotoRect.localScale = Vector2.zero;
        yield return null;
        fullScreenRect.gameObject.SetActive(false);
    }
}
