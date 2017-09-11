using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Assets.Mega.Scripts {
    public class MoveFirstFaceController : MonoBehaviour {
        public static MoveFirstFaceController inst;
        public List<PointMoveOnFirstFaceScene> listPointMoveOnFirstFaceScene = new List<PointMoveOnFirstFaceScene>();

        public Coroutine clickCoroutine;

        public void Awake() {
            inst = this;
        }

        public void Start() {
            //MegaCameraController.inst.currentTypeCameraOnState = TypeCameraOnState.perspective;
        }

        public void MoveForThisFloorPoint(PointMoveOnFirstFaceScene pointMoveOnFirstFaceScene) {
            if (!MegaCameraController.inst.isFirstLookScene) {
                return;
            }
            if (clickCoroutine != null) {
                StopCoroutine(clickCoroutine);
            }
            //Debug.Log(pointMoveOnFirstFaceScene.pointerEventData.pointerCurrentRaycast.worldPosition);
            //var g = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //g.transform.position = pointMoveOnFirstFaceScene.pointerEventData.pointerCurrentRaycast.worldPosition;

            clickCoroutine = StartCoroutine(IEnumCheckSwipe(pointMoveOnFirstFaceScene.pointerEventData.pointerCurrentRaycast.worldPosition, 
                1.638f, MegaCameraController.inst.currentEndAng));
        }

        public void MoveForThisShop (PointerMoveToShop pointerMoveToShop) {
            if(clickCoroutine != null) {
                StopCoroutine(clickCoroutine);
            }
            clickCoroutine = StartCoroutine(IEnumCheckSwipe(pointerMoveToShop.lookPoint.position, 0, pointerMoveToShop.lookPoint.eulerAngles));
        }

        public void StopClickCoroutine() {
            if(clickCoroutine != null) {
                StopCoroutine(clickCoroutine);
            }
        }


        public IEnumerator IEnumCheckSwipe (Vector3 pointMove, float deltaY, Vector3 endAng) {
            yield return new WaitForSeconds(0.5f);
            MegaCameraController.inst.SetNewPosCamera(pointMove + Vector3.up * deltaY, endAng, 
                GlobalParams.fieldOfViewOnFirstLook, GlobalParams.distansOnFirstLook, TypeMoveCamera.normal);
        }

        public void GoToMainScene() {
            SceneManager.LoadScene("New");
        }
    }
}