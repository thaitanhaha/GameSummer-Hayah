using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantPoison : MonoBehaviour
{
	//[SerializeField] ParticleSystem beingTouch;

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			other.gameObject.GetComponent<PlayerHealth>().BeingPoison();
			//beingTouch.Play();
			Destroy(this.gameObject);
		}
	}

}
