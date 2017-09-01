using UnityEngine;

namespace Assets.Mega.Scripts {
    public class KeyController : MonoBehaviour {
        private float speedKey = 2;
        private float speedTouch = 0.03f;//0.003f;
        private float speedZoom = 0.03f;//0.003f;

        public bool isFirstLookScene;

        public void Start () {
            Debug.Log("KeyController LIVE!!!");
        }

        public float i = 0;

        public void Update () {
            float x = 0;
            float y = 0;
            Touch touch = new Touch();
            touch.deltaPosition = Vector2.zero;

            bool swipeFlag = false;
            bool pinchFlag = false;

            if(Input.GetKey(KeyCode.S)) {
                touch.deltaPosition = new Vector2(Time.deltaTime * speedKey, touch.deltaPosition.y);
                swipeFlag = true;
            }
            if(Input.GetKey(KeyCode.F)) {
                touch.deltaPosition = new Vector2(-Time.deltaTime * speedKey, touch.deltaPosition.y);
                swipeFlag = true;
            }
            if(Input.GetKey(KeyCode.E)) {
                touch.deltaPosition = new Vector2(touch.deltaPosition.x, -Time.deltaTime * speedKey);
                swipeFlag = true;
            }
            if(Input.GetKey(KeyCode.D)) {
                touch.deltaPosition = new Vector2(touch.deltaPosition.x, Time.deltaTime * speedKey);
                swipeFlag = true;
            }

            if(swipeFlag) {
                if(MegaCameraController.inst.listCamerasOrto.Count > 0) {
                    x = touch.deltaPosition.x * (MegaCameraController.inst.listCamerasOrto[0].orthographicSize / 2f);
                    y = touch.deltaPosition.y * (MegaCameraController.inst.listCamerasOrto[0].orthographicSize / 2f);
                } else {
                    x = touch.deltaPosition.x;
                    y = touch.deltaPosition.y;
                }

            }

            if(Input.touchCount == 1) {
                if(isFirstLookScene) {
                    touch = Input.GetTouch(0);
                    Debug.Log("x = " + touch.deltaPosition.x);
                    Debug.Log("y = " + touch.deltaPosition.y);
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
                    Debug.Log("x = " + touch.deltaPosition.x);
                    Debug.Log("y = " + touch.deltaPosition.y);
                    x = touch.deltaPosition.x * speedTouch * (MegaCameraController.inst.listCamerasOrto[0].orthographicSize / 2f);
                    y = touch.deltaPosition.y * speedTouch * (MegaCameraController.inst.listCamerasOrto[0].orthographicSize / 2f);
                    swipeFlag = true;
                }
            }

            float deltaMagnitudeDiff = 0;
            if(Input.touchCount == 2) {
                pinchFlag = true;
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);
                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
                float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
                deltaMagnitudeDiff = (prevTouchDeltaMag - touchDeltaMag) * speedZoom;
                if(MegaCameraController.inst.listCamerasOrto.Count > 0) {
                    deltaMagnitudeDiff *= (MegaCameraController.inst.listCamerasOrto[0].orthographicSize / 2f);
                }
                if(MoveFirstFaceController.inst) {
                    MoveFirstFaceController.inst.StopClickCoroutine();
                }
            }

            if(Input.GetKey(KeyCode.O)) {
                pinchFlag = true;
                deltaMagnitudeDiff = 1f * speedZoom * speedKey;
            }

            if(Input.GetKey(KeyCode.P)) {
                pinchFlag = true;
                deltaMagnitudeDiff = -1f * speedZoom * speedKey;
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
