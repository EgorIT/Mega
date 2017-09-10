using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Mega.Scripts {
    /*public enum AngelsLook {
        leftTop,
        rightTop,
        leftDown,
        rightDown
    }*/

    public class TableShop : MonoBehaviour {
        public GameObject iconShop;
        public Coroutine showCoro;
        public TextMesh text;
        public Text text1;
        public string shopName;

        public void Awake () {
        }

        public void Start () {
           
            TableController.inst.listLittleShops.Add(this);

        }

        public void SetName(string var) {
            shopName = var;
            if(text != null) {
                text.text = var;
            }
            if(text1 != null) {
                text1.text = var;
            }
        }

        public void DisableShop() {
            if (showCoro != null) {
                StopCoroutine(showCoro);
                showCoro = null;
            }
            showCoro = StartCoroutine(IEnumChangeScale(Vector3.zero));
        }

        public void EnableShop () {
            if(showCoro != null) {
                StopCoroutine(showCoro);
                showCoro = null;
            }
            showCoro = StartCoroutine(IEnumChangeScale(GlobalParams.scaleIconShop));
        }

        public IEnumerator IEnumChangeScale (Vector3 endScale) {
            var startScale = iconShop.transform.localScale;

            float time = GlobalParams.timeToShowIcons;
            float currentTime = 0;
            while(currentTime < time) {
               // Debug.Log(shopName + " " + iconShop.transform.localScale.x + " " + startScale + " " + endScale);
                var t = currentTime / time;
                iconShop.transform.localScale = Vector3.Slerp(startScale, endScale, t);

                currentTime += Time.deltaTime;
                yield return null;
            }
            iconShop.transform.localScale = endScale;
        }

    }
}
