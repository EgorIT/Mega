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

            MainLogic.inst.isRoadLook = false;
            //MegaCameraController.inst.distansAllMega = GP.distansOnAllMega;
            //MegaCameraController.inst.stateLookVector3AllMega = new Vector3(12f, 0, -70f);

            KidsArrowController.inst.HideArrow();
            StockArrowController.inst.HideArrow();
            //ButtonAdds.inst.ShowUpButton();
            MegaCameraController.inst.isFirstLookScene = false;
            //InterfaceController.inst.ShowBasic();
            MegaCameraController.inst.SetNewPosCamera(MegaCameraController.inst.stateLookVector3AllMega, GP.eulerAnglesForCameraInAllMega, 
                GP.fieldOfViewOnAllMega, MegaCameraController.inst.distansAllMega, TypeMoveCamera.slow);
            MegaCameraController.inst.ortoRayCastCamera.gameObject.SetActive(true);


            InterfaceController.inst.ShowBasic();

            //ButtonAdds.inst.btnRollIn.gameObject.SetActive(true);
            //ButtonAdds.inst.btnRollOut.gameObject.SetActive(false);

            MainLogic.inst.HideRoof(1);

            MegaCameraController.inst.isFirstLookScene = false;

        }

        public ViewStates GetViewStates () {
            return viewStates;
        }

        public TypeCameraOnState GetTypeCameraOnState () {
            return typeCameraOnState;
        }

    }
}