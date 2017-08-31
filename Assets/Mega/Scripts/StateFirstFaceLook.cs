using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Mega.Scripts {
    public class StateFirstFaceLook : MonoBehaviour, iViewState {
        public ViewStates viewStates;
        public TypeCameraOnState typeCameraOnState;
        public Transform StateLookTransform;

        public void Start () {
            MainLogic.inst.listViewStates.Add(this);
        }

        public void EndState () {

        }

        public void SetThis() {
            MainLogic.inst.ChangeState(viewStates);
        }

        public void StartState () {
            MegaCameraController.inst.SetNewPosCamera(StateLookTransform.position, GlobalParams.eulerAnglesForCameraInShops, 0, typeCameraOnState);
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