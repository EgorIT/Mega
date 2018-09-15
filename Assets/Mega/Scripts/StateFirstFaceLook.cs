using System.Collections;
using System.Collections.Generic;
using Assets.Mega.Scripts.Interface;
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

        //public int countTouch;
        //public float timeDeltaTouch;
        public PointerEventData pointerEventData;

        public List<GameObject> roadList;

        public void Awake () {
            inst = this;
        }

        public void Start () {
            ScanAllPoints();
            MainLogic.inst.listViewStates.Add(this);
        }

        //public void GoToNearestArrow () {
        //    Ray ray = new Ray();
        //    if(pointerEventData == null) {
        //        Debug.Log("pointerEventData null");
        //        ray = MegaCameraController.inst.ortoRayCastCamera.ScreenPointToRay(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f));
        //    } else {
        //        ray = MegaCameraController.inst.ortoRayCastCamera.ScreenPointToRay(pointerEventData.position);
        //    }
        //    pointerEventData = null;
        //    RaycastHit hit;
        //    Vector3 pos = Vector3.zero;
        //    if(Physics.Raycast(ray, out hit)) {
        //        pos = hit.point;
        //    }
        //    float dis = float.MaxValue;
        //    int index = 0;
        //    for(int i = 0; i < ArrowController.inst.listArrowOnFloor.Count; i++) {
        //
        //        var newDis = (ArrowController.inst.listArrowOnFloor[i].gameObject.transform.position - pos).sqrMagnitude;
        //        //Debug.Log(newDis + " " + i);
        //        if(newDis < dis) {
        //            dis = newDis;
        //            index = i;
        //        }
        //    }
        //    hardMovePointerMoveToShop = ArrowController.inst.listArrowOnFloor[index].gameObject.transform.position;
        //    MegaCameraController.inst.GoToFirstLook(false);
        //}

        public void ScanAllPoints () {
            var allPoints = rootStateLooksTransform.GetComponentsInChildren<Transform>();
            for(int i = 1; i < allPoints.Length; i++) {
                allPoints[i].GetComponent<MeshRenderer>().enabled = false;
                listStateLooksTransform.Add(allPoints[i]);
            }
        }

        public Vector3 FindPoint () {
            float dis = float.MaxValue;
            int index = 0;
            for(int i = 0; i < listStateLooksTransform.Count; i++) {
                var newDis = (listStateLooksTransform[i].position - MegaCameraController.inst.posCamera.position).sqrMagnitude;
                if(newDis < dis) {
                    dis = newDis;
                    index = i;
                }
            }
            return new Vector3(listStateLooksTransform[index].position.x, GP.distansEye, listStateLooksTransform[index].position.z);
        }

        public void StartState () {
            Debug.Log("StartState StateFirstFaceLook");
            //Debug.Log("go StartState");
            if(isHardMove) {
                MegaCameraController.inst.SetNewPosCamera(FindPoint(), new Vector3(0, GP.eulerAnglesForCameraInShops.y, 0),
                    GP.fieldOfViewOnFirstLook, GP.distansOnFirstLook, TypeMoveCamera.fast);
                StartCoroutine(WaitAfterStartState());
            } else {
                MegaCameraController.inst.SetNewPosCamera(hardMovePointerMoveToShop.lookPoint.position,
                    hardMovePointerMoveToShop.lookPoint.eulerAngles,
                    GP.fieldOfViewOnFirstLook,
                    GP.distansOnFirstLook,
                    TypeMoveCamera.fast);
                StartCoroutine(WaitAfterStartState());
            }
            SetActivRoad(false);
            KidsArrowController.inst.HideArrow();
            StockArrowController.inst.HideArrow();
        }

        public IEnumerator WaitAfterStartState () {
            yield return new WaitForSeconds(GP.timeToFly - 1.8f);
            TableController.inst.HideAllTable();
            AllCaps.inst.HideAllCaps();
            MainLogic.inst.ShowRoof();
        }

        public void EndState () {
            Debug.Log("EndState StateFirstFaceLook");
            MainLogic.inst.showTableAndCaps = false;
            SetActivRoad(true);
            MegaCameraController.inst.isFirstLookScene = false;
            //InterfaceController.inst.ShowBasic();
            //if (RoofProcessor.inst) {
            //    RoofProcessor.inst.DoTransparent();
            //}

        }

        public void SetActivRoad (bool var) {
            for(int i = 0; i < roadList.Count; i++) {
                roadList[i].SetActive(var);
            }
        }


        public ViewStates GetViewStates () {
            return viewStates;
        }

        public TypeCameraOnState GetTypeCameraOnState () {
            return typeCameraOnState;
        }



        public void MoveForThisArrowOnFloor (ArrowOnFloor arrowOnFloor) {
            StopClickCoroutine();
            var v3 = new Vector3(/*arrowOnFloor.pointerEventData.pointerCurrentRaycast.worldPosition.x*/arrowOnFloor.transform.position.x,
                GP.distansEye,
                /*arrowOnFloor.pointerEventData.pointerCurrentRaycast.worldPosition.z*/arrowOnFloor.transform.position.z);

            clickCoroutine = StartCoroutine(IEnumCheckSwipe(v3, MegaCameraController.inst.currentEndAng));
        }

        public void StopClickCoroutine () {
            if(clickCoroutine != null) {
                StopCoroutine(clickCoroutine);
            }
            clickCoroutine = null;
        }

        public IEnumerator IEnumCheckSwipe (Vector3 pointMove, Vector3 endAng) {
            yield return new WaitForSeconds(0.2f);
            ArrowController.inst.AllArrowBack();
            //Debug.Log(pointMove.y);
            MegaCameraController.inst.SetNewPosCamera(pointMove, endAng,
                GP.fieldOfViewOnFirstLook, GP.distansOnFirstLook, TypeMoveCamera.normal);
        }

        //public void GoToNearestShop () {
        //    Ray ray = new Ray();
        //    if(pointerEventData == null) {
        //        Debug.Log("pointerEventData null");
        //        ray = MegaCameraController.inst.ortoRayCastCamera.ScreenPointToRay(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f));
        //    } else {
        //        ray = MegaCameraController.inst.ortoRayCastCamera.ScreenPointToRay(pointerEventData.position);
        //    }
        //
        //    pointerEventData = null;
        //    RaycastHit hit;
        //    Vector3 pos = Vector3.zero;
        //    if(Physics.Raycast(ray, out hit)) {
        //        pos = hit.point;
        //    }
        //
        //    float dis = float.MaxValue;
        //    int index = 0;
        //
        //    for(int i = 0; i < AllCaps.inst.listShopCaps.Count; i++) {
        //        if(AllCaps.inst.listShopCaps[i].pointerMoveToShop != null) {
        //            var newDis = (AllCaps.inst.listShopCaps[i].pointerMoveToShop.lookPoint.position - pos).sqrMagnitude;
        //            //Debug.Log(newDis + " " + i);
        //            if(newDis < dis) {
        //                dis = newDis;
        //                index = i;
        //            }
        //        } else {
        //            //Debug.Log(" null " + AllCaps.inst.listShopCaps[i].name);
        //        }
        //    }
        //
        //    StateFirstFaceLook.inst.hardMovePointerMoveToShop = AllCaps.inst.listShopCaps[index].pointerMoveToShop;
        //    MegaCameraController.inst.GoToFirstLook(false);
        //}
    }
}