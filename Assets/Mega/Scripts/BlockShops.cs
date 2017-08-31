using UnityEngine;

namespace Assets.Mega.Scripts {
    public class BlockShops : MonoBehaviour {
        public int numberQuarter;
        public Transform leftTop;
        public Transform RightBotton;

        public Vector3 centerForCamera;
        public float sizeForOrtoCamera;

        public void Start() {
            var v3 = (leftTop.position + RightBotton.position) * 0.5f;
            centerForCamera = new Vector3(v3.x, 0, v3.z);
            sizeForOrtoCamera = (leftTop.position - RightBotton.position).magnitude* GlobalParams.sizeFactorCameraOrtoInshops;
            leftTop.gameObject.SetActive(false);
            RightBotton.gameObject.SetActive(false);
        }
    }
}