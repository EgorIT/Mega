using UnityEngine;

namespace Assets.Mega.Scripts {
    public class UnderlineLook : MonoBehaviour {

        public Transform table;

        public void Update() {
            if (MegaCameraController.inst.isFirstLookScene) {
                table.LookAt(MegaCameraController.inst.perspectiveCamera.transform);
                table.Rotate(Vector3.up*180);
            } else {
                table.localEulerAngles = Vector3.zero;
            }
        }

    }
}