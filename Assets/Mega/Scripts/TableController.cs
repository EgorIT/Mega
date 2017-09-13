using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Mega.Scripts {
    public class TableController : MonoBehaviour {
        public static TableController inst;
        public List<TableShop> listLittleShops;

        //private bool swapRoof = true;

        public TableShop prefabTableShop;

        public void Awake () {
            inst = this;
        }

        public void Start () {
            var allTable = new GameObject();
            allTable.name = "allTable";
            allTable.transform.position = Vector3.zero;
            allTable.transform.eulerAngles = Vector3.zero;
            allTable.transform.localScale = Vector3.one;

            var allShopCap = FindObjectsOfType<ShopCap>();
            for(int i = 0; i < allShopCap.Length; i++) {
                var tableShop = Instantiate(prefabTableShop);
                tableShop.gameObject.SetActive(true);
               // tableShop.transform.parent = allTable.transform;
                tableShop.transform.parent = allShopCap[i].transform;


                var mesh = allShopCap[i].GetComponent<MeshFilter>();
                Vector3 midl = Vector3.zero;
                for(int j = 0; j < mesh.mesh.vertices.Length; j++) {
                    midl += mesh.mesh.vertices[j];
                }
                midl = midl * (1f / mesh.mesh.vertices.Length);
                tableShop.transform.localPosition = new Vector3(midl.x, 0, midl.z);

                /*if(allShopCap[i].useFactor) {
                    tableShop.transform.position =
                        allShopCap[i].transform.position - new Vector3(midl.x, 0, midl.z) + Vector3.up * 2;
                } else {
                    tableShop.transform.position =
                        allShopCap[i].transform.position + new Vector3(midl.x, 0, midl.z) + Vector3.up * 2;
                }*/


                /* } else {
                     tableShop.transform.position = allShopCap[i].transform.position + Vector3.up * 2;
                     tableShop.startPos = tableShop.transform.position;
                 }*/
                tableShop.transform.parent = allTable.transform;
                tableShop.transform.localPosition = new Vector3(tableShop.transform.localPosition.x, 5.5f, tableShop.transform.localPosition.z);
                //tableShop.transform.localScale = Vector3.zero;
                tableShop.transform.localScale = GlobalParams.scaleIconShop;
                tableShop.SetName(allShopCap[i].name);
                tableShop.shopCap = allShopCap[i];
                allShopCap[i].tableShop = tableShop;
                allShopCap[i].Setup();
            }

            StartCoroutine(WaitToDis());
        }

        public IEnumerator WaitToDis () {
            yield return new WaitForSeconds(0.2f);
            DisAllShops();
        }


        public void SetAngelsForIcons (float y) {
            for(int i = 0; i < listLittleShops.Count; i++) {
                listLittleShops[i].iconShop.transform.eulerAngles = new Vector3(0, y, 0);
            }
        }

        /*public void RoofToOff () {
            if(swapRoof) {
                ShowAllShops();
                swapRoof = false;
            }

        }

        public void RoofToOn () {
            if(!swapRoof) {
                
                swapRoofswapRoof = true;
            }

        }*/

        public void ShowAllShops () {
            for(int i = 0; i < listLittleShops.Count; i++) {
                listLittleShops[i].EnableShop();
            }
        }

        public void DisAllShops () {
            for(int i = 0; i < listLittleShops.Count; i++) {
                listLittleShops[i].DisableShop();
            }
        }

    }
}
