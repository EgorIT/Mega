using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeButton : MonoBehaviour {
    public Timeline tl;
    public Sprite normal;
    public Sprite pressed;
    public int number;
    public bool year;

    private Image image;

    public void Clicked () {
        tl.SwitchTimes(number);
    }

    void Start () {
        image = gameObject.GetComponent<Image>();
    }

    public void SetNormal () {
        image.sprite = normal;
    }

    public void SetPressed () {
        image.sprite = pressed;
    }
}
