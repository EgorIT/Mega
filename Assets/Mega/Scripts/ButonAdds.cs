using UnityEngine;
using UnityEngine.UI;

namespace Assets.Mega.Scripts {
    public class ButonAdds : MonoBehaviour {
        public static ButonAdds inst;
        public Toggle parkingBeforTogle;
        public Toggle floorBeforTogle;
        public Button secondLineButton;
        public Button backFromAllShops;
        public Button goRoads;
        public Button goFloors;
        public Button backFromRoads;
        public Image fadeImage;

        public void Awake() {
            inst = this;
        }

        public void Start() {
            parkingBeforTogle.onValueChanged.AddListener(BaseEventData => { MainLogic.inst.SwapParking(parkingBeforTogle.isOn); });
            floorBeforTogle.onValueChanged.AddListener(BaseEventData => { FloorController.inst.SetQuartal(floorBeforTogle.isOn); });
            secondLineButton.onClick.AddListener(() => { MainLogic.inst.GoVideo(); });
            backFromAllShops.onClick.AddListener(() => { MainLogic.inst.GoAllMega(); });
            goRoads.onClick.AddListener(() => { MainLogic.inst.GoAllRoads(); });
            goFloors.onClick.AddListener(() => { MainLogic.inst.GoAllMega(); });
            backFromRoads.onClick.AddListener(() => { MainLogic.inst.DisRoof(3); });
        }


    }
}