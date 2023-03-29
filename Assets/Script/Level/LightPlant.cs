using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPlant : MonoBehaviour
{
	[SerializeField] private GameObject plant;
	[SerializeField] private GameObject mirror;

	void Update()
	{
		if (this.transform.position == mirror.transform.position)
		{
			plant.GetComponent<Animator>().SetBool("Mirror", true);
			plant.GetComponent<BoxCollider2D>().enabled = false;
		}
	}
}
