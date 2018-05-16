using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Mega.Scripts {
    public class ClickPlane : MonoBehaviour, IPointerClickHandler {
        public string namePlane;

        public void OnPointerClick (PointerEventData eventData) {
            //Debug.Log(namePlane);
            switch(namePlane) {
                case "Kids":
                    ButonAdds.inst.kids.OnPointerClick(new PointerEventData(EventSystem.current));
                    break;
                case "20":
                    MainLogic.inst.actionSelectZone(20);
                    ZoneFlashController.inst.StartFlashmeshRenderer(0);
                    break;
                case "21":
                    MainLogic.inst.actionSelectZone(21);
                    ZoneFlashController.inst.StartFlashmeshRenderer(1);
                    break;
                case "22":
                    MainLogic.inst.actionSelectZone(22);
                    ZoneFlashController.inst.StartFlashmeshRenderer(2);
                    break;
                case "23":
                    MainLogic.inst.actionSelectZone(23);
                    ZoneFlashController.inst.StartFlashmeshRenderer(3);
                    break;
            }


            


        }
    }
}