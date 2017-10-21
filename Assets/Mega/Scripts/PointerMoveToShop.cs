using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Mega.Scripts {
    public class PointerMoveToShop : MonoBehaviour, IPointerDownHandler {

        public PointerEventData pointerEventData;
        public Transform lookPoint;
        public List<MeshRenderer> meshs;
        public bool test;
        public ShopCap shopCap;
        public bool dontUseAng;


        public void Start () {

            
            meshs = GetComponentsInChildren<MeshRenderer>().ToList();

            for (int i = 0; i < meshs.Count; i++) {
                meshs[i].enabled = false;
                //meshs[i].material.color = Color.red;
            }
        }

        public void Setup() {
            shopCap = transform.parent.GetComponent<ShopCap>();
            lookPoint.position = new Vector3(lookPoint.position.x, GlobalParams.distansEye, lookPoint.position.z);
            if(!dontUseAng) {
                lookPoint.LookAt(shopCap.tableShop.transform);
                lookPoint.eulerAngles = new Vector3(0, lookPoint.eulerAngles.y, 0);
            }
            
        }

        public void OnPointerDown (PointerEventData data) {
            pointerEventData = data;
            if (MegaCameraController.inst.isFirstLookScene) {
                StateFirstFaceLook.inst.MoveForThisShop(this);
            }
        }

        public void GoToThisShop() {
            if(!MegaCameraController.inst.isFirstLookScene) {
                StateFirstFaceLook.inst.hardMovePointerMoveToShop = this;
                MegaCameraController.inst.GoToFirstLook(false);
                //Debug.Log("OnPointerDown " + gameObject.name);
            }
        }

        /*public void GoToFirstLook () {
            TableController.inst.DisAllShops();
            PauseForUI();
            StateFirstFaceLook.inst.isHardMove = true;
            MainLogic.inst.ChangeState(ViewStates.firstFaceLook);
            isFirstLookScene = true;
            StartCoroutine(WaitToOff());
            //MainLogic.inst.SwapRoof(true);
        }*/


        public void Update() {
            if (test) {
                test = false;
                if(MegaCameraController.inst.isFirstLookScene) {
                    StateFirstFaceLook.inst.MoveForThisShop(this);
                } else {
                    StateFirstFaceLook.inst.hardMovePointerMoveToShop = this;
                    MegaCameraController.inst.GoToFirstLook(false);
                    Debug.Log("OnPointerDown " + gameObject.name);
                }
            }
        }

        public void AddThis () {
            //MoveFirstFaceController.inst.listPointMoveOnFirstFaceScene.Add(this);
        }

        public void SetThis () {

        }
    }
}