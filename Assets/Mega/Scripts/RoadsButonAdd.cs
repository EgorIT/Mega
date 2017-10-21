using UnityEngine;
using UnityEngine.UI;

namespace Assets.Mega.Scripts {
    public class RoadsButonAdd : MonoBehaviour {
        public Toggle parkingBeforTogle;
        public Toggle floorBeforTogle;
        public Button secondLineButton;

        public void Start() {
            parkingBeforTogle.onValueChanged.AddListener(BaseEventData => { MainLogic.inst.SwapParking(parkingBeforTogle.isOn); });
            floorBeforTogle.onValueChanged.AddListener(BaseEventData => { FloorController.inst.SetQuartal(floorBeforTogle.isOn); });
            secondLineButton.onClick.AddListener(() => { MainLogic.inst.GoVideo(); });
        }
    }
}