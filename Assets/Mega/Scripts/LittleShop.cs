using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Mega.Scripts {
    public enum AngelsLook {
        leftTop,
        rightTop,
        leftDown,
        rightDown
    }

    public class LittleShop : MonoBehaviour, IPointerDownHandler {

        public AngelsLook angelsLook;
        public LittleShopController littleShopController;
        public List<BoxCollider> listCollider;
        public GameObject iconShop;

        public Coroutine showCoro;
        public List<Transform> listLeftTopAnc;
        public List<Transform> listRightDownAnc;

        public void Awake () {
            var colliders = GetComponents<BoxCollider>();
            for (int i = 0; i < colliders.Length; i++) {
                listCollider.Add(colliders[i]);

            }
        }

        public void Start () {


        }

        public void OnPointerDown (PointerEventData data) {
            Debug.Log(gameObject.name);
            littleShopController.SetLittleShop(this);
            var v3 = new Vector3();

            /*switch (angelsLook) {
                case AngelsLook.leftTop:
                    v3 = GlobalParams.startLocalEulerAnglesForCamera + new Vector3(0, 90, 0);
                    break;
                case AngelsLook.rightTop:
                    v3 = GlobalParams.startLocalEulerAnglesForCamera + new Vector3(0, 180, 0);
                    break;
                case AngelsLook.leftDown:
                    v3 = GlobalParams.startLocalEulerAnglesForCamera + new Vector3(0, 0, 0);
                    break;
                case AngelsLook.rightDown:
                    v3 = GlobalParams.startLocalEulerAnglesForCamera + new Vector3(0, 270, 0);
                    break;
            }*/

            //MegaCameraController.inst.SetNewPosCamera(transform.position, v3, -150, GlobalParams.sizeOrtocameraLittleShop);
        }

        public void DisableShop() {
            for(int i = 0; i < listCollider.Count; i++) {
                listCollider[i].enabled = false;
            }
            if (showCoro != null) {
                StopCoroutine(showCoro);
                showCoro = null;
            }
            showCoro = StartCoroutine(IEnumChangeScale(Vector3.zero));
            //iconShop.SetActive(false);
        }

        public IEnumerator IEnumChangeScale (Vector3 endScale) {
            if (endScale != Vector3.zero) {
                yield return new WaitForSeconds(1.8f);
            }
            var startScale = iconShop.transform.localScale;

            float time = GlobalParams.timeToShowIcons;
            float currentTime = 0;
            while(currentTime < time) {
                var t = currentTime / time;
                iconShop.transform.localScale = Vector3.Slerp(startScale, endScale, t);

                currentTime += Time.deltaTime;
                yield return null;
            }
            iconShop.transform.localScale = endScale;
            /*if (endScale == Vector3.zero) {
                
            }*/
        }

        public void EnableShop() {
            for(int i = 0; i < listCollider.Count; i++) {
                listCollider[i].enabled = true;
            }
            if(showCoro != null) {
                StopCoroutine(showCoro);
                showCoro = null;
            }
            showCoro = StartCoroutine(IEnumChangeScale(GlobalParams.scaleIconShop));
            //iconShop.SetActive(true);
        }

        public void Update() {
            //iconShop.transform.LookAt(MegaCameraController.inst.listCamerasOrto[0].transform);
            //iconShop.transform.Rotate(new Vector3(0, 180, 0));
        }

    }
}