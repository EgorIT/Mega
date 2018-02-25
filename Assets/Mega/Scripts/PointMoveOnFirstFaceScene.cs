using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Mega.Scripts {
    public class PointMoveOnFirstFaceScene : MonoBehaviour, IPointerDownHandler {

        public PointerEventData pointerEventData;
        public int countTouch;
        public float timeDeltaTouch;

        public int k = 0;


        public void Start() {
            countTouch = 0;
            AddThis();
        }

        public void OnPointerDown (PointerEventData data) {
            pointerEventData = data;
            //Debug.Log(data.position + " " + pointerEventData.pointerCurrentRaycast.worldPosition);
            //Debug.Log(data.clickCount);
            countTouch++;
            timeDeltaTouch = 0;


        }

        public void Update() {
            if (countTouch == 2) {
                //StateFirstFaceLook.inst.MoveForThisFloorPoint(this);

                if (MainLogic.inst.GetViewCurrentStates() != ViewStates.firstFaceLook && !MegaCameraController.inst.isFirstLookScene) {
                    Debug.Log("Move Down");
                    GoToNearestShop();
                } else {
                   
                }
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

        public void GoToNearestShop() {
            Ray ray = Camera.main.ScreenPointToRay(pointerEventData.position);
            RaycastHit hit;
            Vector3 pos = Vector3.zero;
            if(Physics.Raycast(ray, out hit)) {
                pos = hit.point;
                //var gov3Click = GameObject.CreatePrimitive(PrimitiveType.Cube);
                //gov3Click.transform.localScale = Vector3.one * 2;
                //gov3Click.transform.position = hit.point;
                //gov3Click.GetComponent<MeshRenderer>().material.color = Color.red;
                //Destroy(gov3Click.GetComponent<Collider>());
                //Debug.Log(hit.transform.gameObject.name);
                // Do something with the object that was hit by the raycast.
            }

            float dis = float.MaxValue;
            int index = 0;
            
            for(int i = 0; i < AllCaps.inst.listShopCaps.Count; i++) {
                if (AllCaps.inst.listShopCaps[i].pointerMoveToShop != null) {
                    var newDis = (AllCaps.inst.listShopCaps[i].pointerMoveToShop.lookPoint.position - pos).sqrMagnitude;
                    //Debug.Log(newDis + " " + i);
                    if(newDis < dis) {
                        dis = newDis;
                        index = i;
                    }
                } else {
                  Debug.Log(" null " + AllCaps.inst.listShopCaps[i].name);  
                }
            }

            //var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //Debug.Log(index);
            //go.transform.position = new Vector3(StateFirstFaceLook.inst.listStateLooksTransform[index].position.x,
            //    GlobalParams.distansEye, StateFirstFaceLook.inst.listStateLooksTransform[index].position.z);

            StateFirstFaceLook.inst.hardMovePointerMoveToShop = AllCaps.inst.listShopCaps[index].pointerMoveToShop;
            MegaCameraController.inst.GoToFirstLook(false);
            //
            //return new Vector3(StateFirstFaceLook.inst.listStateLooksTransform[index].position.x, 
            //    GlobalParams.distansEye, StateFirstFaceLook.inst.listStateLooksTransform[index].position.z);

        }


    }
}