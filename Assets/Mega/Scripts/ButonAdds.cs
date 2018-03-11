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

        public Button firstViewButton;
        public Button zoomCameraButton;
        public Button moveCameraButton;
        public Button rotateCameraButton;

        public Image firstViewImage;
        public Image zoomCameraImage;
        public Image moveCameraImage;
        public Image rotateCameraImage;

        public void Awake() {
            inst = this;
        }

        public void Start() {
            parkingBeforTogle.onValueChanged.AddListener(BaseEventData => { MainLogic.inst.SwapParking(parkingBeforTogle.isOn); });
            //floorBeforTogle.onValueChanged.AddListener(BaseEventData => { FloorController.inst.SetFloor(floorBeforTogle.isOn); });
            secondLineButton.onClick.AddListener(() => { MainLogic.inst.GoVideo(); });
            backFromAllShops.onClick.AddListener(() => { MainLogic.inst.GoAllMega(); });
            goRoads.onClick.AddListener(() => { MainLogic.inst.GoAllRoads(); });
            goFloors.onClick.AddListener(() => { MainLogic.inst.GoAllMega(); });
            backFromRoads.onClick.AddListener(() => { MainLogic.inst.GoAllMega(); MainLogic.inst.DisRoof(3); });



            firstViewButton.onClick.AddListener(() => { Debug.Log("firstViewButton"); });
            zoomCameraButton.onClick.AddListener(() => { Debug.Log("zoomCameraButton"); });
            moveCameraButton.onClick.AddListener(() => { Debug.Log("moveCameraButton"); });
            rotateCameraButton.onClick.AddListener(() => { Debug.Log("rotateCameraButton"); });
        }


    }
}