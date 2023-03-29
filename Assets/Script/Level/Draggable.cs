using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
	public delegate void DragEndedDelegate(Draggable dragObj);

	public DragEndedDelegate dragEndedCallback;

	private bool isDragged = false;
	private Vector3 mouseDragStart;
	private Vector3 spriteDragStart;

	private Vector3 firstPos;
	[SerializeField] Transform endPos;

	void Start()
	{
		this.gameObject.GetComponent<SpriteRenderer>().material.color = new Color(1.0000f, 1.0000f, 1.0000f, 0.4705882f);
		firstPos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
	}



	private void OnMouseDown()
	{
		isDragged = true;
		mouseDragStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		spriteDragStart = transform.localPosition;
	}
	
	
	private void OnMouseDrag()
    {
        if (isDragged)
        {
            transform.localPosition = spriteDragStart + (Camera.main.ScreenToWorldPoint(Input.mousePosition) - mouseDragStart);
        }
    }
    

    private void OnMouseUp()
    {
        isDragged = false;
        dragEndedCallback(this);
	if (transform.localPosition != endPos.position)
	{
		transform.localPosition = firstPos;
	}
    }
}
