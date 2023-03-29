using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
	Vector2 targetPos;


    void Start()
    {
        Cursor.visible = false;
    }


    void Update()
    {
        targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	transform.position = targetPos;
    }
}
