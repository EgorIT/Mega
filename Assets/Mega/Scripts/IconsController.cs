using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Mega.Scripts {
    public class IconsController : MonoBehaviour {
        public static IconsController inst;
        public List<IconShop> listLittleShops;

        private bool swapRoof = true;

        public void Awake() {
            inst = this;
        }

        public void Start() {
            StartCoroutine(WaitToDis());
        }

        public IEnumerator WaitToDis() {
            yield return new WaitForSeconds(0.2f);
            DisAllShops();
        }


        public void SetAngelsForIcons(float y) {
            for(int i = 0; i < listLittleShops.Count; i++) {
                listLittleShops[i].iconShop.transform.eulerAngles = new Vector3(0, y, 0);
            }
        }

        public void RoofToOff() {
            if (swapRoof) {
                ShowAllShops();
                swapRoof = false;
            }
            
        }

        public void RoofToOn () {
            if(!swapRoof) {
                DisAllShops();
                swapRoof = true;
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