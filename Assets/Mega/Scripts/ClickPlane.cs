using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Mega.Scripts {
    public class ClickPlane : MonoBehaviour, IPointerClickHandler {
        public string namePlane;

        public void OnPointerClick(PointerEventData eventData) {
            Debug.Log(namePlane);
            if (namePlane == "Kids") {
                ButonAdds.inst.kids.OnPointerClick(new PointerEventData(EventSystem.current));
            }
        }
    }
}