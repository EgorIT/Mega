using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Mega.Scripts {

    public class StateOne : MonoBehaviour, IPointerDownHandler, iViewState {
        public Image imageOne;
        public ViewStates viewStates;
        private TypeCameraOnState typeCameraOnState = TypeCameraOnState.perspective;

        public Transform StateLookTransform;

        public void OnPointerDown (PointerEventData data) {
            //Debug.Log(gameObject.name);
            MainLogic.inst.ChangeState(ViewStates.allMega);
        }

        public void Start () {
            MainLogic.inst.listViewStates.Add(this);
            MainLogic.inst.ChangeState(viewStates);
        }


        public void EndState () {
            imageOne.raycastTarget = false;
            Video.inst.FadeOff();
        }

        public void CheckThis () {
            MainLogic.inst.ChangeState(viewStates);
        }

        public void StartState () {
            Video.inst.FadeOn();
            MainLogic.inst.interfaceMega.SetActive(false);
            imageOne.raycastTarget = true;
            MegaCameraController.inst.SetNewPosCamera(StateLookTransform.position, GlobalParams.eulerAnglesForCameraInAllMega, GlobalParams.fieldOfViewOnStateOne, GlobalParams.distansOnStateOne, false);
        }

        public ViewStates GetViewStates () {
            return viewStates;
        }

        public TypeCameraOnState GetTypeCameraOnState () {
            return typeCameraOnState;
        }
    }
}