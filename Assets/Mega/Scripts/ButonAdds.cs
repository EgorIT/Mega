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

        public Button kids;

        public Button firstViewButton;
        public Button zoomCameraButton;
        public Button moveCameraButton;
        public Button rotateCameraButton;

        public Image firstViewImage;
        public Image zoomCameraImage;
        public Image moveCameraImage;
        public Image rotateCameraImage;

        public void Awake () {
            inst = this;
        }

        public void Start () {
            parkingBeforTogle.onValueChanged.AddListener(BaseEventData => { MainLogic.inst.SwapParking(parkingBeforTogle.isOn); });
            //floorBeforTogle.onValueChanged.AddListener(BaseEventData => { FloorController.inst.SetFloor(floorBeforTogle.isOn); });
            secondLineButton.onClick.AddListener(() => { MainLogic.inst.GoVideo(); });
            backFromAllShops.onClick.AddListener(() => { MainLogic.inst.GoAllMega(); });
            goRoads.onClick.AddListener(() => { MainLogic.inst.GoAllRoads(); });
            goFloors.onClick.AddListener(() => { MainLogic.inst.GoAllMega(); });
            backFromRoads.onClick.AddListener(() => { MainLogic.inst.GoAllMega(); MainLogic.inst.DisRoof(3); });
            kids.onClick.AddListener(() => { MainLogic.inst.GoKids(); });


            firstViewButton.onClick.AddListener(() => { MainLogic.inst.SetFirstLook(); });
            zoomCameraButton.onClick.AddListener(() => { MainLogic.inst.SetZoom(); });
            moveCameraButton.onClick.AddListener(() => { MainLogic.inst.SetMoveAllMega(); });
            rotateCameraButton.onClick.AddListener(() => { MainLogic.inst.SetRotate(); });
        }



        public void SetActivCurrentUpButton (Image curImage) {
            zoomCameraImage.color = Color.white;
            moveCameraImage.color = Color.white;
            rotateCameraImage.color = Color.white;

            curImage.color = new Color(200, 200, 200, 255) / 255;
        }

        public void ShowUpButton () {
            SetActivButtons(true);
        }

        public void HideUpButton () {
            SetActivButtons(false);
        }

        public void SetActivButtons (bool var) {
            firstViewButton.gameObject.SetActive(var);
            zoomCameraButton.gameObject.SetActive(var);
            moveCameraButton.gameObject.SetActive(var);
            rotateCameraButton.gameObject.SetActive(var);
        }
    }
}