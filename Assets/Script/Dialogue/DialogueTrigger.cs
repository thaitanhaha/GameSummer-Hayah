using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	[SerializeField] private Dialogue dialogue;

	[SerializeField] private DialogueManager dlg_mng;


	void Start()
	{
		this.gameObject.SetActive(PlayerPrefs.GetInt(gameObject.name + "dlg") == 0);
	}


	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			PlayerPrefs.SetInt(gameObject.name + "dlg", 1);

			dlg_mng.StartDialogue(dialogue);

			GameObject[] enemies = GameObject.FindGameObjectsWithTag("EnemyAttack");
			foreach (GameObject e in enemies)
			{
				e.gameObject.SetActive(false);
			}

			Time.timeScale = 0f;
		}
	}


}