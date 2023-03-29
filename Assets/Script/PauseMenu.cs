using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	private int numHP;
	
	[SerializeField] private GameObject player;

	[SerializeField] GameObject panel;
	[SerializeField] GameObject tabPanel;
	
	[Header("Item")]
	[SerializeField] Button hppotion_button;
	[SerializeField] Button mir_button;
	[SerializeField] Text hptext;
	[SerializeField] Image hppotion, mir;
    [SerializeField] Sprite question, hppotion_sprite, mir_sprite;
    [SerializeField] Transform mir_real;
    
    private GameObject[] enemies;
	
	bool isPaused;
	bool isTab;


	void Start()
	{
		isPaused = false;
		isTab = false;
		numHP = 3;
	}

    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("EnemyAttack");
        
        if (PlayerPrefs.GetInt("HPPotion") == 1)
        {
            hppotion.sprite = hppotion_sprite;
            hptext.text = "x " + numHP.ToString();
            hppotion_button.interactable = true;
        }
        else 
        {
            hppotion_button.interactable = false;
            hppotion.sprite = question;
            hptext.text = "";
        }
        
        if (PlayerPrefs.GetInt("Mirror") == 1)
        {
            mir.sprite = mir_sprite;
            mir_button.interactable = true;
        }
        else 
        {
            mir.sprite = question;
            mir_button.interactable = false;
        }
        
            
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (!isPaused)
			{
				PauseGame();
				
			}
			else
			{
				Resume();
			}
		}


		if (Input.GetKeyDown(KeyCode.Tab))
		{
			if (!isTab)
			{
				TabGame();
				
			}
			else
			{
				TabResume();
			}			

		}
	}

	public void ToMainMenu()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene(1);
	}

	public void Resume()
	{
		ContinueAll();
	    panel.SetActive(false);
	    isPaused = false;
		if (!isTab && !isPaused) 
		    Time.timeScale = 1f;
	}

	public void PauseGame()
	{
		StopAll();
        panel.SetActive(true);
		Time.timeScale = 0f;
		isPaused = true;
	}

	public void Restart()
	{
		Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(scene.name);
		Time.timeScale = 1f;
	}



    public void TabGame()
    {
        StopAll();
        tabPanel.SetActive(true);
		Time.timeScale = 0f;
		isTab = true;
    }
    
    
    public void TabResume()
    {
	ContinueAll();
        tabPanel.SetActive(false);
        isTab = false;
		if (!isTab && !isPaused) 
		    Time.timeScale = 1f;
    }
    
    
    
    public void UsePotion()
    {
        if (numHP > 0)
        {
            numHP--;
            player.GetComponent<PlayerHealth>().TakingHP();
        }
        else
        {
                	
        }
        Resume();
        TabResume();

    }
    
    public void UseMirror()
    {
        Resume();
        TabResume();
        
        Vector2 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mir_real.position = targetPos;
        
    }



	void StopAll()
	{
	   /* Player */
		player.gameObject.GetComponent<PlayerMovement>().enabled = false;
		player.gameObject.GetComponent<PlayerHealth>().enabled = false;
		player.gameObject.GetComponent<PlayerMana>().enabled = false;
		
	    /* Enemy */
	    foreach (GameObject e in enemies)
	    {
	        e.gameObject.SetActive(false);
	    }
	}

	void ContinueAll()
	{
	    /* Player */
		player.gameObject.GetComponent<PlayerMovement>().enabled = true;
		player.gameObject.GetComponent<PlayerHealth>().enabled = true;
		player.gameObject.GetComponent<PlayerMana>().enabled = true;
		
	    /* Enemy */
	    foreach (GameObject e in enemies)
	    {
	        e.gameObject.SetActive(true);
	    }
	}
}



