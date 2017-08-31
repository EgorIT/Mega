using System.Collections.Generic;
using UnityEngine;

namespace Assets.Mega.Scripts {
    public class LittleShopController : MonoBehaviour {

        public static LittleShopController inst;
        public List<LittleShop> listLittleShops;
       

        public void Awake() {
            inst = this;
        }

        public void Start() {
            var allShops = FindObjectsOfType<LittleShop>();
            for (int i = 0; i < allShops.Length; i++) {
                allShops[i].littleShopController = this;
                listLittleShops.Add(allShops[i]);

            }

        }

        public void SetLittleShop(LittleShop littleShop) {
            
        }

        public void SetAngelsForIcons(float y) {
            for(int i = 0; i < listLittleShops.Count; i++) {
                listLittleShops[i].iconShop.transform.eulerAngles = new Vector3(0, y, 0);
            }
        }

        public void ShowAllShops () {
            for(int i = 0; i < listLittleShops.Count; i++) {
                listLittleShops[i].EnableShop();
            }
        }

        public void DisAllShops() {
            for(int i = 0; i < listLittleShops.Count; i++) {
                listLittleShops[i].DisableShop();
            }
        }

    }
}