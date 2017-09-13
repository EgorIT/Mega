using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Mega.Scripts.Interface {
    public class IconClick : MonoBehaviour, IPointerDownHandler {
        public TableShop tableShop;

        public void OnPointerDown (PointerEventData data) {
            Debug.Log(tableShop.gameObject.name);
        }

    }
}