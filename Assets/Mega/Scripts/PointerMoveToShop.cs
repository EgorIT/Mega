﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Mega.Scripts {
    public class PointerMoveToShop : MonoBehaviour, IPointerDownHandler {
        //public PointerEventData pointerEventData;
        public Transform lookPoint;
        public List<MeshRenderer> meshs;
        //public bool test;
        public ShopCap shopCap;
        public bool dontUseAng;


        public void Start () {
            meshs = GetComponentsInChildren<MeshRenderer>().ToList();
            for(int i = 0; i < meshs.Count; i++) {
                meshs[i].enabled = false;
                //meshs[i].material.color = Color.red;
            }
        }

        public void Setup () {
            shopCap = transform.parent.GetComponent<ShopCap>();
            lookPoint.position = new Vector3(lookPoint.position.x, GP.distansEye, lookPoint.position.z);
            if(!dontUseAng) {
                lookPoint.LookAt(shopCap.tableShop.transform);
                lookPoint.eulerAngles = new Vector3(0, lookPoint.eulerAngles.y, 0);
            }
        }

        public void OnPointerDown (PointerEventData data) {
        }

        //public void GoToThisShop () {
        //    StateFirstFaceLook.inst.hardMovePointerMoveToShop = this;
        //    MegaCameraController.inst.GoToFirstLook(false);
        //}

        public void AddThis () {
            //MoveFirstFaceController.inst.listPointMoveOnFirstFaceScene.Add(this);
        }

        public void SetThis () {

        }


        public void GoToFirstLook () {
            //TableController.inst.DisAllShops();
            //PauseForUI();
            StateFirstFaceLook.inst.isHardMove = true;
            MainLogic.inst.ChangeState(ViewStates.firstFaceLook);
            //isFirstLookScene = true;
            //StartCoroutine(WaitToOff());
            //MainLogic.inst.SwapRoof(true);
        }
    }
}