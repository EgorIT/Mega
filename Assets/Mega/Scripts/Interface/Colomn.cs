using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Colomn : MonoBehaviour {
    public Text[] texts;
    public ChangesInfo changesInfo;
    public AllShops allShops;
    public bool all = false;

    public void Click (string name) {
        if(all) {
            allShops.SetShop(name);
        } else {
            changesInfo.SetShop(name);
        }
    }
}
