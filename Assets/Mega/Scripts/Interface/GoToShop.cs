using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GoToShop : MonoBehaviour {

    public AllCaps allCaps;
    public Image image;

    public void PushButton () {
        for (int i = 0; i < allCaps.allCaps.Count; i++) {
            if (allCaps.allCaps[i].name == image.sprite.name) {
                allCaps.allCaps[i].MoveCamera();
            }
            
        }
        //ShopCap shopCap = allCaps.allCaps.First(x => x.GetComponent<ShopCap>().name == image.name);
        //if(shopCap != null) {
        //    shopCap.MoveCamera();
        //}
    }

    void Start () {
        allCaps = GameObject.FindObjectOfType<AllCaps>();
        image = gameObject.GetComponent<Image>();
    }

}
