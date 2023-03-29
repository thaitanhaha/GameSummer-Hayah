using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantWind : MonoBehaviour
{
	[SerializeField] private float speed;

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * speed);
		}
			
	}
}
