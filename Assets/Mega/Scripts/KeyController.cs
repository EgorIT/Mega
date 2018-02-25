using System;
using UnityEngine;

namespace Assets.Mega.Scripts {
    public class KeyController : MonoBehaviour {
        private float speedKey = 2;
        //private float speedTouch = 0.003f;//0.003f;
        private float speedTouch = 0.0015f;//0.003f;
        private float speedSwipeMouse = -0.02f;//0.003f;
        //private float speedSwipeMouse = -0.004f;//0.003f;
        private float speedZoom = 0.09f;//0.003f;
        private float speedZoomWheel = 5f;//0.003f;

        public Vector3 lastPosCur;
        public Vector3 currentPosCur;

        public float i = 0;

        public bool stabilizationFlag;

        public int doubleClick = 0;
        public float timeToDoubleClick = 0;
        public bool canSecondClick;
        public bool rotateTouchs;
        public float oldRotateVar;
        public float deltaY;

        public void Update () {
            float x = 0;
            float y = 0;

            if(Input.GetKey(KeyCode.Q)) {
               MegaCameraController.inst.RotateFromPinch(Time.deltaTime * GlobalParams.speedRotateCamera);
                //SetChangeCamera();
            }
            if(Input.GetKey(KeyCode.E)) {
                MegaCameraController.inst.RotateFromPinch(-Time.deltaTime * GlobalParams.speedRotateCamera);
                //SetChangeCamera();
            }


            if (doubleClick == 1) {
                timeToDoubleClick += Time.deltaTime;
                if (timeToDoubleClick > 0.5f) {
                    canSecondClick = false;
                    timeToDoubleClick = 0;
                    doubleClick = 0;
                }
            }

            if (doubleClick == 2) {
                
                if (MainLogic.inst.GetViewCurrentStates() == ViewStates.firstFaceLook && MegaCameraController.inst.GetCurrentDistans() > -100 && MegaCameraController.inst.isFirstLookScene) {
                    Debug.Log("DOUBLE");
                    MainLogic.inst.ChangeState(ViewStates.allMega);
                }
                canSecondClick = false;
                timeToDoubleClick = 0;
                doubleClick = 0;
            }

            Touch touch = new Touch();
            touch.deltaPosition = Vector2.zero;

            bool swipeFlag = false;
            bool pinchFlag = false;
            float deltaMagnitudeDiff = 0;

            if(Input.GetAxis("Mouse ScrollWheel") > 0) {
                MainLogic.inst.ResetTime();
                pinchFlag = true;
                if(Math.Abs(MegaCameraController.inst.disCamera.localPosition.z) < 0.0001) {
                    deltaMagnitudeDiff = -1f * speedZoomWheel;
                } else {
                    deltaMagnitudeDiff = -1f * speedZoomWheel *
                        (MegaCameraController.inst.disCamera.localPosition.z / GlobalParams.factorPerspStabilization);
                }

            }

            if(Input.GetAxis("Mouse ScrollWheel") < 0) {
                MainLogic.inst.ResetTime();

                pinchFlag = true;
                if(Math.Abs(MegaCameraController.inst.disCamera.localPosition.z) < 0.0001) {
                    deltaMagnitudeDiff = 1f * speedZoomWheel;
                } else {
                    deltaMagnitudeDiff = 1f * speedZoomWheel *
                        (MegaCameraController.inst.disCamera.localPosition.z / GlobalParams.factorPerspStabilization);
                }
            }

            if(Input.GetKey(KeyCode.Mouse0)) {
                MainLogic.inst.ResetTime();
                if (IsTouchUI(Input.mousePosition)) {
                    return;
                }
                if(doubleClick == 0) {
                    doubleClick++;
                }
                if(doubleClick == 1 && canSecondClick) {
                    doubleClick++;
                }
                if (stabilizationFlag) {
                    swipeFlag = true;
                    var v3 = Input.mousePosition - lastPosCur;
                   // Debug.Log(v3.sqrMagnitude);
                    if(StateFirstFaceLook.inst && v3.sqrMagnitude > 50) {
                        timeToDoubleClick = 0;
                        doubleClick = 0;
                        if (StateFirstFaceLook.inst) {
                            StateFirstFaceLook.inst.StopClickCoroutine();
                        }
                        
                    }

                    touch.deltaPosition = new Vector2(Time.deltaTime * v3.x * speedSwipeMouse, Time.deltaTime * v3.y * speedSwipeMouse);
                }
                stabilizationFlag = true;
                lastPosCur = Input.mousePosition;
            } else {
                if (!Input.touchSupported && doubleClick == 1 && !canSecondClick) {
                    canSecondClick = true;
                }
                    

                stabilizationFlag = false;
            }
            

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

            if(Input.touchSupported && Input.touchCount == 1) {
                MainLogic.inst.ResetTime();
                if(IsTouchUI(Input.GetTouch(0).position)) {
                    return;
                }
                if (doubleClick == 0) {
                    doubleClick++;
                }
                if(doubleClick == 1 && canSecondClick) {
                    doubleClick++;
                }
                
                doubleClick++;
                if(MegaCameraController.inst.isFirstLookScene) {
                    touch = Input.GetTouch(0);
                    //Debug.Log("x = " + touch.deltaPosition.x);
                    //Debug.Log("y = " + touch.deltaPosition.y);
                    if(touch.deltaPosition.x > 0.001f || touch.deltaPosition.y > 0.001f) {
                        timeToDoubleClick = 0;
                        doubleClick = 0;
                        if(StateFirstFaceLook.inst) {
                            StateFirstFaceLook.inst.StopClickCoroutine();
                        }
                    }
                    x = -touch.deltaPosition.x * speedTouch;
                    y = -touch.deltaPosition.y * speedTouch;
                    swipeFlag = true;
                } else {
                    touch = Input.GetTouch(0);
                    //Debug.Log("x = " + touch.deltaPosition.x);
                    //Debug.Log("y = " + touch.deltaPosition.y);
                    x = -touch.deltaPosition.x * speedTouch * (MegaCameraController.inst.disCamera.localPosition.z / GlobalParams.factorPerspStabilization);
                    y = -touch.deltaPosition.y * speedTouch * (MegaCameraController.inst.disCamera.localPosition.z / GlobalParams.factorPerspStabilization);
                    swipeFlag = true;
                }
            } else {
                if(Input.touchSupported && doubleClick == 1 && !canSecondClick) {
                    canSecondClick = true;
                }
            }

            //if(Time.time > newTime) {
            //    tapCount = 0;
            //}

            if(Input.touchSupported && Input.touchCount == 2) {
                if(IsTouchUI(Input.GetTouch(1).position)) {
                    return;
                }
                //swipeFlag = false; old
                //pinchFlag = true;
                //Touch touchZero = Input.GetTouch(0);
                //Touch touchOne = Input.GetTouch(1);
                //Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                //Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
                //float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                //float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
                //deltaMagnitudeDiff = (prevTouchDeltaMag - touchDeltaMag) * speedZoom;
                //deltaMagnitudeDiff *= (MegaCameraController.inst.disCamera.localPosition.z / GlobalParams.factorPerspStabilization);
                //Debug.Log("Dif = " + deltaMagnitudeDiff);


                swipeFlag = false;
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);
                //rotate block
                var v2 = touchZero.position - touchOne.position;
                var currentRoatateVar = Mathf.Atan2(v2.x, v2.y);
                if(!rotateTouchs) {
                    oldRotateVar = currentRoatateVar;
                    rotateTouchs = true;
                } else {
                    deltaY = currentRoatateVar - oldRotateVar;
                    oldRotateVar = currentRoatateVar;
                    //DebugLogSalt.Log(deltaY.ToString());
                    if(Math.Abs(deltaY) > 0.01f && Math.Abs(deltaY) < 2f) {
                        timeToDoubleClick = 0;
                        doubleClick = 0;
                        MegaCameraController.inst.RotateFromPinch(Time.deltaTime * GlobalParams.speedRotateCamera * -deltaY);
                    }
                }


                if(StateFirstFaceLook.inst) {
                    StateFirstFaceLook.inst.StopClickCoroutine();
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
              
                //Debug.Log("deltaMagnitudeDiff = " + deltaMagnitudeDiff);
                //MegaCameraController.inst.ZoomFromPinch(deltaMagnitudeDiff);
               // MegaCameraController.inst.RotateFromPinch(dY);
            }
        }


        public bool IsTouchUI(Vector3 v3) {
            int screenW = Screen.width;
            int screenH = Screen.height;
            
            int timeLineW = (int)((2000f * screenW) / 3840f);
            int timeLineH = (int)((560f * screenH) / 2160f);
            
            if (v3.x < Screen.width*0.5f + timeLineW * 0.5f
                && v3.x > Screen.width * 0.5f - timeLineW * 0.5f
                && v3.y < timeLineH) {
                return true;
            }
            return false;
        }
    }
}
