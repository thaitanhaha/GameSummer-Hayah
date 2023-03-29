using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			if (SceneManager.GetActiveScene().buildIndex == 4)
				SceneManager.LoadScene(5);
			SceneManager.LoadScene(1);
			PlayerPrefs.SetInt("currentScene", Mathf.Max(SceneManager.GetActiveScene().buildIndex - 1, PlayerPrefs.GetInt("currentScene")));
		}
	}
}
