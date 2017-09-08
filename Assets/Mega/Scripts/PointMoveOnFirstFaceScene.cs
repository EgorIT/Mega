using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Mega.Scripts {
    public class PointMoveOnFirstFaceScene : MonoBehaviour, IPointerDownHandler {

        public PointerEventData pointerEventData;

        public void Start() {
            AddThis();
        }

        public void OnPointerDown (PointerEventData data) {
            pointerEventData = data;
            MoveFirstFaceController.inst.MoveForThisFloorPoint(this);
            //Debug.Log("OnPointerDown " + gameObject.name);

        }

        public void AddThis() {
            MoveFirstFaceController.inst.listPointMoveOnFirstFaceScene.Add(this);
        }

        public void SetThis() {
            
        }
    }
}