using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapController : MonoBehaviour
{
    public Transform snapPoint;
    public Draggable obj;
    private float range = 10f;
    
    void Start()
    {
        obj.dragEndedCallback = OnDragEnded;
    }
    
    private void OnDragEnded(Draggable drg)
    {
        float closestDistance = -1f;
        Transform closestSnapPoint = null;
        
        float currDistance = Vector2.Distance(drg.transform.localPosition, snapPoint.localPosition);
        closestSnapPoint = snapPoint;
        closestDistance = currDistance;
        
        if (closestSnapPoint != null && closestDistance <= range)
        {
		drg.gameObject.GetComponent<SpriteRenderer>().material.color = new Color(1.0000f, 1.0000f, 1.0000f, 1.0000f);

            drg.transform.localPosition = closestSnapPoint.localPosition;
            Destroy(drg.gameObject.GetComponent<Draggable>());
        }
    }
}
