using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Mega.Scripts {
    public class StateAllMega : MonoBehaviour, iViewState {
        public ViewStates viewStates;
        private TypeCameraOnState typeCameraOnState = TypeCameraOnState.perspective;
        
        //public Transform StateLookTransform;
        
        public void Start () {
            MainLogic.inst.listViewStates.Add(this);
        }
        
        public void EndState () {

        }

        public void StartState () {
            //TableController.inst.HideAllTable();
            //AllCaps.inst.HideAllCaps();
            MegaCameraController.inst.isFirstLookScene = false;
            MainLogic.inst.interfaceMega.SetActive(true);
            MegaCameraController.inst.SetNewPosCamera(MegaCameraController.inst.stateLookVector3AllMega, GlobalParams.eulerAnglesForCameraInAllMega, 
                GlobalParams.fieldOfViewOnAllMega, MegaCameraController.inst.distansAllMega, TypeMoveCamera.slow);
        }

        public ViewStates GetViewStates () {
            return viewStates;
        }

        public TypeCameraOnState GetTypeCameraOnState () {
            return typeCameraOnState;
        }

    }
}