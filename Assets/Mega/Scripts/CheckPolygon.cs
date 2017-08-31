using Assets.Mega.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class CheckPolygon : MonoBehaviour, IPointerDownHandler {
    public bool isPolygonCheck;
    public ViewBlock viewBlock;

    public void Awake () {
        isPolygonCheck = false;
        viewBlock = transform.parent.parent.gameObject.GetComponent<ViewBlock>();
    }
    
    public void OnPointerDown (PointerEventData data) {
        //Debug.Log(gameObject.name);
        if (viewBlock) {
            //Debug.Log(data.pointerCurrentRaycast.worldPosition);
            viewBlock.SetClick(data.pointerCurrentRaycast.worldPosition);
        } else {
          Debug.Log("viewBlock EMPTY" + gameObject.name);  
        }
        
    }

    /*public void OnTriggerEnter (Collider other) {
        Debug.Log("OnTriggerEnter! " + other.gameObject.name);
    }*/

    //public void OnPointerDown (PointerEventData eventData) {
    //    Debug.Log(gameObject.name + " C");
    //    isPolygonCheck = true;
    //}
    //
    //public void OnPointerUp (PointerEventData eventData) {
    //    isPolygonCheck = false;
    //}

    //void Update () {
    ////RaycastHit2D hit = Physics2D.Raycast(MegaCameraController.inst.mainCamera.transform.position, Input.mousePosition);
    ////if(hit != null && hit.collider != null) {
    ////    Debug.Log(hit.collider.gameObject.name);
    ////}
    //if(Input.GetMouseButtonDown(0)) {
    //    Debug.Log(Input.mousePosition);
    //}
    //
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
    //    if(hit.collider != null && Input.GetMouseButtonDown(0)) {
    //        Debug.Log(hit.collider.gameObject.name);
    //    }
    //}



    /*void Update () {
        if(Input.GetMouseButtonDown(0)) {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);
            if(hit != null) {
                Debug.Log("Hit Collider: " + hit.transform.name);
            } else {
                Debug.Log("No colliders hit from mouse click");
            }
        }
    }*/
}
