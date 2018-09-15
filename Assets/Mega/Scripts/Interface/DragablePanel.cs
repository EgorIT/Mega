using System.Collections;
using System.Collections.Generic;
using Assets.Mega.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragablePanel : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public RectTransform rectTransform;
    
    private Vector2 normalPosition = new Vector2(0, 0);
    private Vector2 hiddenPosition = new Vector2(0, -400);
    public Vector2 startedPosition;
    private float startedHeight;
    public bool dragOnSurfaces = true;

    private GameObject m_DraggingIcon;
    //private RectTransform m_DraggingPlane;

    public void DragLog () {
        //Debug.Log("777");
    }

    public void OnBeginDrag (PointerEventData eventData) {
        return;
        //Debug.Log("OnBeginDrag");
        InterfaceController.inst.isDrag = true;
        startedHeight = eventData.position.y;
        startedPosition = rectTransform.anchoredPosition;//Debug.Log("111");
        //startedPosition
        //var canvas = FindInParents<Canvas>(gameObject);
        //if (canvas == null) {
        //    return;
        //}

        //if (dragOnSurfaces) {
        //    m_DraggingPlane = transform as RectTransform;
        //} else {
        //    m_DraggingPlane = canvas.transform as RectTransform;
        //}
    }

    public void OnDrag (PointerEventData data) {
        return;
        rectTransform.anchoredPosition = startedPosition - new Vector2(0, startedHeight - data.position.y);
        InterfaceController.inst.upperPanelRectTransform.anchoredPosition = startedPosition - new Vector2(0, startedHeight - data.position.y);

        if(rectTransform.anchoredPosition.y > 10) {
            rectTransform.anchoredPosition = normalPosition;
            InterfaceController.inst.upperPanelRectTransform.anchoredPosition = normalPosition;
        }

        if(rectTransform.anchoredPosition.y < hiddenPosition.y) {
            rectTransform.anchoredPosition = hiddenPosition;
            InterfaceController.inst.upperPanelRectTransform.anchoredPosition = hiddenPosition;
        }
    }
    
    public void OnEndDrag (PointerEventData eventData) {
        return;
        //Debug.Log("OnEndDrag");
        InterfaceController.inst.isDrag = false;
        StartCoroutine(Rolling(eventData.position.y > startedHeight));
    }

    [EasyButtons.Button]
    public void RollIn() {
        StartCoroutine(Rolling(true));
    }

    [EasyButtons.Button]
    public void RollOut () {
        StartCoroutine(Rolling(false));
    }

    private IEnumerator Rolling (bool rollIn) {
        float time = 0;
        float time2 = 0.2f;
        //Debug.Log(rt.name);
        Vector3 startPosition = rectTransform.anchoredPosition;

        while(time < time2) {
            if(rollIn) {
                rectTransform.anchoredPosition = Vector3.Lerp(startPosition, normalPosition, time / time2);
                InterfaceController.inst.upperPanelRectTransform.anchoredPosition = Vector3.Lerp(startPosition, normalPosition, time / time2);
            } else {
                rectTransform.anchoredPosition = Vector3.Lerp(startPosition, hiddenPosition, time / time2);
                InterfaceController.inst.upperPanelRectTransform.anchoredPosition = Vector3.Lerp(startPosition, hiddenPosition, time / time2);
            }
            time += Time.deltaTime;
            yield return null;
        }
        if(rollIn) {
            rectTransform.anchoredPosition = normalPosition;
            InterfaceController.inst.upperPanelRectTransform.anchoredPosition = normalPosition;
            GP.fullUI = true;
            //Debug.Log("true");
            //Countdown();
        } else {
            rectTransform.anchoredPosition = hiddenPosition;
            InterfaceController.inst.upperPanelRectTransform.anchoredPosition = hiddenPosition;
            GP.fullUI = false;
            //Debug.Log("false");
        }
        yield return null;
    }

    static public T FindInParents<T> (GameObject go) where T : Component {
        if(go == null)
            return null;
        var comp = go.GetComponent<T>();

        if(comp != null)
            return comp;

        Transform t = go.transform.parent;
        while(t != null && comp == null) {
            comp = t.gameObject.GetComponent<T>();
            t = t.parent;
        }
        return comp;
    }

    //private void SetDraggedPosition (PointerEventData data) {
    //    if(dragOnSurfaces && data.pointerEnter != null && data.pointerEnter.transform as RectTransform != null)
    //        m_DraggingPlane = data.pointerEnter.transform as RectTransform;
    //
    //    var rt = m_DraggingIcon.GetComponent<RectTransform>();
    //    Vector3 globalMousePos;
    //    if(RectTransformUtility.ScreenPointToWorldPointInRectangle(m_DraggingPlane, data.position, data.pressEventCamera, out globalMousePos)) {
    //        rt.position = globalMousePos;
    //        rt.rotation = m_DraggingPlane.rotation;
    //    }
    //}
}
