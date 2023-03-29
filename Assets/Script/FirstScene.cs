using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FirstScene : MonoBehaviour
{
	[SerializeField] Button con_bt;

	void Start()
	{
		con_bt.interactable = (PlayerPrefs.GetInt("newgame") == 1);
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	public void NewGame()
	{
		PlayerPrefs.DeleteAll();
		PlayerPrefs.SetInt("newgame", 1);
		SceneManager.LoadScene(1);
	}

	public void ContinueGame()
	{
		SceneManager.LoadScene(1);
	}
}
