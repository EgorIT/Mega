using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
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
        
        public List<SceneForFirstFaceLook> AllFirstFaceLookScenes = new List<SceneForFirstFaceLook>();
        private ViewStates viewCurrentStates = ViewStates.none;

        public List<iViewState> listViewStates = new List<iViewState>();

        public Transform AllSeeTransform;
        public List<Sprite> listPlans;
        //public SpriteRenderer spriteRendererPlan;

        public GameObject interfaceMega;

        public int currentNumberBlockShops;

        public List<Transform> borders = new List<Transform>();

        public void Awake() {
            inst = this;
        }

        public ViewStates GetViewCurrentStates () {
            return viewCurrentStates;
        }

        public void Start() {
            if (Application.platform != RuntimePlatform.WindowsEditor) {
                //WindowMod.StartFromController();
            }
            
            //GoToAllSeeTransform();
        }

        public void SetQuarter(int number) {
            //spriteRendererPlan.sprite = listPlans[number];
            currentNumberBlockShops = number;
            viewCurrentStates = ViewStates.none;
            ChangeState(ViewStates.shops);
        }



        public void ChangeState(ViewStates newState) {
            if (viewCurrentStates == newState) {
                return;
            }
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
            StartCoroutine(IEnumGoToSceneFirstLook(currentSceneName, currentTransform));
        }

        public IEnumerator IEnumGoToSceneFirstLook(string nameScene, Transform transform) {
            MegaCameraController.inst.SetNewPosCamera(transform.position, GlobalParams.eulerAnglesForCameraInShops, 0, TypeCameraOnState.perspective);
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadSceneAsync(nameScene);
        }

        /*public void GoToAllSeeTransform () {
            MegaCameraController.inst.SetNewPosCamera(AllSeeTransform.position, 
                GlobalParams.startLocalEulerAnglesForCamera, -150, GlobalParams.sizeOrtocameraAllMega);
            LittleShopController.inst.DisAllShops();
        }*/

        public void GoMiddleView() {
            LittleShopController.inst.ShowAllShops();
        }

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