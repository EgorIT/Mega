﻿using UnityEngine;

namespace Assets.Mega.Scripts {
    public class GlobalParams {
        public static float deltaYpointMidlleInViewBlock = 0.6f;
        public static float deltaPerimetrInViewBlock = 0.15f;
        
        //public static Vector3 eulerAnglesForCameraInAllMega = new Vector3(24, 54, 0);
        public static Vector3 eulerAnglesForCameraInAllMega = new Vector3(45, 45, 0);

        public static Vector3 eulerAnglesForCameraInShops = new Vector3(45, 45, 0);

        public static Vector3 eulerAnglesForCameraInStateOne = new Vector3(3, 0, 0);
        
        public static float timeToBackFromBorder = 0.5f;
        public static float timeToFly = 3.5f;
        public static float timeToShowIcons = 0.4f;

        public static Vector3 scaleIconShop = new Vector3(0.03f, 0.03f, 0.03f);

        public static float cameraXmaxPerspective = -60f;
        public static float cameraXMinPerspective = 60f;

        public static float factorPerspStabilization = 100f;



        public static float fieldOfViewOnStateOne = 1f;
        public static float fieldOfViewOnAllMega = 1f;
        public static float fieldOfViewOnFirstLook = 95f;

        public static float distansOnStateOne = -8200f;
        public static float distansOnAllMega = -8200f;
        public static float distansOnFirstLook = 0f;

        public static float minDistancePesr = -1000f;
        public static float maxDistancePesr = -26000f;
        public static float kidsDistancePesr = -4500f;
        public static float stockDistancePesr = -1800f;

        public static float needTimeToSleep = 220f;//220

        public static float distansEye = 1f;

        public static float arrowDistansSqrt = 100f;
        public static float arrowMinDistansSqrt = 10f;

        public static float timeFloorSwap = 1f;

        public static float speedRotateCamera = 1f;


        public static float timeToDoubleClick = 0.4f;

        public static bool full = true;

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