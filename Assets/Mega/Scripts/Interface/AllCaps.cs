using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Mega.Scripts;
using UnityEngine;

public class AllCaps : MonoBehaviour {
    public static AllCaps inst;

    public List<ShopCap> listShopCaps = new List<ShopCap>();
    public static AllCaps allCaps;

    

    public void Awake() {
        inst = this;
    }

    public void Start () {
        allCaps = this;
        listShopCaps = GameObject.FindObjectsOfType<ShopCap>().ToList();
    }

    public void Refresh () {
        //listShopCaps.ForEach(x => x.gameObject.SetActive(false));
        //TableController.inst.DisAllShops();
    }

    public void ActivateAll () {
        listShopCaps.ForEach(x => x.gameObject.SetActive(true));
        //TableController.inst.DisAllShops();
    }

    public void Update() {
        if (TableController.inst.showCapsTest) {
            TableController.inst.showCapsTest = false;
            ShowAllCaps();
        }
        if(TableController.inst.hideCapsTest) {
            TableController.inst.hideCapsTest = false;
            HideAllCaps();
        }
    }

    public void ShowAllCaps() {
        for (int i = 0; i < listShopCaps.Count; i++) {
            listShopCaps[i].meshRenderer.enabled = true;
        }
        
    }

    public void HideAllCaps() {
        for(int i = 0; i < listShopCaps.Count; i++) {
            listShopCaps[i].meshRenderer.enabled = false;
        }
    }

    public void Activate (string name) {
        bool found = false;
        listShopCaps.ForEach(x => {
            if(x.name == name) {
                found = true;
                x.gameObject.SetActive(true);
                //x.tableShop.EnableShop();
            }
        });
        if(!found) {
            Debug.Log("Cap not found " + name);
        }
    }
}
