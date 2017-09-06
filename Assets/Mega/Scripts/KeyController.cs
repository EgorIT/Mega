﻿using System;
using UnityEngine;

namespace Assets.Mega.Scripts {
    public class KeyController : MonoBehaviour {
        private float speedKey = 2;
        private float speedTouch = 0.003f;//0.003f;
        private float speedSwipeMouse = -0.02f;//0.003f;
        private float speedZoom = 0.003f;//0.003f;
        private float speedZoomWheel = 5f;//0.003f;

        //public bool isFirstLookScene;

        public void Start () {
            //Debug.Log("KeyController LIVE!!!");
        }

        public Vector3 lastPosCur;
        public Vector3 currentPosCur;

        public float i = 0;

        public void Update () {
            float x = 0;
            float y = 0;
            Touch touch = new Touch();
            touch.deltaPosition = Vector2.zero;

            bool swipeFlag = false;
            bool pinchFlag = false;
            float deltaMagnitudeDiff = 0;

            if(Input.GetAxis("Mouse ScrollWheel") > 0) {
                pinchFlag = true;
                if(Math.Abs(MegaCameraController.inst.disCamera.localPosition.z) < 0.0001) {
                    deltaMagnitudeDiff = -1f * speedZoomWheel;
                } else {
                    deltaMagnitudeDiff = -1f * speedZoomWheel *
                        (MegaCameraController.inst.disCamera.localPosition.z / GlobalParams.factorPerspStabilization);
                }

            }

            if(Input.GetAxis("Mouse ScrollWheel") < 0) {
                pinchFlag = true;
                if(Math.Abs(MegaCameraController.inst.disCamera.localPosition.z) < 0.0001) {
                    deltaMagnitudeDiff = 1f * speedZoomWheel;
                } else {
                    deltaMagnitudeDiff = 1f * speedZoomWheel *
                        (MegaCameraController.inst.disCamera.localPosition.z / GlobalParams.factorPerspStabilization);
                }
            }

            if(Input.GetKey(KeyCode.Mouse0)) {
                swipeFlag = true;
                var v3 = Input.mousePosition - lastPosCur;
                touch.deltaPosition = new Vector2(Time.deltaTime * v3.x * speedSwipeMouse, Time.deltaTime * v3.y * speedSwipeMouse);
                //Debug.Log(touch.deltaPosition);
            }
            lastPosCur = Input.mousePosition;

            if(swipeFlag) {
                if (Math.Abs(MegaCameraController.inst.disCamera.localPosition.z) < 0.0001) {
                    x = touch.deltaPosition.x;
                    y = touch.deltaPosition.y;
                }
                else {
                    x = touch.deltaPosition.x * (MegaCameraController.inst.disCamera.localPosition.z / GlobalParams.factorPerspStabilization);
                    y = touch.deltaPosition.y * (MegaCameraController.inst.disCamera.localPosition.z / GlobalParams.factorPerspStabilization);
                }

                
            }

            if(Input.touchCount == 1) {
                if(MegaCameraController.inst.isFirstLookScene) {
                    touch = Input.GetTouch(0);
                    //Debug.Log("x = " + touch.deltaPosition.x);
                    //Debug.Log("y = " + touch.deltaPosition.y);
                    if(touch.deltaPosition.x > 0.001f || touch.deltaPosition.y > 0.001f) {
                        if(MoveFirstFaceController.inst) {
                            MoveFirstFaceController.inst.StopClickCoroutine();
                        }
                    }
                    x = -touch.deltaPosition.x * speedTouch;
                    y = touch.deltaPosition.y * speedTouch;
                    swipeFlag = true;
                } else {
                    touch = Input.GetTouch(0);
                    //Debug.Log("x = " + touch.deltaPosition.x);
                    //Debug.Log("y = " + touch.deltaPosition.y);
                    x = touch.deltaPosition.x * speedTouch * (MegaCameraController.inst.disCamera.localPosition.z / GlobalParams.factorPerspStabilization);
                    y = touch.deltaPosition.y * speedTouch * (MegaCameraController.inst.disCamera.localPosition.z / GlobalParams.factorPerspStabilization);
                    swipeFlag = true;
                }
            }

            if(Input.touchCount == 2) {
                pinchFlag = true;
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);
                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
                float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
                deltaMagnitudeDiff = (prevTouchDeltaMag - touchDeltaMag) * speedZoom;
                deltaMagnitudeDiff *= (MegaCameraController.inst.disCamera.localPosition.z / GlobalParams.factorPerspStabilization);

                if(MoveFirstFaceController.inst) {
                    MoveFirstFaceController.inst.StopClickCoroutine();
                }
            }

            i += Time.deltaTime;
            if(i > 3) {
                //Debug.Log(Input.touchCount);
                i = 0;
            }

            if(swipeFlag) {
                // Debug.Log("x = " + touch.deltaPosition.x + " y=" + touch.deltaPosition.y);
                MegaCameraController.inst.MoveFromSwipe(x, y);
            }

            if(pinchFlag) {
                // Debug.Log("x = " + touch.deltaPosition.x + " y=" + touch.deltaPosition.y);
                MegaCameraController.inst.ZoomFromPinch(deltaMagnitudeDiff);
            }
        }
    }
}
