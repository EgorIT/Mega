using Assets.Mega.Scripts.Interface;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Mega.Scripts {
    public class ButtonAdds : MonoBehaviour {
        public static ButtonAdds inst;

        public Toggle parkingBeforTogle;
        public Toggle floorBeforTogle;
        public Button secondLineButton;
        public Button videoKidsButton;
        public Button commercialButton;
        public Button backFromAllShops;
        public Button goRoads;
        public Button goFloors;
        public Button backFromRoads;
        public Button stockButton;
        public Image fadeImage;

        public Button kids;

        //public Button firstViewButton;
        public Button zoomCameraButton;
        public Button moveCameraButton;
        public Button rotateCameraButton;

        public Image firstViewImage;
        public Image zoomCameraImage;
        public Image moveCameraImage;
        public Image rotateCameraImage;

        public Button btnLeftRotate;
        public Button btnRightRotate;

        public Button btnZoomIn;
        public Button btnZoomOut;

        public Button btnRollIn;
        public Button btnRollOut;

        public void Awake () {
            inst = this;
        }

        public void Start () {
            //parkingBeforTogle.onValueChanged.AddListener(BaseEventData => { MainLogic.inst.SwapParking(parkingBeforTogle.isOn); });
            //floorBeforTogle.onValueChanged.AddListener(BaseEventData => { FloorController.inst.SetFloor(floorBeforTogle.isOn); });
            secondLineButton.onClick.AddListener(() => { MainLogic.inst.GoVideoToBigMega(); });
            commercialButton.onClick.AddListener(() => { MainLogic.inst.GoVideoCommercional(); });
            videoKidsButton.onClick.AddListener(() => { MainLogic.inst.GoVideokids(); });
            backFromAllShops.onClick.AddListener(() => { MainLogic.inst.GoAllMega(false); });

            goRoads.onClick.AddListener(() => {
                MainLogic.inst.GoAllRoads();
                ParkingArrowsController.inst.ShowArrow();
                ZoneFlashController.inst.StartFlashAll();
                KidsArrowController.inst.SetActivZoneClick(false);
            });

            backFromRoads.onClick.AddListener(() => {
                MainLogic.inst.GoAllMega(false);
                MainLogic.inst.SetActiveZoneParking(false);
                ParkingArrowsController.inst.SetActivArrow(false);
                ZoneFlashController.inst.StopFlash();
                KidsArrowController.inst.SetActivZoneClick(true);
            });

            goFloors.onClick.AddListener(() => { MainLogic.inst.GoAllMega(false);  });
           
            kids.onClick.AddListener(() => { MainLogic.inst.GoKids(); });
            stockButton.onClick.AddListener(() => { MainLogic.inst.GoStock(); });

            //firstViewButton.onClick.AddListener(() => { MainLogic.inst.SetFirstLook(); });
            zoomCameraButton.onClick.AddListener(() => { MainLogic.inst.SetZoom(); });
            moveCameraButton.onClick.AddListener(() => { MainLogic.inst.SetMoveAllMega(); });
            rotateCameraButton.onClick.AddListener(() => { MainLogic.inst.SetRotate(); });
        }



        public void SetActiveCurrentUpButton (Image curImage) {
            zoomCameraImage.color = Color.white;
            moveCameraImage.color = Color.white;
            rotateCameraImage.color = Color.white;

            curImage.color = new Color(200, 200, 200, 255) / 255;
        }

        public void ShowUpButton () {
            SetActiveButtons(true);
        }

        public void HideUpButton () {
            SetActiveButtons(false);
        }

        public void SetActiveButtons (bool var) {
            //firstViewButton.gameObject.SetActive(var);
            zoomCameraButton.gameObject.SetActive(false);
            moveCameraButton.gameObject.SetActive(false);
            rotateCameraButton.gameObject.SetActive(false);

            btnLeftRotate.gameObject.SetActive(var);
            btnRightRotate.gameObject.SetActive(var);
            btnZoomIn.gameObject.SetActive(var);
            btnZoomOut.gameObject.SetActive(var);
            if (!var) {
                btnRollIn.gameObject.SetActive(var);
            }
            btnRollOut.gameObject.SetActive(var);
            
            
        }
    }
}