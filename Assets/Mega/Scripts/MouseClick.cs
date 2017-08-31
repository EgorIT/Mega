using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MouseClick : MonoBehaviour, IPointerClickHandler {

    public void OnPointerClick (PointerEventData data) {
        Debug.Log("Clicked the Collider!");
    }
}