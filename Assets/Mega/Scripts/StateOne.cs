using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Mega.Scripts {
    
    public class StateOne : MonoBehaviour, IPointerDownHandler, iViewState {
        public Image imageOne;
        public ViewStates viewStates;
        public TypeCameraOnState typeCameraOnState;

        public Transform StateLookTransform;

        public void OnPointerDown (PointerEventData data) {
            //Debug.Log(gameObject.name);
            MainLogic.inst.ChangeState(ViewStates.allMega);
        }

        public void Start() {
            MainLogic.inst.listViewStates.Add(this);
            MainLogic.inst.ChangeState(viewStates);
        }


        public void EndState() {
            imageOne.raycastTarget = false;

        }

        public void CheckThis () {
            MainLogic.inst.ChangeState(viewStates);
        }

        public void StartState () {
            MainLogic.inst.interfaceMega.SetActive(false);
            imageOne.raycastTarget = true;
            MegaCameraController.inst.SetNewPosCamera(StateLookTransform.position, GlobalParams.eulerAnglesForCameraInStateOne, GlobalParams.sizeOrtocameraStateOne, typeCameraOnState);
        }

        public ViewStates GetViewStates() {
            return viewStates;
        }

        public TypeCameraOnState GetTypeCameraOnState () {
            return typeCameraOnState;
        }
    }
}