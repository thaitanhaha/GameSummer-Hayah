using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	// public Text nameText;

	public Text DialogueName;
	private Queue <string> nameText;

	public Text dialogueText;

	public Animator animator;

	private Queue<string> sentences;

	
	void Start () {
		sentences = new Queue<string>();
		nameText = new Queue<string>();
	}

	public void StartDialogue (Dialogue dialogue)
	{
		animator.SetBool("IsOpen", true);

		// nameText.text = dialogue.name;
		
		nameText.Clear();
		sentences.Clear();


		foreach (string name in dialogue.name)
		{
			nameText.Enqueue(name);
		}

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence ()
	{
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		string name = nameText.Dequeue();
		DialogueName.text = name;

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence (string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

	void EndDialogue()
	{
		animator.SetBool("IsOpen", false);

		Time.timeScale = 1f;
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		player.gameObject.GetComponent<PlayerMovement>().enabled = true;
		player.gameObject.GetComponent<PlayerHealth>().enabled = true;
		player.gameObject.GetComponent<PlayerMana>().enabled = true;


		FindObjectOfType<PlayerHealth>().GetComponent<PlayerMovement>().enabled = true;

			GameObject[] enemies = FindInActiveObjectsByTag("EnemyAttack");
			foreach (GameObject e in enemies)
			{
				e.gameObject.SetActive(true);
			}


		Destroy(transform.parent.parent.gameObject);
	}




GameObject[] FindInActiveObjectsByTag(string tag)
{
    	List<GameObject> validTransforms = new List<GameObject>();
    	Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
    	for (int i = 0; i < objs.Length; i++)
    	{
        	if (objs[i].hideFlags == HideFlags.None)
        	{
            		if (objs[i].gameObject.CompareTag(tag))
            		{
                		validTransforms.Add(objs[i].gameObject);
            		}
        	}
    	}
    	return validTransforms.ToArray();
}

}