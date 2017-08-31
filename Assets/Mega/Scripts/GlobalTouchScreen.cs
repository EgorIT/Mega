using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Mega.Scripts {
    public class GlobalTouchScreen : MonoBehaviour, IPointerDownHandler {
        public static GlobalTouchScreen inst;

        public void Awake() {
            inst = this;
        }

        public void OnPointerDown (PointerEventData data) {
            Debug.Log(gameObject.name);
        }
        void Update () {
           
        }
    }
}