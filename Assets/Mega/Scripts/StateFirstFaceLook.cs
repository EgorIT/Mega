using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Mega.Scripts {
    public class StateFirstFaceLook : MonoBehaviour, iViewState {
        public ViewStates viewStates;
        private TypeCameraOnState typeCameraOnState = TypeCameraOnState.perspective;
        public Transform StateLookTransform;
        public List<Transform> listStateLooksTransform = new List<Transform>();

        public Transform rootStateLooksTransform;

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

        
        /*public void SetThis() {
            MainLogic.inst.ChangeState(viewStates);
        }*/

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
            
            MegaCameraController.inst.SetNewPosCamera(FindPoint(), GlobalParams.eulerAnglesForCameraInShops, 
                GlobalParams.fieldOfViewOnFirstLook, GlobalParams.distansOnFirstLook, TypeMoveCamera.fast);
            StartCoroutine(WaitAfterStartState());
        }

        public IEnumerator WaitAfterStartState() {
            yield return new WaitForSeconds(GlobalParams.timeToFly - 0.2f);
            RoofProcessor.inst.DoStandard();
        }

        public void EndState () {
            RoofProcessor.inst.DoTransparent();
        }


        /*public IEnumerator IEnumLoadSceneFirstFaceLook() {
            
        }*/

        public ViewStates GetViewStates () {
            return viewStates;
        }

        public TypeCameraOnState GetTypeCameraOnState () {
            return typeCameraOnState;
        }

    }
}