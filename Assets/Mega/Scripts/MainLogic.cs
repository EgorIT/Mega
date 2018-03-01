﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Mega.Scripts {
    public enum ViewStates {
        one,
        allMega,
        shops,
        firstFaceLook,
        none
    }

    public class MainLogic : MonoBehaviour {
        public static MainLogic inst;

        public List<SceneForFirstFaceLook> AllFirstFaceLookScenes = new List<SceneForFirstFaceLook>();
        private ViewStates viewCurrentStates = ViewStates.none;
        public List<iViewState> listViewStates = new List<iViewState>();
        public List<Sprite> listPlans;

        [HideInInspector]
        public GameObject interfaceMega;

        public int currentNumberBlockShops;

        public List<Transform> borders = new List<Transform>();

        public bool iconFlag;
        private bool roofEnable = true;

        public GameObject parkNow;
        public GameObject parkAfter;

        public GameObject probes;

        public bool showTableAndCaps;

        public Video mainVideo;
        public VideoLogo logo;

        
        public void Awake () {
            inst = this;
            FindNeedObject();
        }

        public ViewStates GetViewCurrentStates () {
            return viewCurrentStates;
        }

        public float currentTime;

        public Toggle firstView;
        public Toggle fullscreen;
        public Toggle moveCamera;
        public Toggle rotateCamera;
        
        [ContextMenu("GoFirstLook")]
        public void GoFirstLook() {//ot pervogo lica
            if (MegaCameraController.inst.isFirstLookScene) {
                return;
            }
            StateFirstFaceLook.inst.pointerEventData = null;
            KeyController.inst.SwapZoom();
        }

        [ContextMenu("GoAllLook")]
        public void GoAllLook () {//vsy mega
            if(!MegaCameraController.inst.isFirstLookScene) {
                return;
            }
            KeyController.inst.SwapZoom();
        }

        [ContextMenu("Set360")]
        public void Set360 () {//360 
            KeyController.inst.Set360();
        }

        [ContextMenu("SetPanorama")]
        public void SetPanorama() {//panorama 
            KeyController.inst.SetPanoram();
        }

        public void ResetTime () {
            currentTime = 0;
        }

        public void FindNeedObject () {
            parkNow = GameObject.FindGameObjectWithTag("parkNow");
            parkAfter = GameObject.FindGameObjectWithTag("parkAfter");
            interfaceMega = FindObjectOfType<InterfaceController>().gameObject;
            interfaceMega.SetActive(false);
        }

        public void Start () {
            //Debug.Log("w = " + Screen.currentResolution.width);
            //Debug.Log("h = " + Screen.currentResolution.height);
            //if(/*boolWindowMod && */Application.platform != RuntimePlatform.WindowsEditor) {
            //    StartCoroutine(IEnumWaitWindowMod());
            //}
            RoadsProcessor.inst.StartFromController();
            RoadsProcessor.inst.ToOldDo();
            
            //RoadsProcessor.inst.ToNewDo();
            StartCoroutine(IEnumWaitAfterStart());
        }

        public IEnumerator IEnumWaitAfterStart() {
            yield return new WaitForSeconds(1f);
            RoadsProcessor.inst.ToNewDo();
        }

        //public IEnumerator IEnumWaitWindowMod () {
        //    yield return new WaitForSeconds(2f);
        //    WindowMod.StartFromController();
        //}

        public void SwapParking (bool showNew) {
            if(showNew) {
                RoadsProcessor.inst.ToOldDo();
            } else {
                RoadsProcessor.inst.ToNewDo();
            }
        }

        public void GoAllMega () {
            MegaCameraController.inst.distansAllMega = GlobalParams.distansOnAllMega;
            MegaCameraController.inst.stateLookVector3AllMega = new Vector3(12f, 0, -70f);
            ChangeState(ViewStates.allMega);
            if(TableController.inst) {
                TableController.inst.SetAngelsForIcons(MegaCameraController.inst.angelYCamera.localEulerAngles.y);
            }
            DisRoof(3);
        }

        public void GoAllRoads() {
            MegaCameraController.inst.distansAllMega = GlobalParams.maxDistancePesr;
            MegaCameraController.inst.stateLookVector3AllMega = new Vector3(-37f, 0, -220f);
            ChangeState(ViewStates.allMega);
            if(TableController.inst) {
                TableController.inst.SetAngelsForIcons(MegaCameraController.inst.angelYCamera.localEulerAngles.y);
            }
            EnableRoof();
        }

        public void DisRoof (float time) {
            if(roofEnable) {
                roofEnable = false;
                StartCoroutine(IEnumDisRoof(time));
            }
        }

        public void EnableRoof () {
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

        public void GoVideo () {
            StateOne.inst.isShowVideo = true;
            ChangeState(ViewStates.one);
        }

        public void Update () {
            if(viewCurrentStates != ViewStates.one) {
                currentTime += Time.deltaTime;
            }
            
            if(currentTime > GlobalParams.needTimeToSleep && GetViewCurrentStates() != ViewStates.one) {
                currentTime = GlobalParams.needTimeToSleep;
                ChangeState(ViewStates.one);
                //Timeline.inst.Sleep();
                AllCaps.allCaps.ActivateAll();
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
                if(listViewStates[i].GetViewStates() == newState) {
                    listViewStates[i].StartState();
                }
            }

            viewCurrentStates = newState;
        }

        public void GoToSceneFirstLook (string nameShop) {
            string currentSceneName = string.Empty;
            Transform currentTransform = null;
            for(int i = 0; i < AllFirstFaceLookScenes.Count; i++) {
                for(int j = 0; j < AllFirstFaceLookScenes[i].allShopsName.Count; j++) {
                    if(AllFirstFaceLookScenes[i].allShopsName[j] == nameShop) {
                        currentSceneName = AllFirstFaceLookScenes[i].nameScene;
                        currentTransform = AllFirstFaceLookScenes[i].currentTransformShop[j];
                    }
                }
            }
            Debug.Log(currentSceneName);
            Debug.Log(currentTransform);
            //StartCoroutine(IEnumGoToSceneFirstLook(currentSceneName, currentTransform));
        }

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