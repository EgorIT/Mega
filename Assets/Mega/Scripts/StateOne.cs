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
            MainLogic.inst.DisRoof(3);
        }

        public void Start () {
            MainLogic.inst.listViewStates.Add(this);
            MainLogic.inst.ChangeState(viewStates);
        }


        public void EndState () {
            Debug.Log("EndState StateOne");
            imageOne.raycastTarget = false;
            if (isShowVideo) {
                Video.inst.FadeOff();

                isShowVideo = false;
            } else {
                VideoLogo.inst.FadeOff();
            }

            InterfaceController.inst.ShowBasic();
        }

        public void CheckThis () {
            MainLogic.inst.ChangeState(viewStates);
        }

        public void StartState () {
            Debug.Log("StartState StateOne");
            MainLogic.inst.EnableRoof();
            TableController.inst.ShowAllTable();
            //MainLogic.inst.DisRoof(0f);

            KidsArrowController.inst.HideArrow();
            StockArrowController.inst.HideArrow();

            MegaCameraController.inst.isFirstLookScene = false;

            ButonAdds.inst.HideUpButton();
            MainLogic.inst.isRoadLook = false;

            MegaCameraController.inst.distansAllMega = GlobalParams.distansOnAllMega;
            MegaCameraController.inst.stateLookVector3AllMega = new Vector3(12f, 0, -70f);
            if (!isShowVideo && VideoLogo.inst) {
                VideoLogo.inst.FadeOn();
            }

            if (isShowVideo && Video.inst) {
                Video.inst.FadeOn();
            }
            
            imageOne.raycastTarget = true;
            MegaCameraController.inst.SetNewPosCamera(new Vector3(12f, 0, -70f), GlobalParams.eulerAnglesForCameraInAllMega, 
                GlobalParams.fieldOfViewOnStateOne, GlobalParams.distansOnStateOne, TypeMoveCamera.slow);

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
