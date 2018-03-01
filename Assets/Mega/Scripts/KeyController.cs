using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Mega.Scripts {
    public class KeyController : MonoBehaviour {
        public static KeyController inst;
        private float speedKey = 2;
        //private float speedTouch = 0.003f;//0.003f;
        private float speedTouch = 0.0015f;//0.003f;
        private float speedSwipeMouse = -0.02f;//0.003f;
        //private float speedSwipeMouse = -0.004f;//0.003f;
        private float speedZoom = 0.09f;//0.003f;
        private float speedZoomWheel = 5f;//0.003f;

        public Vector3 lastPosCur;
        public Vector3 currentPosCur;

        public bool stabilizationFlag;

        public int doubleClick = 0;
        public float timeToDoubleClick = 0;
        public bool canSecondClick;
        public bool rotateTouchs;
        public float oldRotateVar;
        public float deltaY;

        public Button btnSet360;
        public Button btnSetPanoram;
        public Button btnSetZoom;

        public bool isPanoram = true;

        public bool clickOnMap;

        public void Awake() {
            inst = this;
        }

        public void Start() {
            btnSet360.onClick.AddListener(Set360);
            btnSetPanoram.onClick.AddListener(SetPanoram);
            btnSetZoom.onClick.AddListener(SetZoom);
            SetPanoram();
        }

        public void SetZoom () {
            if(MegaCameraController.inst.GetDontUseUi()) {
                return;
            }
            SwapZoom();
        }

        public void SetPanoram() {
            if (MegaCameraController.inst.GetDontUseUi()) {
                return;
            }
            isPanoram = true;
            btnSetPanoram.image.color = Color.blue;
            btnSet360.image.color = Color.white;
        }
        
        public void Set360 () {
            if(MegaCameraController.inst.GetDontUseUi()) {
                return;
            }
            isPanoram = false;
            btnSetPanoram.image.color = Color.white;
            btnSet360.image.color = Color.blue;
        }

        public void SetActivButton(bool var) {
            btnSet360.gameObject.SetActive(var);
            btnSetPanoram.gameObject.SetActive(var);
            btnSetZoom.gameObject.SetActive(var);
        }

        public void SwapZoom() {
           
            if(MainLogic.inst.GetViewCurrentStates() == ViewStates.firstFaceLook && MegaCameraController.inst.GetCurrentDistans() > -100 && MegaCameraController.inst.isFirstLookScene) {
                Debug.Log("GoOutFirstLook");
                SetActivButton(true);
                MegaCameraController.inst.GoOutFirstLook();
            }
            if(MainLogic.inst.GetViewCurrentStates() != ViewStates.firstFaceLook && MegaCameraController.inst.GetCurrentDistans() < -5000 && !MegaCameraController.inst.isFirstLookScene) {
                SetActivButton(false);
                btnSetZoom.gameObject.SetActive(true);
                Debug.Log("GoToNearestShop");
                StateFirstFaceLook.inst.GoToNearestShop();
            }
        }

        public void ResetClick() {
            //Debug.Log("reset");
            canSecondClick = false;
            timeToDoubleClick = 0;
            doubleClick = 0;
        }

        public void CheckDoubleClick() {
            if(doubleClick == 1) {
                timeToDoubleClick += Time.deltaTime;
                if(timeToDoubleClick > GlobalParams.timeToDoubleClick) {
                    ResetClick();
                }
            }

            if(doubleClick == 2) {
                //Debug.Log("cccc");
                StateFirstFaceLook.inst.pointerEventData = null;
                SwapZoom();
                ResetClick();
            }

            if ((Input.touchSupported && Input.touchCount > 0) || (!Input.touchSupported  && Input.GetKey(KeyCode.Mouse0))) {
                if(doubleClick == 0) {
                    doubleClick++;
                }
                if(doubleClick == 1 && canSecondClick) {
                    doubleClick++;
                }
            }

            if((Input.touchSupported && Input.touchCount == 0) || (!Input.touchSupported && !Input.GetKey(KeyCode.Mouse0))) {
                if(doubleClick == 1 && !canSecondClick) {
                    canSecondClick = true;
                }
            }


           

        }

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

            CheckDoubleClick();

            Touch touch = new Touch();
            touch.deltaPosition = Vector2.zero;

            bool swipeFlag = false;
            bool pinchFlag = false;
            float deltaMagnitudeDiff = 0;

           

            if(Input.GetKey(KeyCode.Mouse0)) {
                MainLogic.inst.ResetTime();

                if (IsTouchUI(Input.mousePosition)) {
                    return;
                }
               
                if (stabilizationFlag) {
                    swipeFlag = true;
                    var v3 = Input.mousePosition - lastPosCur;
                   // Debug.Log(v3.sqrMagnitude);
                    if(StateFirstFaceLook.inst && v3.sqrMagnitude > 50) {
                        ResetClick();
                        if (StateFirstFaceLook.inst) {
                            StateFirstFaceLook.inst.StopClickCoroutine();
                        }
                    }
                    touch.deltaPosition = new Vector2(Time.deltaTime * v3.x * speedSwipeMouse, Time.deltaTime * v3.y * speedSwipeMouse);
                }
                stabilizationFlag = true;
                lastPosCur = Input.mousePosition;
            } else {
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
                if(MegaCameraController.inst.isFirstLookScene) {
                    touch = Input.GetTouch(0);
                    if(touch.deltaPosition.x > 0.001f || touch.deltaPosition.y > 0.001f) {
                        ResetClick();
                        if(StateFirstFaceLook.inst) {
                            StateFirstFaceLook.inst.StopClickCoroutine();
                        }
                    }
                    x = -touch.deltaPosition.x * speedTouch;
                    y = -touch.deltaPosition.y * speedTouch;
                    swipeFlag = true;
                } else {
                    touch = Input.GetTouch(0);
                    x = -touch.deltaPosition.x * speedTouch * (MegaCameraController.inst.disCamera.localPosition.z / GlobalParams.factorPerspStabilization);
                    y = -touch.deltaPosition.y * speedTouch * (MegaCameraController.inst.disCamera.localPosition.z / GlobalParams.factorPerspStabilization);
                    swipeFlag = true;
                }
            }

            if(swipeFlag) {
                // Debug.Log("x = " + touch.deltaPosition.x + " y=" + touch.deltaPosition.y);
                if (isPanoram || MegaCameraController.inst.isFirstLookScene) {
                    MegaCameraController.inst.MoveFromSwipe(x, y);
                } else {
                    MegaCameraController.inst.RotateFromPinch(Time.deltaTime * GlobalParams.speedRotateCamera * x);
                }
               

                
            }

            //if(pinchFlag) {
            //    //Debug.Log("deltaMagnitudeDiff = " + deltaMagnitudeDiff);
            //    //MegaCameraController.inst.ZoomFromPinch(deltaMagnitudeDiff);
            //   // MegaCameraController.inst.RotateFromPinch(dY);
            //}

            //if(Input.touchSupported && Input.touchCount == 2) { old rotate for 2 finger
            //    if(IsTouchUI(Input.GetTouch(1).position)) {
            //        return;
            //    }
            //    //swipeFlag = false; old
            //    //pinchFlag = true;
            //    //Touch touchZero = Input.GetTouch(0);
            //    //Touch touchOne = Input.GetTouch(1);
            //    //Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            //    //Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
            //    //float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            //    //float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
            //    //deltaMagnitudeDiff = (prevTouchDeltaMag - touchDeltaMag) * speedZoom;
            //    //deltaMagnitudeDiff *= (MegaCameraController.inst.disCamera.localPosition.z / GlobalParams.factorPerspStabilization);
            //    //Debug.Log("Dif = " + deltaMagnitudeDiff);
            //
            //
            //    swipeFlag = false;
            //    Touch touchZero = Input.GetTouch(0);
            //    Touch touchOne = Input.GetTouch(1);
            //    //rotate block
            //    var v2 = touchZero.position - touchOne.position;
            //    var currentRoatateVar = Mathf.Atan2(v2.x, v2.y);
            //    if(!rotateTouchs) {
            //        oldRotateVar = currentRoatateVar;
            //        rotateTouchs = true;
            //    } else {
            //        deltaY = currentRoatateVar - oldRotateVar;
            //        oldRotateVar = currentRoatateVar;
            //        //DebugLogSalt.Log(deltaY.ToString());
            //        if(Math.Abs(deltaY) > 0.01f && Math.Abs(deltaY) < 2f) {
            //            MegaCameraController.inst.RotateFromPinch(Time.deltaTime * GlobalParams.speedRotateCamera * -deltaY);
            //        }
            //    }
            //
            //
            //    if(StateFirstFaceLook.inst) {
            //        StateFirstFaceLook.inst.StopClickCoroutine();
            //    }
            //}

            //if(Input.GetAxis("Mouse ScrollWheel") > 0) { old zoom wheel
            //    MainLogic.inst.ResetTime();
            //    pinchFlag = true;
            //    if(Math.Abs(MegaCameraController.inst.disCamera.localPosition.z) < 0.0001) {
            //        deltaMagnitudeDiff = -1f * speedZoomWheel;
            //    } else {
            //        deltaMagnitudeDiff = -1f * speedZoomWheel *
            //            (MegaCameraController.inst.disCamera.localPosition.z / GlobalParams.factorPerspStabilization);
            //    }
            //
            //}
            //
            //if(Input.GetAxis("Mouse ScrollWheel") < 0) {
            //    MainLogic.inst.ResetTime();
            //
            //    pinchFlag = true;
            //    if(Math.Abs(MegaCameraController.inst.disCamera.localPosition.z) < 0.0001) {
            //        deltaMagnitudeDiff = 1f * speedZoomWheel;
            //    } else {
            //        deltaMagnitudeDiff = 1f * speedZoomWheel *
            //            (MegaCameraController.inst.disCamera.localPosition.z / GlobalParams.factorPerspStabilization);
            //    }
            //}
        }


        public bool IsTouchUI(Vector3 v3) {
            //return !clickOnMap;
            int screenW = Screen.width;
            int screenH = Screen.height;
            
            int timeLineW = (int)((2000f * screenW) / 3840f);
            int timeLineH = (int)((560f * screenH) / 2160f);
            
            if (v3.x < Screen.width*0.5f + timeLineW * 0.5f + 300
                && v3.x > Screen.width * 0.5f - timeLineW * 0.5f
                && v3.y < timeLineH) {
                //Debug.Log("good");
                return true;
            }
            //Debug.Log("bad");
            return false;
        }
    }
}

