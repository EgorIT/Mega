using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Mega.Scripts.Interface;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Mega.Scripts {
    public enum ViewStates {
        one,
        allMega,
        //shops,
        firstFaceLook,
        none
    }

    public class MainLogic : MonoBehaviour {
        public static MainLogic inst;

        //public List<SceneForFirstFaceLook> AllFirstFaceLookScenes = new List<SceneForFirstFaceLook>();
        private ViewStates viewCurrentStates = ViewStates.none;
        public List<iViewState> listViewStates = new List<iViewState>();
        //public List<Sprite> listPlans;

        ///[HideInInspector]
        ///public GameObject interfaceMega;

        public GameObject zoneView;
        public GameObject zoneClick;

        public int currentNumberBlockShops;

        public List<Transform> borders = new List<Transform>();

        public bool iconFlag;
        private bool roofEnable = true;

        public GameObject parkNow;
        //public GameObject parkAfter;

        public GameObject probes;

        public bool showTableAndCaps;

        public bool isRoadLook;

        public Action<int> actionSelectZone;

        public void Awake () {
            inst = this;
            FindNeedObject();
        }

        public ViewStates GetViewCurrentStates () {
            return viewCurrentStates;
        }

        public float currentTime;

        [ContextMenu("SetAllLook")]
        public void SetAllLook () {//ot pervogo lica
            if(!MegaCameraController.inst.isFirstLookScene) {
                return;
            }
            StateFirstFaceLook.inst.pointerEventData = null;
            //KeyController.inst.SwapFirstLookAndAllMega();
        }

        //[ContextMenu("SetFirstLook")]
        //public void SetFirstLook() {//ot pervogo lica
        //    if (MegaCameraController.inst.isFirstLookScene) {
        //        return;
        //    }
        //    StateFirstFaceLook.inst.pointerEventData = null;
        //    //KeyController.inst.SwapFirstLookAndAllMega();
        //}

        [ContextMenu("SetZoom")]
        public void SetZoom () {//vsy mega
            ButonAdds.inst.SetActivCurrentUpButton(ButonAdds.inst.zoomCameraImage);
            KeyController.inst.SetZoom();
        }

        [ContextMenu("SetRotate")]
        public void SetRotate () {//360 
            ButonAdds.inst.SetActivCurrentUpButton(ButonAdds.inst.rotateCameraImage);
            KeyController.inst.SetRotate();
            GoAllMega(false);
        }

        [ContextMenu("SetMoveAllMega")]
        public void SetMoveAllMega () {//panorama 
            ButonAdds.inst.SetActivCurrentUpButton(ButonAdds.inst.moveCameraImage);
            KeyController.inst.SetMoveAllMega();
        }

        public void ResetTime () {
            currentTime = 0;
        }

        public void FindNeedObject () {
            parkNow = GameObject.FindGameObjectWithTag("parkNow");
            //parkAfter = GameObject.FindGameObjectWithTag("parkAfter");
            //interfaceMega = FindObjectOfType<InterfaceController>().gameObject;
            //interfaceMega.SetActive(false);
        }

        public void Start () {
            RoadsProcessor.inst.StartFromController();
            //StartCoroutine(IEnumWaitAfterStart());
            SetMoveAllMega();
            GoAllMega(true);
            SetActivZoneParking(false);

            actionSelectZone += CallbackSelectZone;
        }

        public void CallbackSelectZone(int var) {
            //Debug.Log(var);
        }

        //public IEnumerator IEnumWaitAfterStart() {
        //    RoadsProcessor.inst.ToNewDo();
        //    yield return new WaitForSeconds(1f);
        //    //RoadsProcessor.inst.ToOldDo();
        //}

        //public void SwapParking (bool showNew) {
        //    if(showNew) {
        //        RoadsProcessor.inst.ToOldDo();
        //    } else {
        //        RoadsProcessor.inst.ToNewDo();
        //    }
        //}

        [ContextMenu("GoAllMega")]
        public void GoAllMega (bool showRoof) {
            MegaCameraController.inst.distansAllMega = GlobalParams.distansOnAllMega;
            MegaCameraController.inst.stateLookVector3AllMega = new Vector3(12f, 0, -70f);
            ChangeState(ViewStates.allMega);
            //if(TableController.inst) {
                TableController.inst.SetAngelsForIcons(MegaCameraController.inst.angelYCamera.localEulerAngles.y);
            //}
            if (showRoof) {
                ShowRoof();
            } else {
                HideRoof(3);
            }
       
            ButonAdds.inst.ShowUpButton();
            isRoadLook = false;
            KidsArrowController.inst.HideArrow();
            StockArrowController.inst.HideArrow();
        }

        [ContextMenu("GoStock")]
        public void GoStock () {
            MegaCameraController.inst.distansAllMega = GlobalParams.stockDistancePesr;
            MegaCameraController.inst.stateLookVector3AllMega = new Vector3(83f, 0, -45f);
            ChangeState(ViewStates.allMega);
            if(TableController.inst) {
                TableController.inst.SetAngelsForIcons(MegaCameraController.inst.angelYCamera.localEulerAngles.y);
            }
            StockArrowController.inst.ShowArrow();
            //InterfaceController.inst.HardHideBasic();
        }

        [ContextMenu("GoKids")]
        public void GoKids () {
            MegaCameraController.inst.distansAllMega = GlobalParams.kidsDistancePesr;
            MegaCameraController.inst.stateLookVector3AllMega = new Vector3(140f, 0, -55f);
            ChangeState(ViewStates.allMega);
            if(TableController.inst) {
                TableController.inst.SetAngelsForIcons(MegaCameraController.inst.angelYCamera.localEulerAngles.y);
            }
            KidsArrowController.inst.ShowArrow();
            //InterfaceController.inst.HardHideBasic();
        }


        public void GoAllRoads() {
            MegaCameraController.inst.distansAllMega = GlobalParams.maxDistancePesr;
            MegaCameraController.inst.stateLookVector3AllMega = new Vector3(-37f, 0, -220f);
            ChangeState(ViewStates.allMega);
            if(TableController.inst) {
                TableController.inst.SetAngelsForIcons(MegaCameraController.inst.angelYCamera.localEulerAngles.y);
            }
            SetActivZoneParking(true);
            ParkingArrowsController.inst.SetActivArrow(true);
            ShowRoof();
            SetMoveAllMega();
            ButonAdds.inst.HideUpButton();

            isRoadLook = true;
        }

        public void SetActivZoneParking(bool var) {
            zoneView.SetActive(var);
            zoneClick.SetActive(var);
        }

        public void HideRoof (float time) {
            if(roofEnable) {
                roofEnable = false;
                StartCoroutine(IEnumDisRoof(time));
            }
        }

        public void ShowRoof () {
            if(!roofEnable) {
                roofEnable = true;
                if(RoofProcessor.inst) {
                    RoofProcessor.inst.DoStandard();
                }
            }
        }

        public IEnumerator IEnumDisRoof (float time) {
            yield return new WaitForSeconds(time);

            if(RoofProcessor.inst) {
                RoofProcessor.inst.DoTransparent();
            }
        }

        [ContextMenu("GoVideo1")]
        public void GoVideo1 () {
            StateOne.inst.isShowVideo = true;
            VideoController.inst.SetSourse(VideoController.TypeVideo.changePlate);
            ChangeState(ViewStates.one);
        }

        [ContextMenu("GoVideo2")]
        public void GoVideo2 () {
            StateOne.inst.isShowVideo = true;
            VideoController.inst.SetSourse(VideoController.TypeVideo.toBigMega);
            ChangeState(ViewStates.one);
        }

        public void Update () {
            if(viewCurrentStates != ViewStates.one) {
                currentTime += Time.deltaTime;
            }
            
            if(currentTime > GlobalParams.needTimeToSleep && GetViewCurrentStates() != ViewStates.one) {
                currentTime = GlobalParams.needTimeToSleep;
                //ChangeState(ViewStates.one);
                //Timeline.inst.Sleep();
                //AllCaps.inst.ActivateAll();
                GoAllMega(true);
            }


            if(MegaCameraController.inst.GetCurrentDistans() >= GlobalParams.maxDistancePesr && MegaCameraController.inst.GetCurrentDistans() < -100) {
                if(!showTableAndCaps) {
                    showTableAndCaps = true;
                    if(TableController.inst) {
                        TableController.inst.ShowAllTable();
                    }
                    if(AllCaps.inst) {
                        AllCaps.inst.ShowAllCaps();
                    }
                }
            }

            //if(MegaCameraController.inst.GetCurrentDistans() < -5000) {
            //    if(showTableAndCaps) {
            //        showTableAndCaps = false;
            //        if(TableController.inst) {
            //            TableController.inst.HideAllTable();
            //        }
            //        if(AllCaps.inst) {
            //            AllCaps.inst.HideAllCaps();
            //        }
            //    }
            //}

            //if (MegaCameraController.inst.GetCurrentDistans() < GlobalParams.distansOnAllMega - 500) {
            //    if (!roofEnable) {
            //        roofEnable = true;
            //        if (RoofProcessor.inst) {
            //            RoofProcessor.inst.DoStandard();
            //        }
            //        //TableController.inst.DisAllShops();
            //    }
            //
            //}
            //if(MegaCameraController.inst.GetCurrentDistans() > GlobalParams.distansOnAllMega + 1000 && MegaCameraController.inst.GetCurrentDistans() < -1001) {
            //    if(roofEnable) {
            //        roofEnable = false;
            //        if (RoofProcessor.inst) {
            //            RoofProcessor.inst.DoTransparent();
            //        }
            //        //TableController.inst.ShowAllShops();
            //    }
            //}


        }

        public void ChangeState (ViewStates newState) {
            //if(viewCurrentStates == newState && newState != ViewStates.allMega) {
            //    return;
            //}
            //Debug.Log("newState = " + newState.ToString());
            MegaCameraController.inst.PauseForUi();
            for(int i = 0; i < listViewStates.Count; i++) {
                if(listViewStates[i].GetViewStates() == viewCurrentStates) {
                    listViewStates[i].EndState();
                }
            }

            for(int i = 0; i < listViewStates.Count; i++) {
                if(listViewStates[i].GetViewStates() == newState) {
                    listViewStates[i].StartState();
                }
            }

            viewCurrentStates = newState;
        }

        //public void GoToSceneFirstLook (string nameShop) {
        //    string currentSceneName = string.Empty;
        //    Transform currentTransform = null;
        //    for(int i = 0; i < AllFirstFaceLookScenes.Count; i++) {
        //        for(int j = 0; j < AllFirstFaceLookScenes[i].allShopsName.Count; j++) {
        //            if(AllFirstFaceLookScenes[i].allShopsName[j] == nameShop) {
        //                currentSceneName = AllFirstFaceLookScenes[i].nameScene;
        //                currentTransform = AllFirstFaceLookScenes[i].currentTransformShop[j];
        //            }
        //        }
        //    }
        //    Debug.Log(currentSceneName);
        //    Debug.Log(currentTransform);
        //    //StartCoroutine(IEnumGoToSceneFirstLook(currentSceneName, currentTransform));
        //}

        /*public IEnumerator IEnumGoToSceneFirstLook(string nameScene, Transform transform) {
            MegaCameraController.inst.SetNewPosCamera(transform.position, GlobalParams.eulerAnglesForCameraInAllMega, 5, TypeCameraOnState.orto);
            yield return new WaitForSeconds(1.0f);
            SceneManager.LoadSceneAsync(nameScene);
        }*/

        /*public void GoToAllSeeTransform () {
            MegaCameraController.inst.SetNewPosCamera(AllSeeTransform.position, 
                GlobalParams.startLocalEulerAnglesForCamera, -150, GlobalParams.sizeOrtocameraAllMega);
            LittleShopController.inst.DisAllShops();
        }*/

        /*public void GoMiddleView() {
            TableController.inst.ShowAllShops();
        }*/

        public void GoViewStateOne () {
            ChangeState(ViewStates.one);
        }

        ///public void GoViewAllMega () {
        ///    ChangeState(ViewStates.allMega);
        ///}
        ///
        ///public void ShowLittleView () {
        ///
        ///}

        /*public void SetPlan(int i) {
            spriteRendererPlan.sprite = listPlans[i];
            GoToAllSeeTransform();
        }*/
    }
}