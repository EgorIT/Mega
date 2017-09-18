using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Mega.Scripts {
    public class PointMoveOnFirstFaceScene : MonoBehaviour, IPointerDownHandler {

        public PointerEventData pointerEventData;
        public int countTouch;
        public float timeDeltaTouch;

        public void Start() {
            countTouch = 0;
            AddThis();
        }
       
        public void OnPointerDown (PointerEventData data) {
            pointerEventData = data;
            countTouch++;
            timeDeltaTouch = 0;
            //Debug.Log("OnPointerDown " + gameObject.name);

        }

        public void Update() {
            if (countTouch == 2) {
                StateFirstFaceLook.inst.MoveForThisFloorPoint(this);
                countTouch = 0;
            }
            timeDeltaTouch += Time.deltaTime;
            if (timeDeltaTouch > 0.5f) {
                countTouch = 0;
                timeDeltaTouch = 0.5f;
            }
        }

        public void AddThis() {
            StateFirstFaceLook.inst.listPointMoveOnFirstFaceScene.Add(this);
        }

        public void SetThis() {
            
        }
    }
}