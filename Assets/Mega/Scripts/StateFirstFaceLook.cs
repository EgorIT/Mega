using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Mega.Scripts {
    public class StateFirstFaceLook : MonoBehaviour, iViewState {
        public static StateFirstFaceLook inst;
        public List<PointMoveOnFirstFaceScene> listPointMoveOnFirstFaceScene = new List<PointMoveOnFirstFaceScene>();

        public Coroutine clickCoroutine;
        public ViewStates viewStates;
        private TypeCameraOnState typeCameraOnState = TypeCameraOnState.perspective;
        public Transform StateLookTransform;
        public List<Transform> listStateLooksTransform = new List<Transform>();

        public Transform rootStateLooksTransform;

        public bool isHardMove;
        public PointerMoveToShop hardMovePointerMoveToShop;

        public void Awake() {
            inst = this;
        }

        public void Start () {
            ScanAllPoints();
            MainLogic.inst.listViewStates.Add(this);
        }

        public void ScanAllPoints() {
            var allPoints = rootStateLooksTransform.GetComponentsInChildren<Transform>();
            for (int i = 1; i < allPoints.Length; i++) {
                listStateLooksTransform.Add(allPoints[i]);
            }
        }

        public Vector3 FindPoint() {
            float dis = float.MaxValue;
            int index = 0;
            for (int i = 0; i < listStateLooksTransform.Count; i++) {
                var newDis = (listStateLooksTransform[i].position - MegaCameraController.inst.posCamera.position).sqrMagnitude;
                if (newDis < dis) {
                    dis = newDis;
                    index = i;
                }
            }
            return listStateLooksTransform[index].position;
        }

        public void StartState () {
            TableController.inst.HideAllTable();
            MainLogic.inst.showTableAndCaps = false;
            if (isHardMove) {
                MegaCameraController.inst.SetNewPosCamera(FindPoint(), new Vector3(0, GlobalParams.eulerAnglesForCameraInShops.y, 0),
                    GlobalParams.fieldOfViewOnFirstLook, GlobalParams.distansOnFirstLook, TypeMoveCamera.fast);
                StartCoroutine(WaitAfterStartState());
            }
            else {
                MegaCameraController.inst.SetNewPosCamera(hardMovePointerMoveToShop.lookPoint.position, 
                    hardMovePointerMoveToShop.lookPoint.eulerAngles,
                    GlobalParams.fieldOfViewOnFirstLook, 
                    GlobalParams.distansOnFirstLook, 
                    TypeMoveCamera.fast);
                StartCoroutine(WaitAfterStartState());
            }
        }

        public IEnumerator WaitAfterStartState() {
            yield return new WaitForSeconds(GlobalParams.timeToFly - 0.2f);
            MainLogic.inst.EnableRoof();
            
        }

        public void EndState () {
            //if (RoofProcessor.inst) {
            //    RoofProcessor.inst.DoTransparent();
            //}
            
        }

        public ViewStates GetViewStates () {
            return viewStates;
        }

        public TypeCameraOnState GetTypeCameraOnState () {
            return typeCameraOnState;
        }

        public void MoveForThisFloorPoint (PointMoveOnFirstFaceScene pointMoveOnFirstFaceScene) {
            if(!MegaCameraController.inst.isFirstLookScene) {
                return;
            }
            if(clickCoroutine != null) {
                StopCoroutine(clickCoroutine);
            }
            var v3 = new Vector3(pointMoveOnFirstFaceScene.pointerEventData.pointerCurrentRaycast.worldPosition.x,
                GlobalParams.distansEye,
                pointMoveOnFirstFaceScene.pointerEventData.pointerCurrentRaycast.worldPosition.z);
            clickCoroutine = StartCoroutine(IEnumCheckSwipe(v3, MegaCameraController.inst.currentEndAng));
        }

        public void MoveForThisShop (PointerMoveToShop pointerMoveToShop) {
            if(clickCoroutine != null) {
                StopCoroutine(clickCoroutine);
            }
            clickCoroutine = StartCoroutine(IEnumCheckSwipe(pointerMoveToShop.lookPoint.position, 
                pointerMoveToShop.lookPoint.eulerAngles));
        }

        public void StopClickCoroutine () {
            if(clickCoroutine != null) {
                StopCoroutine(clickCoroutine);
            }
        }

        public IEnumerator IEnumCheckSwipe (Vector3 pointMove, Vector3 endAng) {
            yield return new WaitForSeconds(0.5f);
            MegaCameraController.inst.SetNewPosCamera(pointMove, endAng,
                GlobalParams.fieldOfViewOnFirstLook, GlobalParams.distansOnFirstLook, TypeMoveCamera.normal);
        }
    }
}