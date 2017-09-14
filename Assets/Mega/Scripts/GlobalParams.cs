using UnityEngine;

namespace Assets.Mega.Scripts {
    public class GlobalParams {
        public static float deltaYpointMidlleInViewBlock = 0.6f;
        public static float deltaPerimetrInViewBlock = 0.15f;
        
        //public static Vector3 eulerAnglesForCameraInAllMega = new Vector3(24, 54, 0);
        public static Vector3 eulerAnglesForCameraInAllMega = new Vector3(45, 45, 0);

        public static Vector3 eulerAnglesForCameraInShops = new Vector3(24, 54, 0);
        
        public static Vector3 eulerAnglesForCameraInStateOne = new Vector3(3, 0, 0);
        
        public static float timeToBackFromBorder = 0.5f;
        public static float timeToFly = 2f;
        public static float timeToShowIcons = 0.4f;

        public static Vector3 scaleIconShop = new Vector3(0.1f, 0.1f, 0.1f);

        public static float cameraXmaxPerspective = -60f;
        public static float cameraXMinPerspective = 60f;

        public static float factorPerspStabilization = 100f;

        public static float fieldOfViewOnStateOne = 1f;
        public static float fieldOfViewOnAllMega = 1f;
        public static float fieldOfViewOnFirstLook = 60f;

        public static float distansOnStateOne = -8200;
        public static float distansOnAllMega = -8200;
        public static float distansOnFirstLook = 0f;

        public static float minDistancePesr = -1000f;
        public static float maxDistancePesr = -10000;

        public static float needTimeToSleep = 120f;
        //public static float ortoMinSize = 5f;
        //public static float ortoMaxSize = 150f;

        //public static float sizeOrtocameraStateOne = 0.09f;
        //public static float finalDistansOrto = -500;
        //public static float sizeFactorCameraOrtoInshops = 0.3f;
        //public static Vector3 startLocalEulerAnglesForCamera = new Vector3(24, 54, 0);
        //public static float distancePointForCameraMidlleFragment = -20f;
        //public static float sizeOrtocameraMidlleFragment = 5;
        //public static float sizeOrtocameraAllMega = 100;
    }
}