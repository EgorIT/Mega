using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Mega.Scripts {
    public class StateAllMega : MonoBehaviour, iViewState {
        public ViewStates viewStates;
        public TypeCameraOnState typeCameraOnState;

        public Transform StateLookTransform;
        
        public void Start () {
            MainLogic.inst.listViewStates.Add(this);
        }
        
        public void EndState () {

        }

        public void StartState () {
            MainLogic.inst.interfaceMega.SetActive(true);
            MegaCameraController.inst.SetNewPosCamera(StateLookTransform.position, GlobalParams.eulerAnglesForCameraInAllMega, GlobalParams.sizeOrtocameraAllMega, typeCameraOnState);
        }

        public ViewStates GetViewStates () {
            return viewStates;
        }

        public TypeCameraOnState GetTypeCameraOnState () {
            return typeCameraOnState;
        }

    }
}