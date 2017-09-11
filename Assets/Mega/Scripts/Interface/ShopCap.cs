using System.Collections;
using System.Collections.Generic;
using Assets.Mega.Scripts;
using UnityEngine;

public class ShopCap : MonoBehaviour {
    public string name;

    public float deltaX;
    public float deltaZ;

    public bool useFactor;

    public TableShop tableShop;

    public void OnDisable() {
        tableShop.EnableShop();
        //Debug.Log("OnDisable " + gameObject.name);
    }

    public void OnEnable() {
        if (tableShop) {
            tableShop.DisableShop();
        }
        
        //Debug.Log("OnEnable " + gameObject.name);
    }


}
