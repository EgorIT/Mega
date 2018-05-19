using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Mega.Scripts {
    public class TableController : MonoBehaviour {
        public static TableController inst;
        public List<TableShop> listLittleShops;

        //private bool swapRoof = true;
        public bool createTables;
        
        public bool showTable;
        public bool hideTable;

        public bool showRoof;
        public bool hideRoof;

        public bool showCapsTest;
        public bool hideCapsTest;

        public TableShop prefabTableShop;

        public Transform underlineTran;

        public void Awake () {
            inst = this;
        }

        public void Start () {
            if (createTables)
            {
                var allTable = new GameObject {name = "allTable"};
                allTable.transform.position = Vector3.zero;
                allTable.transform.eulerAngles = Vector3.zero;
                allTable.transform.localScale = Vector3.one;

                var allShopCap = FindObjectsOfType<ShopCap>();
                for (int i = 0; i < allShopCap.Length; i++)
                {
                    if (allShopCap[i].dontUse)
                    {
                        continue;
                    }
                    var tableShop = Instantiate(prefabTableShop);
                    tableShop.gameObject.SetActive(true);
                    tableShop.transform.parent = allShopCap[i].transform;
                    var mesh = allShopCap[i].GetComponent<MeshFilter>();
                    var midl = Vector3.zero;
                    foreach (Vector3 t in mesh.mesh.vertices)
                    {
                        midl += t;
                    }
                    midl = midl * (1f / mesh.mesh.vertices.Length);
                    tableShop.transform.localPosition = new Vector3(midl.x, 0, midl.z);

                    if (allShopCap[i].pointTable)
                    {
                        tableShop.transform.position = new Vector3(allShopCap[i].pointTable.position.x,
                            tableShop.transform.position.y, allShopCap[i].pointTable.position.z);
                    }
                    tableShop.transform.parent = allTable.transform;
                    tableShop.transform.localPosition = new Vector3(tableShop.transform.localPosition.x, 5f,
                        tableShop.transform.localPosition.z);
                    tableShop.transform.localScale = Vector3.zero;
                    tableShop.SetName(allShopCap[i].name);
                    tableShop.shopCap = allShopCap[i];
                    allShopCap[i].tableShop = tableShop;
                    allShopCap[i].Setup();
                }
                SetAngelsForIcons(MegaCameraController.inst.angelYCamera.localEulerAngles.y);
            }
        }

        public void SetAngelsForIcons (float y) {
            foreach (var t in listLittleShops) {
                t.iconShop.transform.eulerAngles = new Vector3(45, y, 0);
            }
        }

        public void Update() {
            if (showTable) {
                showTable = false;
                ShowAllTable();
            }
            if(hideTable) {
                hideTable = false;
                HideAllTable();
            }
        }

        public void ShowAllTable () {
            for(int i = 0; i < listLittleShops.Count; i++) {
                listLittleShops[i].EnableTable();
            }
        }

        public void HideAllTable () {
            for(int i = 0; i < listLittleShops.Count; i++) {
                listLittleShops[i].DisableTable();
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
    }
}
