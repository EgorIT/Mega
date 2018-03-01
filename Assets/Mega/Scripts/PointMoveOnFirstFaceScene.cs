using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Mega.Scripts {
    public class PointMoveOnFirstFaceScene : MonoBehaviour, IPointerDownHandler {
        public void OnPointerDown (PointerEventData data) {
            if(!MegaCameraController.inst.isFirstLookScene) {
                StateFirstFaceLook.inst.pointerEventData = data;
            }
        }
    }
}