using Assets.Mega.Scripts.Interface;
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
            Debug.Log("EndState StateAllMega");
        }

        public void StartState () {
            Debug.Log("StartState StateAllMega");
            KidsArrowController.inst.HideArrow();
            StockArrowController.inst.HideArrow();
            ButonAdds.inst.ShowUpButton();
            MegaCameraController.inst.isFirstLookScene = false;
            //InterfaceController.inst.ShowBasic();
            MegaCameraController.inst.SetNewPosCamera(MegaCameraController.inst.stateLookVector3AllMega, GlobalParams.eulerAnglesForCameraInAllMega, 
                GlobalParams.fieldOfViewOnAllMega, MegaCameraController.inst.distansAllMega, TypeMoveCamera.slow);
            MegaCameraController.inst.ortoRayCastCamera.gameObject.SetActive(true);
        }

        public ViewStates GetViewStates () {
            return viewStates;
        }

        public TypeCameraOnState GetTypeCameraOnState () {
            return typeCameraOnState;
        }

    }
}