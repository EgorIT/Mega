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
            imageOne.raycastTarget = false;
            if (isShowVideo) {
                Video.inst.FadeOff();
                isShowVideo = false;
            } else {
                VideoLogo.inst.FadeOff();
            }
            
            
        }

        public void CheckThis () {
            MainLogic.inst.ChangeState(viewStates);
        }

        public void StartState () {
            MainLogic.inst.EnableRoof();
            TableController.inst.ShowAllTable();
            //MainLogic.inst.DisRoof(0f);

            MegaCameraController.inst.distansAllMega = GlobalParams.distansOnAllMega;
            MegaCameraController.inst.stateLookVector3AllMega = new Vector3(12f, 0, -70f);
            if (!isShowVideo && VideoLogo.inst) {
                VideoLogo.inst.FadeOn();
            }

            if (isShowVideo && Video.inst) {
                Video.inst.FadeOn();
            }

            if (MainLogic.inst.interfaceMega) {
                MainLogic.inst.interfaceMega.SetActive(false);
            }
            
            imageOne.raycastTarget = true;
            MegaCameraController.inst.SetNewPosCamera(new Vector3(12f, 0, -70f), GlobalParams.eulerAnglesForCameraInAllMega, 
                GlobalParams.fieldOfViewOnStateOne, GlobalParams.distansOnStateOne, TypeMoveCamera.slow);
        }

        public ViewStates GetViewStates () {
            return viewStates;
        }

        public TypeCameraOnState GetTypeCameraOnState () {
            return typeCameraOnState;
        }
    }
}
