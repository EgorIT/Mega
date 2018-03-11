using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragablePanel : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{


    public RectTransform rectTransform;
    public RectTransform upperPanelRectTransform;
    private Vector2 normalPosition = new Vector2(0, 0);
    private Vector2 hiddenPosition = new Vector2(0, -400);
    public Vector2 startedPosition;
    private float startedHeight;
	public bool dragOnSurfaces = true;
    
    private GameObject m_DraggingIcon;
    private RectTransform m_DraggingPlane;

    public void DragLog()
    {
        Debug.Log("777");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startedHeight = eventData.position.y;
        startedPosition = rectTransform.anchoredPosition;//Debug.Log("111");
        //startedPosition
        var canvas = FindInParents<Canvas>(gameObject);
        if (canvas == null)
            return;

        // We have clicked something that can be dragged.
        // What we want to do is create an icon for this.
        /*m_DraggingIcon = new GameObject("icon");

        m_DraggingIcon.transform.SetParent(canvas.transform, false);
        m_DraggingIcon.transform.SetAsLastSibling();

        var image = m_DraggingIcon.AddComponent<Image>();

        image.sprite = GetComponent<Image>().sprite;*/
        //image.SetNativeSize();

        if (dragOnSurfaces)
            m_DraggingPlane = transform as RectTransform;
        else
            m_DraggingPlane = canvas.transform as RectTransform;

        //SetDraggedPosition(eventData);
    }

    public void OnDrag(PointerEventData data)
    {
        Debug.Log(startedHeight+" "+data.position.y+" "+(startedHeight - data.position.y));
        rectTransform.anchoredPosition =
            startedPosition- new Vector2(0, startedHeight - data.position.y);
        upperPanelRectTransform.anchoredPosition = 
            startedPosition- new Vector2(0, startedHeight - data.position.y);

        if (rectTransform.anchoredPosition.y > 10)
        {
            rectTransform.anchoredPosition = normalPosition;
            upperPanelRectTransform.anchoredPosition = normalPosition;
        }
        
        if (rectTransform.anchoredPosition.y < hiddenPosition.y)
        {
            rectTransform.anchoredPosition = hiddenPosition;
            upperPanelRectTransform.anchoredPosition = hiddenPosition;
        }
        Debug.Log(data.position);
        //if (m_DraggingIcon != null)
            //SetDraggedPosition(data);
    }

    private void SetDraggedPosition(PointerEventData data)
    {
        if (dragOnSurfaces && data.pointerEnter != null && data.pointerEnter.transform as RectTransform != null)
            m_DraggingPlane = data.pointerEnter.transform as RectTransform;

        var rt = m_DraggingIcon.GetComponent<RectTransform>();
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_DraggingPlane, data.position, data.pressEventCamera, out globalMousePos))
        {
            rt.position = globalMousePos;
            rt.rotation = m_DraggingPlane.rotation;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        StartCoroutine(Rolling(eventData.position.y > startedHeight));
       /* if (eventData.position.y < startedHeight)
        {
            
        }
        else
        {
            
        }
        //Debug.Log("444");
        if (rectTransform.anchoredPosition.y > -230)
        {
        }
        else
        {
            
        }
        if (m_DraggingIcon != null)
            Destroy(m_DraggingIcon);*/
    }

    private IEnumerator Rolling(bool rollIn)
    {
            float time = 0;
            float time2 = 0.2f;
            //Debug.Log(rt.name);
            Vector3 startPosition = rectTransform.anchoredPosition;

            while(time < time2) {
                if (rollIn)
                {
                    rectTransform.anchoredPosition = Vector3.Lerp(startPosition, normalPosition, time / time2);
                    upperPanelRectTransform.anchoredPosition = Vector3.Lerp(startPosition, normalPosition, time / time2);
                }
                else
                {
                    rectTransform.anchoredPosition = Vector3.Lerp(startPosition, hiddenPosition, time / time2);
                    upperPanelRectTransform.anchoredPosition = Vector3.Lerp(startPosition, hiddenPosition, time / time2);
                }
                time += Time.deltaTime;
                yield return null;
            }
            if(rollIn) {
                rectTransform.anchoredPosition = normalPosition;
                upperPanelRectTransform.anchoredPosition = normalPosition;
                //Countdown();
            } else {
                rectTransform.anchoredPosition = hiddenPosition;
                upperPanelRectTransform.anchoredPosition = hiddenPosition;
            }
            yield return null;
    }




    static public T FindInParents<T>(GameObject go) where T : Component
    {
        if (go == null) return null;
        var comp = go.GetComponent<T>();

        if (comp != null)
            return comp;

        Transform t = go.transform.parent;
        while (t != null && comp == null)
        {
            comp = t.gameObject.GetComponent<T>();
            t = t.parent;
        }
        return comp;
    }
}
