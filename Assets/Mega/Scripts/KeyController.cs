using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Mega.Scripts {
    public class KeyController : MonoBehaviour {

        public enum AllMegaState {
            move,
            rotate,
            zoom
        }

        public static KeyController inst;

        public float panoramSpeedTouch = 0.0018f;//0.0018f;
        public float firstLookSpeedTouch = 0.0017f;//0.0017f;
        public float rotateSpeedTouch = -0.06f;//0.06f;

        public float panoramSpeedSwipeMouse = -0.005f;//-0.0018f
        public float firstLookSpeedSwipeMouse = -0.007f;
        public float rotateSpeedSwipeMouse = -0.06f;

        public float zoomSpeedSwipeMouse = 15f;//15f;

        public Vector3 lastPosCur;
        public Vector3 currentPosCur;

        public bool stabilizationFlag;

        public int doubleClick = 0;
        public float timeToDoubleClick = 0;
        public bool canSecondClick;
        public bool rotateTouchs;
        public float oldRotateVar;
        public float deltaY;

        public AllMegaState currentAllMegaState = AllMegaState.move;

        public bool clickOnMap;

        public List<EventTrigger.Entry> listEntries = new List<EventTrigger.Entry>();


        public Coroutine coroSumm;
        public float currentMultiplication;
        public float currentSum;

        public void Awake() {
            inst = this;
        }

        public void Start() {

            //CastButton(ButtonAdds.inst.btnLeftRotate, true, ChangeCameraAngel);
            //CastButton(ButtonAdds.inst.btnRightRotate, false, ChangeCameraAngel);

            ButtonAdds.inst.btnLeftRotate.onClick.AddListener(() => { RotateCameraForAngel(true); });
            ButtonAdds.inst.btnRightRotate.onClick.AddListener(() => { RotateCameraForAngel(false); });

            ButtonAdds.inst.btnZoomIn.onClick.AddListener(() => { ZoomCameraForDis(true); });
            ButtonAdds.inst.btnZoomOut.onClick.AddListener(() => { ZoomCameraForDis(false); });

            
            //CastButton(ButtonAdds.inst.btnZoomIn, true, ChangeCameraZoom);
            //CastButton(ButtonAdds.inst.btnZoomOut, false, ChangeCameraZoom);
            SetMoveAllMega();
        }

        public void SetZoom () {
            currentAllMegaState = AllMegaState.zoom;
        }

        public void SetMoveAllMega() {
            currentAllMegaState = AllMegaState.move;
        }
        
        public void SetRotate () {
            currentAllMegaState = AllMegaState.rotate;
        }

        //public void SwapFirstLookAndAllMega () {
        //    if (MainLogic.inst.isRoadLook) {
        //        return;
        //    }
        //    //Debug.Log(MainLogic.inst.GetViewCurrentStates() + " " + MegaCameraController.inst.GetCurrentDistans() + " " +  MegaCameraController.inst.isFirstLookScene);
        //    if(MainLogic.inst.GetViewCurrentStates() == ViewStates.firstFaceLook && MegaCameraController.inst.GetCurrentDistans() > -100 && MegaCameraController.inst.isFirstLookScene) {
        //        Debug.Log("GoOutFirstLook");
        //        ButonAdds.inst.ShowUpButton();
        //       
        //        MegaCameraController.inst.GoOutFirstLook();
        //    }
        //    if(MainLogic.inst.GetViewCurrentStates() != ViewStates.firstFaceLook && MegaCameraController.inst.GetCurrentDistans() < -1000 && !MegaCameraController.inst.isFirstLookScene) {
        //        Debug.Log("GoToNearestShop");
        //        ButonAdds.inst.HideUpButton();
        //        StateFirstFaceLook.inst.GoToNearestShop();
        //    }
        //}

        public void ResetClick() {
            //Debug.Log("reset");
            canSecondClick = false;
            timeToDoubleClick = 0;
            doubleClick = 0;
        }

        public void CheckDoubleClick() {
            if(doubleClick == 1) {
                timeToDoubleClick += Time.deltaTime;
                if(timeToDoubleClick > GP.timeToDoubleClick) {
                    ResetClick();
                }
            }

            //if(doubleClick == 2) {
            //    SwapFirstLookAndAllMega();
            //    ResetClick();
            //    if(StateFirstFaceLook.inst) {
            //        StateFirstFaceLook.inst.StopClickCoroutine();
            //    }
            //}

            if ((Input.touchSupported && Input.touchCount > 0) || (!Input.touchSupported  && Input.GetKey(KeyCode.Mouse0))) {
                if ((Input.touchSupported && IsTouchUI(Input.GetTouch(0).position)) ||
                    (!Input.touchSupported && IsTouchUI(Input.mousePosition))) {
                    return;
                }
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
               MegaCameraController.inst.RotateFromPinch(Time.deltaTime * GP.speedRotateCamera);
                //SetChangeCamera();
            }
            if(Input.GetKey(KeyCode.E)) {
                MegaCameraController.inst.RotateFromPinch(-Time.deltaTime * GP.speedRotateCamera);
                //SetChangeCamera();
            }

            CheckDoubleClick();

            Touch touch = new Touch();
            touch.deltaPosition = Vector2.zero;

            bool swipeFlag = false;
            bool pinchFlag = false;
            float deltaMagnitudeDiff = 0;

           

            if(Input.GetKey(KeyCode.Mouse0) ) {
                MainLogic.inst.ResetTime();

                if (IsTouchUI(Input.mousePosition) || InterfaceController.inst.isDrag) {
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



                    if(MegaCameraController.inst.isFirstLookScene) {
                        touch.deltaPosition = new Vector2(Time.deltaTime * v3.x * firstLookSpeedSwipeMouse, Time.deltaTime * v3.y * firstLookSpeedSwipeMouse);

                    } else {
                        switch(currentAllMegaState) {
                            case AllMegaState.move:
                                touch.deltaPosition = new Vector2(Time.deltaTime * v3.x * panoramSpeedSwipeMouse, Time.deltaTime * v3.y * panoramSpeedSwipeMouse);
                                break;
                            case AllMegaState.rotate:
                                touch.deltaPosition = new Vector2(Time.deltaTime * v3.x * rotateSpeedSwipeMouse, Time.deltaTime * v3.y * rotateSpeedSwipeMouse);
                                break;
                            case AllMegaState.zoom:
                                touch.deltaPosition = new Vector2(Time.deltaTime * v3.x * rotateSpeedSwipeMouse, Time.deltaTime * v3.y * rotateSpeedSwipeMouse);
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }

                    }

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
                    x = touch.deltaPosition.x * (MegaCameraController.inst.disCamera.localPosition.z / GP.factorPerspStabilization);
                    y = touch.deltaPosition.y * (MegaCameraController.inst.disCamera.localPosition.z / GP.factorPerspStabilization);
                }
            }

            if(Input.touchSupported && Input.touchCount == 1) {
                MainLogic.inst.ResetTime();
                if(IsTouchUI(Input.GetTouch(0).position) || InterfaceController.inst.isDrag) {
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
                    x = -touch.deltaPosition.x * firstLookSpeedTouch;
                    y = -touch.deltaPosition.y * firstLookSpeedTouch;
                    swipeFlag = true;
                } else {
                    touch = Input.GetTouch(0);

                    switch (currentAllMegaState) {
                        case AllMegaState.move:
                            x = -touch.deltaPosition.x * panoramSpeedTouch * (MegaCameraController.inst.disCamera.localPosition.z / GP.factorPerspStabilization);
                            y = -touch.deltaPosition.y * panoramSpeedTouch * (MegaCameraController.inst.disCamera.localPosition.z / GP.factorPerspStabilization);

                            break;
                        case AllMegaState.rotate:
                            x = -touch.deltaPosition.x * rotateSpeedTouch * (MegaCameraController.inst.disCamera.localPosition.z / GP.factorPerspStabilization);
                            y = -touch.deltaPosition.y * rotateSpeedTouch * (MegaCameraController.inst.disCamera.localPosition.z / GP.factorPerspStabilization);

                            break;
                        case AllMegaState.zoom:
                            x = -touch.deltaPosition.x * rotateSpeedTouch * (MegaCameraController.inst.disCamera.localPosition.z / GP.factorPerspStabilization);
                            y = -touch.deltaPosition.y * rotateSpeedTouch * (MegaCameraController.inst.disCamera.localPosition.z / GP.factorPerspStabilization);

                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    
                    swipeFlag = true;
                }
                
            }

            if(swipeFlag) {
                //if(MainLogic.inst.isRoadLook) {
                //    return;
                //}
                // Debug.Log("x = " + touch.deltaPosition.x + " y=" + touch.deltaPosition.y);

                if (MegaCameraController.inst.isFirstLookScene) {
                    MegaCameraController.inst.MoveFromSwipe(x, y);
                } else {

                    switch(currentAllMegaState) {
                        case AllMegaState.move:
                            //Debug.Log("MOVE");
                            MegaCameraController.inst.MoveFromSwipe(x, y);
                            break;
                        case AllMegaState.rotate:
                            MegaCameraController.inst.RotateFromPinch(Time.deltaTime * GP.speedRotateCamera * x);
                            break;
                        case AllMegaState.zoom:
                            deltaMagnitudeDiff = y * zoomSpeedSwipeMouse;
                            //deltaMagnitudeDiff *= (MegaCameraController.inst.disCamera.localPosition.z / GlobalParams.factorPerspStabilization);
                            //Debug.Log(deltaMagnitudeDiff);
                            MegaCameraController.inst.ZoomFromPinch(deltaMagnitudeDiff);
                            break;
                    }
                   
                }
               

                
            }

        }



        public bool IsTouchUI(Vector3 v3) {
            //return !clickOnMap;
            int screenW = Screen.width;
            int screenH = Screen.height;
            
            float timeLineW = ((2000f * screenW) / 3840f);
            float timeLineH = (((GP.fullUI ? 660f : 330f) * screenH) / 2160f);
            
            if (v3.x < Screen.width*0.5f + timeLineW * 0.5f + 300
                && v3.x > Screen.width * 0.5f - timeLineW * 0.5f
                && v3.y < timeLineH) {
                //Debug.Log("good");
                return true;
            }
            //Debug.Log("bad");
            return false;
        }
        
        public void CastButton (Button btn, bool isLeft, Action<bool> actionBool) {
            var trigger = btn.gameObject.AddComponent<EventTrigger>();
            var pointerEnter = new EventTrigger.Entry();

            pointerEnter.eventID = EventTriggerType.PointerEnter;
            pointerEnter.callback.AddListener((e) => { StartMultiplication(isLeft, actionBool); });
            trigger.triggers.Add(pointerEnter);

            var pointerExit = new EventTrigger.Entry();

            pointerExit.eventID = EventTriggerType.PointerExit;
            pointerExit.callback.AddListener((e) => { StopMultiplication(); });
            listEntries.Add(pointerExit);

            trigger.triggers.Add(pointerExit);
        }

        [EasyButtons.Button]
        public void HardStopZoom() {
            foreach (var listEntry in listEntries) {
                listEntry.callback.Invoke(new PointerEventData(EventSystem.current));
            }
        }

        public void StartMultiplication (bool isLeft, Action<bool> actionBool) {
            StopMultiplication();
            coroSumm = StartCoroutine(IEnumSummCount(isLeft, actionBool));
        }

        public void StopMultiplication () {
            if(coroSumm != null) {
                StopCoroutine(coroSumm);
                coroSumm = null;
            }

            currentSum = 0;
            currentMultiplication = 1;
        }

        public IEnumerator IEnumSummCount (bool isLeft, Action<bool> actionBool) {
            while(true) {
                currentMultiplication *= 1.05f;
                currentSum += GP.btnBaseRotateSpeed * (int)currentMultiplication;
                actionBool.Invoke(isLeft);
                //ChangeCameraAngel(isLeft);
                yield return new WaitForSeconds(0.01f);
            }
        }

        public void ChangeCameraAngel (bool isLeft) {
            currentSum = currentSum < GP.btnBaseRotateSpeed ? GP.btnBaseRotateSpeed : currentSum;
            currentSum = currentSum > GP.btnMaxRotateSpeed ? GP.btnMaxRotateSpeed : currentSum;
            if (isLeft) {
                RotateFromButton(currentSum);
            } else {
                RotateFromButton(-currentSum);
            }
        }

        public void ChangeCameraZoom (bool isLeft) {
            currentSum = currentSum < GP.btnBaseZoomSpeed ? GP.btnBaseZoomSpeed : currentSum;
            currentSum = currentSum > GP.btnMaxZoomSpeed ? GP.btnMaxZoomSpeed : currentSum;
            if(isLeft) {
                ZoomFromButton(currentSum);
            } else {
                ZoomFromButton(-currentSum);
            }
        }


        public void RotateFromButton (float currentSum) {
            var x = currentSum * rotateSpeedTouch * (MegaCameraController.inst.disCamera.localPosition.z / GP.factorPerspStabilization);
            MegaCameraController.inst.RotateFromPinch(Time.deltaTime * GP.speedRotateCamera * x);
        }

        public void ZoomFromButton (float currentSum) {
            var deltaMagnitudeDiff = currentSum;
            MegaCameraController.inst.ZoomFromPinch(deltaMagnitudeDiff);
        }


        public void ZoomCameraForDis (bool isForward) {
            ButtonAdds.inst.SetInteractable(false);
            StartCoroutine(IEnumZoomCameraForDis(isForward));
        }

        public IEnumerator IEnumZoomCameraForDis (bool isForward) {
            int indexStep = 0;
            int countSpets = 5;
            var dis = 2000;//Mathf.Abs(GP.maxDistancePesr) / 8 - 500;
            var speedZoom = dis / countSpets;
            while(indexStep < countSpets) {
                MegaCameraController.inst.ZoomFromPinch(isForward ? speedZoom : -speedZoom);
                indexStep++;
                yield return null;
            }

            ButtonAdds.inst.SetInteractable(true);
        }

        public void RotateCameraForAngel(bool isLeft) {
            ButtonAdds.inst.SetInteractable(false);
            StartCoroutine(IEnumRotateCameraForAngel(isLeft));
        }

        public IEnumerator IEnumRotateCameraForAngel(bool isLeft) {
            int indexStep = 0;
            int countSpets = 7;
            int angel = 30;
            int speedRotate = angel / countSpets;
            while (indexStep < countSpets) {
                MegaCameraController.inst.RotateFromPinch(isLeft ? speedRotate : -speedRotate);
                indexStep++;
                yield return null;
            }
            ButtonAdds.inst.SetInteractable(true);
        }


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