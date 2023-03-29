using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    
    private RectTransform rt;
    private CanvasGroup cv;
    
    private void Awake()
    {
        rt = GetComponent<RectTransform>();
        cv = GetComponent<CanvasGroup>();
    }
    
    public void OnBeginDrag(PointerEventData evdt)
    {
        cv.alpha = 0.6f;
        cv.blocksRaycasts = false;
    }
    
    public void OnDrag(PointerEventData evdt)
    {
        rt.anchoredPosition += evdt.delta / canvas.scaleFactor;
        //transform.position = Input.mousePosition;
    }
    
    public void OnEndDrag(PointerEventData evdt)
    {
        cv.alpha = 1f;
        cv.blocksRaycasts = true;
            //transform.localPosition = Vector3.zero;
    }
    
    public void OnPointerDown(PointerEventData evdt)
    {
        
    }
    
    public void OnDrop(PointerEventData evdt)
    {
        throw new System.NotImplementedException();
    }
}
