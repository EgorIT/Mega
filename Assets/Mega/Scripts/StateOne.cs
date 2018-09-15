using Assets.Mega.Scripts.Interface;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Mega.Scripts {

    public class StateOne : MonoBehaviour, IPointerDownHandler, iViewState {
        public static StateOne inst;
        public Image imageOne;
        public ViewStates viewStates;
        private TypeCameraOnState typeCameraOnState = TypeCameraOnState.perspective;

        public bool isShowVideo;
        //public Transform StateLookTransform;

        public void Awake() {
            inst = this;
        }

        public void OnPointerDown (PointerEventData data) {
            //Debug.Log(gameObject.name);
            MainLogic.inst.ChangeState(ViewStates.allMega);
            MainLogic.inst.HideRoof(3);
        }

        public void Start () {
            MainLogic.inst.listViewStates.Add(this);
            //MainLogic.inst.ChangeState(viewStates);
            //EndState();

            imageOne.raycastTarget = false;
            if(isShowVideo) {
                VideoController.inst.FadeOff(0);
                isShowVideo = false;
            } else {
                LogoController.inst.FadeOff(0);
            }

            InterfaceController.inst.ShowBasic();

        }


        public void EndState () {
            Debug.Log("EndState StateOne");
            imageOne.raycastTarget = false;
            if (isShowVideo) {
                VideoController.inst.FadeOff(1);
                isShowVideo = false;
            } else {
                LogoController.inst.FadeOff(1);
            }

            InterfaceController.inst.ShowBasic();
        }

        public void CheckThis () {
            MainLogic.inst.ChangeState(viewStates);
        }

        public void StartState () {
            Debug.Log("StartState StateOne");
            MainLogic.inst.ShowRoof();
            TableController.inst.ShowAllTable();

            KidsArrowController.inst.HideArrow();
            StockArrowController.inst.HideArrow();

            MegaCameraController.inst.isFirstLookScene = false;

            ButtonAdds.inst.HideUpButton();
            MainLogic.inst.isRoadLook = false;

            MegaCameraController.inst.distansAllMega = GP.distansOnAllMega;
            MegaCameraController.inst.stateLookVector3AllMega = new Vector3(12f, 0, -70f);
            if (!isShowVideo && LogoController.inst) {
                LogoController.inst.FadeOn(1);
            }

            if (isShowVideo && VideoController.inst) {
                VideoController.inst.FadeOn(1);
            }
            
            imageOne.raycastTarget = true;
            MegaCameraController.inst.SetNewPosCamera(new Vector3(12f, 0, -70f), GP.eulerAnglesForCameraInAllMega, 
                GP.fieldOfViewOnStateOne, GP.distansOnStateOne, TypeMoveCamera.slow);

            InterfaceController.inst.HardHideAllTable();
        }

        public ViewStates GetViewStates () {
            return viewStates;
        }

        public TypeCameraOnState GetTypeCameraOnState () {
            return typeCameraOnState;
        }
    }
}
