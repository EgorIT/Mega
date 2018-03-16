using System.Collections;
using System.Collections.Generic;
using Assets.Mega.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopCap : MonoBehaviour, IPointerClickHandler{
    public string name;

    public bool dontUse;

    public float deltaX;
    public float deltaZ;

    public bool useFactor;

    //public bool test;

    public TableShop tableShop;
    public PointerMoveToShop pointerMoveToShop;

    public MeshRenderer meshRenderer;

    public Transform pointTable;

    public void Awake() {
        var allTrans = gameObject.GetComponentsInChildren<Transform>();
        for (int i = 0; i < allTrans.Length; i++) {
            if (allTrans[i].gameObject.name == "PointTable") {
                pointTable = allTrans[i];
            }
        }
    }

    public void MoveCamera() {
        tableShop.ClickTable();
    }

    public void OnDisable() {
        //if (tableShop) {
        //    tableShop.EnableShop();
        //}
        //Debug.Log("OnDisable " + gameObject.name);
    }

    public void OnEnable() {
        if (meshRenderer == null) {
            meshRenderer = GetComponent<MeshRenderer>();
        }
        
        //gameObject.SetActive(false);
        //if (tableShop) {
        //    tableShop.DisableTable();
        //}
        
        //Debug.Log("OnEnable " + gameObject.name);
    }

    public void Update() {
        //if (test) {
        //    test = false;
        //    pointerMoveToShop.test = true;
        //}
    }

    public void Setup() {
        pointerMoveToShop = GetComponentInChildren<PointerMoveToShop>();
        if (pointerMoveToShop) {
            pointerMoveToShop.Setup();    
        }

    }


    public void OnPointerClick(PointerEventData eventData) {
        Debug.Log(name);
    }
}
