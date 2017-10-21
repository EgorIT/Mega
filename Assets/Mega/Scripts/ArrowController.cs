using System.Collections.Generic;
using UnityEngine;

namespace Assets.Mega.Scripts {
    public class ArrowController : MonoBehaviour {
        public static ArrowController inst;

        public List<ArrowOnFloor> listArrowOnFloor = new List<ArrowOnFloor>();

        public bool test;

        public void Awake() {
            inst = this;
        }

        public void Update() {
            if (test) {
                test = false;
                AllArrowBack();
            }
        }

        public void AddArrow(ArrowOnFloor arrow) {
            listArrowOnFloor.Add(arrow);
        }

        public void AllArrowBack() {
            for (int i = 0; i < listArrowOnFloor.Count; i++) {
                listArrowOnFloor[i].Fly();
            }
        }
    }
}