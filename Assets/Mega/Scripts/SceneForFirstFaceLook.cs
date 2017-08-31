using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Mega.Scripts {
    [Serializable]
    public class SceneForFirstFaceLook {
        public string nameScene;
        public List<string> allShopsName = new List<string>();
        public List<Transform> currentTransformShop = new List<Transform>();
    }
}