using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Mega.Scripts;
using UnityEngine;

public class AllCaps : MonoBehaviour {

    public List<ShopCap> allCaps = new List<ShopCap>();

    void Start () {
        allCaps = GameObject.FindObjectsOfType<ShopCap>().ToList();
    }

    public void Refresh () {
        allCaps.ForEach(x => x.gameObject.SetActive(false));
        TableController.inst.DisAllShops();
    }

    public void Activate (string name) {
        allCaps.ForEach(x => {
            if (x.name == name) {
                x.gameObject.SetActive(true);
                x.tableShop.EnableShop();
            }
        });
    }
}
