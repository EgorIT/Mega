using System.Collections.Generic;
using UnityEngine;

namespace Assets.Mega.Scripts {
    public class FloorController : MonoBehaviour {
        public static FloorController inst;
        public List<MeshRenderer> allFloor;
        public List<int> allFloorQuartal;
        public Material newFloorMaterial;
        public Material oldFloorMaterial;

        public bool newFloor;
        public bool oldFloor;

        public void Awake() {
            inst = this;
        }

        public void SetNew() {
            for (int i = 0; i < allFloor.Count; i++) {
                allFloor[i].material = newFloorMaterial;
            }
        }

        public void SetOld() {
            for(int i = 0; i < allFloor.Count; i++) {
                allFloor[i].material = oldFloorMaterial;
            }
        }

        public void Start() {

            var allMesh = FindObjectsOfType<MeshRenderer>();

            for (int i = 0; i < allMesh.Length; i++) {
                if (allMesh[i].materials[0].name == "Floor (Instance)") {
                    allFloor.Add(allMesh[i]);
                    //allMesh[i].transform.parent = transform;
                }
            }
        }

        public void SetQuartal(int numberQuart) {
            for (int i = 0; i < allFloorQuartal.Count; i++) {
                if (allFloorQuartal[i] <= numberQuart) {
                    allFloor[i].material = newFloorMaterial;
                } else {
                    allFloor[i].material = oldFloorMaterial;
                }
            }
        }

        public void Update() {
            if (newFloor) {
                newFloor = false;
                SetNew();
            }

            if(oldFloor) {
                oldFloor = false;
                SetOld();
            }
        }


    }
}
