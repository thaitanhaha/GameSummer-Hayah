using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item1_HP : MonoBehaviour
{
    void Start()
    {
	this.gameObject.SetActive(PlayerPrefs.GetInt("HPPotion") == 0);        
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			PlayerPrefs.SetInt("HPPotion", 1);
			Destroy(this.gameObject);
		}
	}

}
