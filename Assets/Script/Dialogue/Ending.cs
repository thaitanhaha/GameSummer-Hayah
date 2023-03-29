using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
	[SerializeField] private Text dialogueText;
	[SerializeField] private string[] Sens;

	void Start()
	{
		StartCoroutine(TypeSentence(Sens));
	}

	IEnumerator TypeSentence (string[] sentence)
	{
		foreach(string s in sentence)
		{
			dialogueText.text = "";
			foreach (char letter in s.ToCharArray())
			{
				dialogueText.text += letter;
				yield return new WaitForSeconds(0.1f);
			}
			yield return new WaitForSeconds(2f);
		}

		yield return new WaitForSeconds(3f);
		PlayerPrefs.DeleteAll();
		SceneManager.LoadScene(0);
	}
}
