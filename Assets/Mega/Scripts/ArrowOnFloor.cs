﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Mega.Scripts {
    public class ArrowOnFloor : MonoBehaviour, IPointerDownHandler {
        public PointerEventData pointerEventData;
        public Collider collider;
        public MeshRenderer meshRenderer;
        public string[] arrayCollName = new string[3];


        public void Start() {
            ArrowController.inst.AddArrow(this);
            collider = GetComponent<BoxCollider>();
        }

        public void LookBack() {
            transform.LookAt(MegaCameraController.inst.posCamera);
            transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
            transform.localEulerAngles += new Vector3(0, 270, 0);
        }

        public void CheckPosition () {
            var dist = (transform.position - MegaCameraController.inst.posCamera.position).sqrMagnitude;
            if(dist < GlobalParams.arrowDistansSqrt && dist > GlobalParams.arrowMinDistansSqrt && MainLogic.inst.GetViewCurrentStates() ==
               ViewStates.firstFaceLook) {
                LookBack();
                if(!collider.enabled) {
                    StartCoroutine(IEnumChangeScale(Vector3.one));
                }
            } else {
                if(collider.enabled) {
                    StartCoroutine(IEnumChangeScale(Vector3.zero));
                }
            }
        }

        public void OnPointerDown (PointerEventData data) {
            pointerEventData = data;
            if(MegaCameraController.inst.isFirstLookScene) {
                StateFirstFaceLook.inst.MoveForThisArrowOnFloor(this);
            }
        }

        public void Fly() {
            //CheckPosition();
        }


        public IEnumerator IEnumChangeScale(Vector3 endScale) {
            if(endScale == Vector3.one) {
                collider.enabled = true;
                meshRenderer.enabled = true;
            }
            var startScale = meshRenderer.gameObject.transform.localScale;
            float time = GlobalParams.timeToFly * 0.3f;
            float currentTime = 0;
            while(currentTime < time) {
                var t = currentTime / time;
                meshRenderer.gameObject.transform.localScale = Vector3.Slerp(startScale, endScale, t);
                currentTime += Time.deltaTime;
                yield return null;
            }
            meshRenderer.gameObject.transform.localScale = endScale;
            if(endScale == Vector3.zero) {
                collider.enabled = false;
                meshRenderer.enabled = false;
            }
            yield return null;
        }

        public void Update() {
            CheckPosition();
            //     if(MainLogic.inst.GetViewCurrentStates() == ViewStates.firstFaceLook) {
            //         if(!collider.enabled) {
            //             collider.enabled = true;
            //         }
            //     } else {
            //         if(collider.enabled) {
            //             collider.enabled = false;
            //             meshRenderer.enabled = false;
            //         }
            //     }
        }

        //public void OnTriggerStay (Collider other) {
        //    if (arrayCollName[0] == String.Empty) {
        //        arrayCollName[0] = other.gameObject.name;
        //    }else {
        //        if (arrayCollName[1] == String.Empty) {
        //            arrayCollName[1] = other.gameObject.name;
        //        }else {
        //            if (arrayCollName[2] == String.Empty) {
        //                arrayCollName[2] = other.gameObject.name;
        //            }
        //        }
        //    }
        //}

        //public void LateUpdate() {
        //    bool flag = true;
        //    for (int i = 0; i < arrayCollName.Length; i++) {
        //        if (arrayCollName[i] == "FloorContact") {
        //            flag = false;
        //        }
        //    }
        //    if (flag) {
        //        meshRenderer.enabled = false;
        //    } else {
        //        if (!meshRenderer.enabled) {
        //            meshRenderer.enabled = true;
        //        }
        //    }
        //    for(int i = 0; i < arrayCollName.Length; i++) {
        //        arrayCollName[i] = String.Empty;
        //    }
        //}

    }
}