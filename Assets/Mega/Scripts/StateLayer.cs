using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Mega.Scripts {
    public class StateLayer : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

        public Image im;

        public void OnPointerDown(PointerEventData eventData) {
            Debug.Log("true");
            KeyController.inst.clickOnMap = true;
            if (im == null) {
                im = GetComponent<Image>();
            }
            im.raycastTarget = false;
        }

        public void OnPointerUp(PointerEventData eventData) {
            Debug.Log("false");
            KeyController.inst.clickOnMap = false;
            im.raycastTarget = true;
        }
    }
}