using UnityEngine;

namespace Assets.Mega.Scripts {
    public class GlobalParams {
        public static float deltaYpointMidlleInViewBlock = 0.6f;
        public static float deltaPerimetrInViewBlock = 0.15f;


        public static float sizeFactorCameraOrtoInshops = 0.3f;
        //public static Vector3 startLocalEulerAnglesForCamera = new Vector3(24, 54, 0);
        //public static float distancePointForCameraMidlleFragment = -20f;

        public static float sizeOrtocameraMidlleFragment = 5;

        public static float sizeOrtocameraAllMega = 100;
        public static Vector3 eulerAnglesForCameraInAllMega = new Vector3(24, 54, 0);

        public static Vector3 eulerAnglesForCameraInShops = new Vector3(24, 54, 0);

        public static float sizeOrtocameraStateOne = 0.09f;
        public static Vector3 eulerAnglesForCameraInStateOne = new Vector3(3, 0, 0);

        public static float finalDistansOrto = -500;

        public static float timeToBackFromBorder = 0.5f;
        public static float timeToFly = 2f;
        public static float timeToShowIcons = 0.4f;

        public static Vector3 scaleIconShop = new Vector3(0.4f, 0.4f, 0.4f);

        public static float cameraXmaxPerspective = -60f;
        public static float cameraXMinPerspective = 60f;


        public static float ortoMinSize = 5f;
        public static float ortoMaxSize = 150f;
    }
}