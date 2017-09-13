using System.Collections;
using System.Collections.Generic;
using Assets.Mega.Scripts;
using UnityEngine;

public class ShopCap : MonoBehaviour {
    public string name;

    public float deltaX;
    public float deltaZ;

    public bool useFactor;

    public bool test;

    public TableShop tableShop;
    public PointerMoveToShop pointerMoveToShop;

    public void MoveCamera() {
        tableShop.ClickTable();
    }

    public void OnDisable() {
        if (tableShop) {
            tableShop.EnableShop();
        }
        //Debug.Log("OnDisable " + gameObject.name);
    }

    public void OnEnable() {
        if (tableShop) {
            tableShop.DisableShop();
        }
        
        //Debug.Log("OnEnable " + gameObject.name);
    }

    public void Update() {
        if (test) {
            test = false;
            pointerMoveToShop.test = true;
        }
    }

    public void Setup() {
        pointerMoveToShop = GetComponentInChildren<PointerMoveToShop>();
        if (pointerMoveToShop) {
            pointerMoveToShop.Setup();    
        }

    }


}
