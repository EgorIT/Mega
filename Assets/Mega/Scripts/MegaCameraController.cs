using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Mega.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum TypeCameraOnState {
    orto,
    perspective,
    none
}

public class MegaCameraController : MonoBehaviour {
    public static MegaCameraController inst;

    public Coroutine moveCamera;
    public Coroutine mainCoroutine;

    public Transform posCamera;
    public Transform angelYCamera;
    public Transform angelXCamera;
    public Transform disCamera;

    public List<Camera> listCamerasOrto;

    public Camera perspectiveCamera;

    public TypeCameraOnState currentTypeCameraOnState = TypeCameraOnState.none;

    public Coroutine moveBack;
    public Vector3 lastGoodPos;
    public Vector3 currentEndAng;
    public bool dontUseSwipeAndPinch;

    public void Awake () {
        inst = this;
    }

    public void Start() {
        currentEndAng = new Vector3(angelXCamera.eulerAngles.x, angelYCamera.eulerAngles.y, 0);

    }


    public void MoveFromSwipe (float dX, float dY) {
        if (dontUseSwipeAndPinch) {
            return;
        }
        if(SceneManager.GetActiveScene().name != "LightTest 1") {
            MoveInPersp(dX, dY);

        } else {
            switch(MainLogic.inst.GetViewCurrentStates()) {
                case ViewStates.one:
                    break;
                case ViewStates.allMega:
                case ViewStates.shops:
                    MoveInOrto(-dX, -dY);
                    break;
                case ViewStates.firstFaceLook:
                    MoveInPersp(dX, dY);
                    break;
                case ViewStates.none:
                    break;
            }
        }

    }

    public void ZoomFromPinch (float dZoom) {
        if (dontUseSwipeAndPinch) {
            return;
        }
        if(SceneManager.GetActiveScene().name != "LightTest 1") {

        } else {
            switch(MainLogic.inst.GetViewCurrentStates()) {
                case ViewStates.one:
                    break;
                case ViewStates.allMega:
                case ViewStates.shops:
                    ZoomInOrto(dZoom);
                    break;
                case ViewStates.firstFaceLook:
                    break;
                case ViewStates.none:
                    break;
            }
        }

    }

    public void ZoomInOrto (float dZoom) {
        for(int i = 0; i < listCamerasOrto.Count; i++) {
            listCamerasOrto[i].orthographicSize = listCamerasOrto[i].orthographicSize + dZoom;
            listCamerasOrto[i].enabled = true;
        }
        CheckOrtoSize();
    }

    public void MoveInOrto (float x, float y) {
        angelYCamera.Translate(new Vector3(x, 0, y));
        var newX = posCamera.position.x + angelYCamera.localPosition.x;
        var newY = posCamera.position.z + angelYCamera.localPosition.z;
        angelYCamera.localPosition = Vector3.zero;

        float y1 = MainLogic.inst.borders[0].position.z;
        float x1 = MainLogic.inst.borders[0].position.x;
        float y2 = MainLogic.inst.borders[1].position.z;
        float x2 = MainLogic.inst.borders[1].position.x;

        if(newX < x2 && newX > x1 && newY < y1 && newY > y2) {
            lastGoodPos = new Vector3(newX, posCamera.position.y, newY);
            if(moveBack != null) {
                StopCoroutine(moveBack);
                moveBack = null;
            }
        }
        if(newX > x2 || newX < x1 || newY > y1 || newY < y2) {
            GoToGoodPos();
        }
        posCamera.position = new Vector3(newX, posCamera.position.y, newY);
    }

    public void GoToGoodPos() {
        if (moveBack != null) {
            StopCoroutine(moveBack);
            moveBack = null;
        }
        moveBack = StartCoroutine(IEnumChangePosAndAngOrtoCamera(lastGoodPos, GlobalParams.eulerAnglesForCameraInShops, -5, listCamerasOrto[0].orthographicSize, GlobalParams.timeToBackFromBorder));
    }

    public void CheckOrtoSize () {
        for(int i = 0; i < listCamerasOrto.Count; i++) {
            if(listCamerasOrto[i].orthographicSize > GlobalParams.ortoMaxSize) {
                listCamerasOrto[i].orthographicSize = GlobalParams.ortoMaxSize;
            }
            if(listCamerasOrto[i].orthographicSize < GlobalParams.ortoMinSize) {
                listCamerasOrto[i].orthographicSize = GlobalParams.ortoMinSize;
            }
        }
    }

    public void CheckPozitionCamera () {
        float y1 = MainLogic.inst.borders[0].position.z;
        float x1 = MainLogic.inst.borders[0].position.x;
        float y2 = MainLogic.inst.borders[1].position.z;
        float x2 = MainLogic.inst.borders[1].position.x;

        float cx = posCamera.position.x;
        float cy = posCamera.position.z;
        //float y =(cx*(y2-y1)-x1*y2+x1*y1+y1*x2-y1*x1)/(y2-y1);*/
        if(cx > x2 || cx < x1) {
            if(posCamera.position.x > x2) {
                posCamera.position = new Vector3(x2, posCamera.position.y, posCamera.position.z);
            }
            if(posCamera.position.x < x1) {
                posCamera.position = new Vector3(x1, posCamera.position.y, posCamera.position.z);
            }
        }
        if(cy > y1 || cy < y2) {
            if(posCamera.position.z > y1) {
                posCamera.position = new Vector3(posCamera.position.x, posCamera.position.y, y1);
            }
            if(posCamera.position.z < y2) {
                posCamera.position = new Vector3(posCamera.position.x, posCamera.position.y, y2);
            }
        }
    }

    public void MoveInPersp (float x, float y) {
        float factor = 30;
        angelYCamera.localEulerAngles += new Vector3(0, x * factor, 0);

        var currentX = angelXCamera.localEulerAngles.x > 180 ? angelXCamera.localEulerAngles.x - 360 : angelXCamera.localEulerAngles.x;
        if(currentX >= GlobalParams.cameraXmaxPerspective || currentX <= GlobalParams.cameraXMinPerspective) {
            angelXCamera.localEulerAngles += new Vector3(-y * factor, 0, 0);
        }

        currentX = angelXCamera.localEulerAngles.x > 180 ? angelXCamera.localEulerAngles.x - 360 : angelXCamera.localEulerAngles.x;
        if(currentX < GlobalParams.cameraXmaxPerspective) {
            angelXCamera.localEulerAngles = new Vector3(GlobalParams.cameraXmaxPerspective, angelXCamera.localEulerAngles.y, 0);
        }
        if(currentX > GlobalParams.cameraXMinPerspective) {
            angelXCamera.localEulerAngles = new Vector3(GlobalParams.cameraXMinPerspective, angelXCamera.localEulerAngles.y, 0);
        }
        currentEndAng = new Vector3(angelXCamera.eulerAngles.x, angelYCamera.eulerAngles.y, 0);
        //Debug.Log(angelXCamera.localEulerAngles.x);
    }

    public void SetNewPosCamera (Vector3 endPos, Vector3 endAng, float finalSizeCamera, TypeCameraOnState newTypeCameraOnState) {
        if (mainCoroutine != null) {
            StopCoroutine(mainCoroutine);
        }
        mainCoroutine = StartCoroutine(IEnumSetNewPosCamera(endPos, endAng, finalSizeCamera, newTypeCameraOnState));
    }

    public IEnumerator IEnumSetNewPosCamera(Vector3 endPos, Vector3 endAng, float finalSizeCamera, TypeCameraOnState newTypeCameraOnState) {
        var finalDistans = -5;
        if(currentTypeCameraOnState == newTypeCameraOnState) {
            if(newTypeCameraOnState == TypeCameraOnState.orto) {
                if(moveCamera != null) {
                    StopCoroutine(moveCamera);
                    moveCamera = null;
                }
                yield return moveCamera = StartCoroutine(IEnumChangePosAndAngOrtoCamera(endPos, endAng, finalDistans, finalSizeCamera, GlobalParams.timeToFly));
            }
            if(newTypeCameraOnState == TypeCameraOnState.perspective) {
                if(moveCamera != null) {
                    StopCoroutine(moveCamera);
                    moveCamera = null;
                }
                yield return moveCamera = StartCoroutine(IEnumChangePosAndAngPersCamera(endPos, endAng, 0, 60));
            }
        } else {
            if(newTypeCameraOnState == TypeCameraOnState.orto) {
                if(moveCamera != null) {
                    StopCoroutine(moveCamera);
                    moveCamera = null;
                }
                yield return moveCamera = StartCoroutine(IEnumSwapPerspectiveToOrto(endPos, endAng, finalDistans, finalSizeCamera));
            }
            if(newTypeCameraOnState == TypeCameraOnState.perspective) {
                if(moveCamera != null) {
                    StopCoroutine(moveCamera);
                    moveCamera = null;
                }
                yield return moveCamera = StartCoroutine(IEnumSwapOrtoToPerspective(endPos, endAng, finalSizeCamera));
            }
        }
        currentEndAng = new Vector3(angelXCamera.eulerAngles.x, angelYCamera.eulerAngles.y, 0);
    }

    public IEnumerator IEnumSwapOrtoToPerspective (Vector3 endPos, Vector3 endAng, float finalSizeCamera) {
        var startPosition = posCamera.localPosition;
        var startEulerAnglesY = angelYCamera.localEulerAngles.y;
        var startEulerAnglesX = angelXCamera.localEulerAngles.x;
        var startDisCamera = disCamera.localPosition.z;
        var startFieldOfView = GetFieldOfView(listCamerasOrto[0].orthographicSize);
        var finalFieldOfView = 60;
        var startPerlocalEulerAngles = perspectiveCamera.transform.localEulerAngles;

        for(int i = 0; i < listCamerasOrto.Count; i++) {
            listCamerasOrto[i].enabled = false;
        }
        perspectiveCamera.enabled = true;

        float time = GlobalParams.timeToFly;
        float currentTime = 0;
        while(currentTime < time) {
            var t = currentTime / time;
            posCamera.position = Vector3.Lerp(startPosition, endPos, t);
            angelYCamera.localEulerAngles = new Vector3(0, Mathf.LerpAngle(startEulerAnglesY, endAng.y, t), 0);
            angelXCamera.localEulerAngles = new Vector3(Mathf.LerpAngle(startEulerAnglesX, endAng.x, t * t), 0, 0);
            disCamera.localPosition = new Vector3(0, 0, Mathf.Lerp(startDisCamera, 0, t * t * t));
            perspectiveCamera.fieldOfView = Mathf.Lerp(startFieldOfView, 60, t * t);
            perspectiveCamera.transform.localEulerAngles =
                new Vector3(Mathf.LerpAngle(startPerlocalEulerAngles.x, 0, t),
                    Mathf.LerpAngle(startPerlocalEulerAngles.y, 0, t),
                    Mathf.LerpAngle(startPerlocalEulerAngles.z, 0, t));
            currentTime += Time.deltaTime;
            yield return null;
        }
        perspectiveCamera.transform.localEulerAngles = Vector3.zero;
        perspectiveCamera.fieldOfView = 60;
        posCamera.position = endPos;
        angelYCamera.localEulerAngles = new Vector3(0, endAng.y, 0);
        angelXCamera.localEulerAngles = new Vector3(endAng.x, 0, 0);
        disCamera.localPosition = new Vector3(0, 0, 0);
        yield return null;
        currentTypeCameraOnState = TypeCameraOnState.perspective;
        moveCamera = null;
    }

    public IEnumerator IEnumSwapPerspectiveToOrto (Vector3 endPos, Vector3 endAng, float finalDistans, float finalSizeCamera) {
        dontUseSwipeAndPinch = true;
        var startPosition = posCamera.localPosition;
        var startEulerAnglesY = angelYCamera.localEulerAngles.y;
        var startEulerAnglesX = angelXCamera.localEulerAngles.x;
        var startDisCamera = disCamera.localPosition.z;
        var startFieldOfView = perspectiveCamera.fieldOfView;
        var finalFieldOfView = GetFieldOfView(finalSizeCamera);
        var startPerlocalEulerAngles = perspectiveCamera.transform.localEulerAngles;
        //Debug.Log(finalFieldOfView);

        float time = GlobalParams.timeToFly;
        float currentTime = 0;
        while(currentTime < time) {
            var t = currentTime / time;
            posCamera.position = Vector3.Lerp(startPosition, endPos, t);
            angelYCamera.localEulerAngles = new Vector3(0, Mathf.LerpAngle(startEulerAnglesY, endAng.y, t), 0);
            angelXCamera.localEulerAngles = new Vector3(Mathf.LerpAngle(startEulerAnglesX, endAng.x, t * t), 0, 0);
            disCamera.localPosition = new Vector3(0, 0, Mathf.Lerp(startDisCamera, finalDistans, t * t * t));
            perspectiveCamera.fieldOfView = Mathf.Lerp(startFieldOfView, finalFieldOfView, t * t);
            perspectiveCamera.transform.localEulerAngles =
                new Vector3(Mathf.LerpAngle(startPerlocalEulerAngles.x, 0, t),
                    Mathf.LerpAngle(startPerlocalEulerAngles.y, 0, t),
                    Mathf.LerpAngle(startPerlocalEulerAngles.z, 0, t));
            currentTime += Time.deltaTime;
            yield return null;
        }
        perspectiveCamera.transform.localEulerAngles = Vector3.zero;
        perspectiveCamera.fieldOfView = finalFieldOfView;
        posCamera.position = endPos;
        angelYCamera.localEulerAngles = new Vector3(0, endAng.y, 0);
        angelXCamera.localEulerAngles = new Vector3(endAng.x, 0, 0);
        disCamera.localPosition = new Vector3(0, 0, finalDistans);
        for(int i = 0; i < listCamerasOrto.Count; i++) {
            listCamerasOrto[i].orthographicSize = finalSizeCamera;
            listCamerasOrto[i].enabled = true;
        }
        perspectiveCamera.enabled = false;
        yield return null;
        currentTypeCameraOnState = TypeCameraOnState.orto;
        dontUseSwipeAndPinch = false;
        moveCamera = null;
    }

    public float GetFieldOfView (float sizeOrto) {
        var w = (sizeOrto / Screen.height) * Screen.width;
        var atan = (Mathf.Atan2(w * 0.5f, 2.2f) / 3.14f) * 180;//1.1=38
        return atan;
    }

    /*public float GetSizeOrto(float fieldOfView ) {
        var w = Mathf.Tan((fieldOfView / 180) * 3.14f) * 1.33f * 2;
        var sizeOrto = (w / Screen.width) * Screen.height;
        return sizeOrto;
    }*/

    private IEnumerator IEnumChangePosAndAngPersCamera (Vector3 endPos, Vector3 endAng, float finalDistans, float finalSizeCamera) {
        var startPosition = posCamera.localPosition;
        var startEulerAnglesY = angelYCamera.localEulerAngles.y;
        var startEulerAnglesX = angelXCamera.localEulerAngles.x;
        var startDisCamera = disCamera.localPosition.z;
        var startFieldOfView = perspectiveCamera.fieldOfView;
        var startPerlocalEulerAngles = perspectiveCamera.transform.localEulerAngles;
        float time = GlobalParams.timeToFly;
        float currentTime = 0;
        while(currentTime < time) {
            var t = currentTime / time;
            posCamera.position = Vector3.Lerp(startPosition, endPos, t);
            angelYCamera.localEulerAngles = new Vector3(0, Mathf.LerpAngle(startEulerAnglesY, endAng.y, t), 0);
            angelXCamera.localEulerAngles = new Vector3(Mathf.LerpAngle(startEulerAnglesX, endAng.x, t * t), 0, 0);
            disCamera.localPosition = new Vector3(0, 0, Mathf.Lerp(startDisCamera, 0, t * t * t));
            perspectiveCamera.transform.localEulerAngles =
                new Vector3(Mathf.LerpAngle(startPerlocalEulerAngles.x, 0, t),
                    Mathf.LerpAngle(startPerlocalEulerAngles.y, 0, t),
                    Mathf.LerpAngle(startPerlocalEulerAngles.z, 0, t));
            perspectiveCamera.fieldOfView = Mathf.Lerp(startFieldOfView, 60, t * t);

            currentTime += Time.deltaTime;
            yield return null;
        }
        perspectiveCamera.transform.localEulerAngles = Vector3.zero;
        perspectiveCamera.fieldOfView = 60;
        posCamera.position = endPos;
        angelYCamera.localEulerAngles = new Vector3(0, endAng.y, 0);
        angelXCamera.localEulerAngles = new Vector3(endAng.x, 0, 0);
        if (LittleShopController.inst) {
            LittleShopController.inst.SetAngelsForIcons(angelYCamera.localEulerAngles.y);
        }
        
        disCamera.localPosition = Vector3.zero;
        yield return null;
        currentTypeCameraOnState = TypeCameraOnState.perspective;
        moveCamera = null;
    }

    private IEnumerator IEnumChangePosAndAngOrtoCamera (Vector3 endPos, Vector3 endAng, float finalDistans, float finalSizeCamera, float time) {
        //endPos += new Vector3(-1, 0, -1);
        var startPosition = posCamera.localPosition;
        var startEulerAnglesY = angelYCamera.localEulerAngles.y;
        var startEulerAnglesX = angelXCamera.localEulerAngles.x;
        var startDisCamera = disCamera.localPosition.z;
        var startSizeCamereaOrto = listCamerasOrto[0].orthographicSize;

        //float time = GlobalParams.timeToFly;
        float currentTime = 0;
        while(currentTime < time) {
            var t = currentTime / time;
            posCamera.position = Vector3.Lerp(startPosition, endPos, t);
            angelYCamera.localEulerAngles = new Vector3(0, Mathf.LerpAngle(startEulerAnglesY, endAng.y, t), 0);
            angelXCamera.localEulerAngles = new Vector3(Mathf.LerpAngle(startEulerAnglesX, endAng.x, t * t), 0, 0);
            disCamera.localPosition = new Vector3(0, 0, Mathf.Lerp(startDisCamera, finalDistans, t * t * t));

            for(int i = 0; i < listCamerasOrto.Count; i++) {
                var mY = Mathf.LerpAngle(startSizeCamereaOrto, finalSizeCamera, t * t);
                listCamerasOrto[i].orthographicSize = mY;// > posCamera.position.y ? posCamera.position.y : mY;
            }

            //perspectiveCamera.fieldOfView = GetFieldOfView(listCamerasOrto[0].orthographicSize);

            LittleShopController.inst.SetAngelsForIcons(angelYCamera.localEulerAngles.y);

            currentTime += Time.deltaTime;
            yield return null;
        }

        for(int i = 0; i < listCamerasOrto.Count; i++) {
            listCamerasOrto[i].orthographicSize = finalSizeCamera;
        }
        //perspectiveCamera.fieldOfView = GetFieldOfView(listCamerasOrto[0].orthographicSize);
        posCamera.position = endPos;
        angelYCamera.localEulerAngles = new Vector3(0, endAng.y, 0);
        angelXCamera.localEulerAngles = new Vector3(endAng.x, 0, 0);
        LittleShopController.inst.SetAngelsForIcons(angelYCamera.localEulerAngles.y);
        disCamera.localPosition = new Vector3(0, 0, finalDistans);
        yield return null;
        currentTypeCameraOnState = TypeCameraOnState.orto;
        moveCamera = null;

    }

    private void Update () {
        //CheckPoint(0,1,MainLogic.inst.borders[2].position);
        //Debug.Log(moveCamera);
    }
}

