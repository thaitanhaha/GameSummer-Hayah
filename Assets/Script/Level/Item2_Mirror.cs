using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item2_Mirror : MonoBehaviour
{
    void Start()
    {
	this.gameObject.SetActive(PlayerPrefs.GetInt("Mirror") == 0);        
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			PlayerPrefs.SetInt("Mirror", 1);
			Destroy(this.gameObject);
		}
	}

}
