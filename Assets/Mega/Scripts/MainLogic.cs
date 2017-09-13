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
        public bool isTest;

        public static MainLogic inst;
        
        public List<SceneForFirstFaceLook> AllFirstFaceLookScenes = new List<SceneForFirstFaceLook>();
        private ViewStates viewCurrentStates = ViewStates.none;
        public List<iViewState> listViewStates = new List<iViewState>();
        public List<Sprite> listPlans;

        public GameObject interfaceMega;
        public int currentNumberBlockShops;

        public List<Transform> borders = new List<Transform>();
        public List<GameObject> listToOnTEMP = new List<GameObject>();

        public bool iconFlag;
        public bool roofEnable = true;

        public void Awake() {
            inst = this;
        }

        public ViewStates GetViewCurrentStates () {
            return viewCurrentStates;
        }

        public void Start() {
            interfaceMega = FindObjectOfType<InterfaceController>().gameObject;
            if (interfaceMega) {
                interfaceMega.SetActive(false);
            }
            //SwapRoof(true);
            /*if (isTest) {
                var allToOff = GameObject.FindGameObjectsWithTag("TempOff");
                for (int i = 0; i < allToOff.Length; i++) {
                    allToOff[i].SetActive(false);
                }
            }*/
            /*for (int i = 0; i < listToOnTEMP.Count; i++) {
                if (listToOnTEMP[i] != null) {
                    if (!isTest) {
                        listToOnTEMP[i].SetActive(true);
                    }
                }
            }*/
        }

        public void SetQuarter(int number) {
            //spriteRendererPlan.sprite = listPlans[number];
            currentNumberBlockShops = number;
            viewCurrentStates = ViewStates.none;
            ChangeState(ViewStates.shops);
            FloorController.inst.SetQuartal(number);
        }


        public void Update() {
            if (MegaCameraController.inst.GetCurrentDistans() < GlobalParams.distansOnAllMega + 1000) {
                if (!roofEnable) {
                    roofEnable = true;
                    RoofProcessor.inst.DoStandard();
                    TableController.inst.DisAllShops();
                }

            }
            if(MegaCameraController.inst.GetCurrentDistans() > GlobalParams.distansOnAllMega + 1000 && MegaCameraController.inst.GetCurrentDistans() < -1001) {
                if(roofEnable) {
                    roofEnable = false;
                    RoofProcessor.inst.DoTransparent();
                    TableController.inst.ShowAllShops();
                }
            }


        }

        public void ChangeState(ViewStates newState) {
            if (viewCurrentStates == newState) {
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