using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Mega.Scripts {
    public class StateShops : MonoBehaviour, iViewState {
        public ViewStates viewStates;
        public List<BlockShops> listBlockShopses = new List<BlockShops>();
        private TypeCameraOnState typeCameraOnState = TypeCameraOnState.perspective;
        public BlockShops currentBlockShops;


        public void Start () {
            var allBlockShopses = FindObjectsOfType<BlockShops>();
            for (int i = 0; i < allBlockShopses.Length; i++) {
                listBlockShopses.Add(allBlockShopses[i]);
            }
            MainLogic.inst.listViewStates.Add(this);
        }
        
        public void EndState () {

        }

        public void StartState () {
            for (int i = 0; i < listBlockShopses.Count; i++) {
                if (listBlockShopses[i].numberQuarter == MainLogic.inst.currentNumberBlockShops) {
                    currentBlockShops = listBlockShopses[i];
                    break;
                }
            }
            //MegaCameraController.inst.SetNewPosCamera(currentBlockShops.centerForCamera, GlobalParams.eulerAnglesForCameraInShops, currentBlockShops.sizeForOrtoCamera, typeCameraOnState);
        }

        public ViewStates GetViewStates () {
            return viewStates;
        }

        public TypeCameraOnState GetTypeCameraOnState () {
            return typeCameraOnState;
        }

    }
}