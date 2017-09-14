using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Mega.Scripts;
using UnityEngine;

public class AllCaps : MonoBehaviour {

    public List<ShopCap> List = new List<ShopCap>();
    public static AllCaps allCaps;

    void Start ()
    {
        allCaps = this;
        List = GameObject.FindObjectsOfType<ShopCap>().ToList();
    }

    public void Refresh () {
        List.ForEach(x => x.gameObject.SetActive(false));
        TableController.inst.DisAllShops();
    }
    
    public void ActivateAll () {
        List.ForEach(x => x.gameObject.SetActive(true));
        //TableController.inst.DisAllShops();
    }

    public void Activate (string name)
    {
        bool found = false;
        List.ForEach(x => {
            if (x.name == name)
            {
                found = true;
                x.gameObject.SetActive(true);
                //x.tableShop.EnableShop();
            }
        });
        if (!found)
        {
            Debug.Log("Cap not found "+name);
        }
    }
}
