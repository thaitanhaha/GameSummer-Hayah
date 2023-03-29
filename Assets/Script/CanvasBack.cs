using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasBack : MonoBehaviour
{
	[SerializeField] private string str_sorting;
	[SerializeField] private int int_sorting;


    void Start()
    {
        Canvas myCanvas = this.GetComponent<Canvas>();
	myCanvas.renderMode = RenderMode.ScreenSpaceCamera;
	myCanvas.worldCamera = Camera.main;

	myCanvas.sortingLayerName = str_sorting;
	myCanvas.sortingOrder = int_sorting;
    }

}
