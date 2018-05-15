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
        public float testX;
        public float testZ;

        public GameObject iconShop;
        public Coroutine showCoro;
        public TextMesh text;
        public Text text1;
        public RectTransform rt;
        public string shopName;
        public ShopCap shopCap;

        public Vector3 startPos;


        public void Awake () {
        }

        public void Start () {
            TableController.inst.listLittleShops.Add(this);
        }

        public void ClickTable() {
           // shopCap.pointerMoveToShop.GoToThisShop();
        }

        public void SetName(string var) {
            TextGenerator textGen = new TextGenerator();
            TextGenerationSettings generationSettings = text1.GetGenerationSettings(text1.rectTransform.rect.size); 
            float width = textGen.GetPreferredWidth(var, generationSettings);
            float height = textGen.GetPreferredHeight(var, generationSettings);
            rt.sizeDelta = new Vector2(width/6,30);
            shopName = var;
            if(text != null) {
                text.text = var;
            }
            if(text1 != null) {
                text1.text = var;
            }
        }

        public void DisableTable() {
            if (showCoro != null) {
                StopCoroutine(showCoro);
                showCoro = null;
            }
            showCoro = StartCoroutine(IEnumChangeScale(Vector3.zero));
        }

        public void EnableTable () {
            //if(!shopCap.gameObject.activeInHierarchy) {
            //    return;
            //}
            if(showCoro != null) {
                StopCoroutine(showCoro);
                showCoro = null;
            }
            showCoro = StartCoroutine(IEnumChangeScale(GlobalParams.scaleIconShop));
        }

        //public void Update() {
        //    testX = transform.position.x;
        //    testZ = transform.position.z;
        //}

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
