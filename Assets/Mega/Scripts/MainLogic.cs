using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        public bool isTest;

        public List<SceneForFirstFaceLook> AllFirstFaceLookScenes = new List<SceneForFirstFaceLook>();
        private ViewStates viewCurrentStates = ViewStates.none;
        public List<iViewState> listViewStates = new List<iViewState>();
        public List<Sprite> listPlans;

        public GameObject interfaceMega;
        public int currentNumberBlockShops;

        public List<Transform> borders = new List<Transform>();
        public List<GameObject> listToOnTEMP = new List<GameObject>();

        public bool iconFlag;
        private bool roofEnable = true;

        public GameObject parkNow;
        public GameObject parkAfter;

        public bool boolWindowMod;

        public void Awake() {
            inst = this;
        }

        public ViewStates GetViewCurrentStates () {
            return viewCurrentStates;
        }

        public float currentTime;
        

        public void ResetTime() {
            MainLogic.inst.currentTime = 0;
        }

        public void Start() {
            parkNow = GameObject.FindGameObjectWithTag("parkNow");
            parkNow.SetActive(true);
            parkAfter = GameObject.FindGameObjectWithTag("parkAfter");
            parkAfter.SetActive(false);
            Debug.Log(FindObjectOfType<InterfaceController>().name);
            Debug.Log(FindObjectOfType<InterfaceController>().gameObject.name);
            interfaceMega = FindObjectOfType<InterfaceController>().gameObject;
            if (interfaceMega) {
                interfaceMega.SetActive(false);
            }

            if (!isTest) {
                for (int i = 0; i < listToOnTEMP.Count; i++) {
                    listToOnTEMP[i].SetActive(true);
                }
            }
            Debug.Log("w = " + Screen.currentResolution.width);
            Debug.Log("h = " + Screen.currentResolution.height);
            if (boolWindowMod && Application.platform != RuntimePlatform.WindowsEditor) {
                StartCoroutine(IEnumWaitWindowMod());
            }
        }

        public IEnumerator IEnumWaitWindowMod() {
            yield return new WaitForSeconds(1);
            WindowMod.StartFromController();
        }

        public void SetQuarter(int number) {
            if (number <= 2) {
                if (!parkNow.activeInHierarchy) {
                    parkNow.SetActive(true);
                }
                if(parkAfter.activeInHierarchy) {
                    parkAfter.SetActive(false);
                }
            } else {
                if(parkNow.activeInHierarchy) {
                    parkNow.SetActive(false);
                }
                if(!parkAfter.activeInHierarchy) {
                    parkAfter.SetActive(true);
                }
            }
            //TableController.inst.DisAllShops();
            MegaCameraController.inst.distansAllMega = GlobalParams.distansOnAllMega;
            MegaCameraController.inst.stateLookVector3AllMega = new Vector3(12f, 0, -70f);
            ChangeState(ViewStates.allMega);
            //spriteRendererPlan.sprite = listPlans[number];
            currentNumberBlockShops = number;
            viewCurrentStates = ViewStates.none;
            ChangeState(ViewStates.shops);
            FloorController.inst.SetQuartal(number);
        }

        public void GoAllMega() {
            MegaCameraController.inst.distansAllMega = GlobalParams.distansOnAllMega;
            MegaCameraController.inst.stateLookVector3AllMega = new Vector3(12f, 0, -70f);
            ChangeState(ViewStates.allMega);
        }

        public void DisRoof(float time) {
            if (roofEnable) {
                roofEnable = false;
                StartCoroutine(IEnumDisRoof(time));
            }
            
        }

        public void EnebleRoof() {
            if (!roofEnable) {
                roofEnable = true;
                RoofProcessor.inst.DoStandard();
            }
        }

        public IEnumerator IEnumDisRoof(float time) {
            yield return new WaitForSeconds(time);
            if(RoofProcessor.inst) {
                RoofProcessor.inst.DoTransparent();
            }
        }

        public void Update() {
            currentTime += Time.deltaTime;
            if (currentTime > GlobalParams.needTimeToSleep) {
                currentTime = GlobalParams.needTimeToSleep;
                ChangeState(ViewStates.one);
                Timeline.timeline.Sleep();
                AllCaps.allCaps.ActivateAll();
            }

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

        public void ChangeState(ViewStates newState) {
            if (viewCurrentStates == newState && newState != ViewStates.allMega) {
                return;
            }
            //Debug.Log("newState = " + newState.ToString());
            for (int i = 0; i < listViewStates.Count; i++) {
                if (listViewStates[i].GetViewStates() == viewCurrentStates) {
                    listViewStates[i].EndState();
                }
                if(listViewStates[i].GetViewStates() == newState) {
                    listViewStates[i].StartState();
                }
            }

            viewCurrentStates = newState;
        }

        public void GoToSceneFirstLook(string nameShop) {
            string currentSceneName = string.Empty;
            Transform currentTransform = null;
            for (int i = 0; i < AllFirstFaceLookScenes.Count; i++) {
                for (int j = 0; j < AllFirstFaceLookScenes[i].allShopsName.Count; j++) {
                    if (AllFirstFaceLookScenes[i].allShopsName[j] == nameShop) {
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

        public void GoViewStateOne() {
           
        }

        public void ShowLittleView() {
            
        }

        /*public void SetPlan(int i) {
            spriteRendererPlan.sprite = listPlans[i];
            GoToAllSeeTransform();
        }*/
    }
}