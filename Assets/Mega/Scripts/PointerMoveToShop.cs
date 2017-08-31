using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Mega.Scripts {
    public class PointerMoveToShop : MonoBehaviour, IPointerDownHandler {

        public PointerEventData pointerEventData;
        public Transform lookPoint;
        public MeshRenderer mesh;

        public void Start () {
            mesh = GetComponent<MeshRenderer>();
            if (mesh) {
                mesh.enabled = false;
            }
            //AddThis();
        }

        public void OnPointerDown (PointerEventData data) {
            pointerEventData = data;
            MoveFirstFaceController.inst.MoveForThisShop(this);
            Debug.Log("OnPointerDown " + gameObject.name);

        }



        public void AddThis () {
            //MoveFirstFaceController.inst.listPointMoveOnFirstFaceScene.Add(this);
        }

        public void SetThis () {

        }
    }
}