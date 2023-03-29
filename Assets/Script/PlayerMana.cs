using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class PlayerMana : MonoBehaviour
{
	[SerializeField] AudioSource fireSound;
	[SerializeField] AudioSource manaSound;
	[SerializeField] AudioSource pushSound;




	[SerializeField] Slider manaSlider;
	private float max_mana = 100f;
	public float curr_mana = 0f;

    [SerializeField] private CinemachineVirtualCamera virCam;

    [Header("Attack")]
    
	[SerializeField] ParticleSystem attack_push;
	[SerializeField] ParticleSystem attack_mana;
	
	[SerializeField] private GameObject Attack_Fire;
    
    
    private Vector3 mousePos;
    [SerializeField] private GameObject Attack_Prepare;
    [SerializeField] private Transform Attack_Prepare_Tip;
    private bool canShoot = true;
    private float timer;
    private Transform player1;
    private float timeBetweenShoot = 1f;
    private Camera mainCam;


	public bool checkMana = true;

    PlayerMovement player;
    
    
    
    void Start()
    {
        player1 = this.transform;
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        player = this.GetComponent<PlayerMovement>();
        attack_mana.Stop();
        Attack_Fire.SetActive(false);
    }


	void Update()
	{
	    
	    checkMana = player.isTouchingGround;
	    
		manaSlider.value = 1f * curr_mana/max_mana;
		
		/* Increase Mana*/
		if (Input.GetKey(KeyCode.K) && checkMana)
		{
			manaSound.Play();		
	
		    attack_mana.Play();
		    this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			player.myAnim.SetFloat("speed", 0);
			player.enabled = false;
			IncreaseMana();
		}
		else
		{
			manaSound.Stop();			

		    player.enabled = true;
		    attack_mana.Stop();
		}


        /* Push */
		if (Input.GetKeyDown(KeyCode.T))
		{
			if (curr_mana == 100f)
			{
		pushSound.Play();

                DestroyEnemyInRange();
                attack_push.Play();
                curr_mana = 0f;
                
                StopAllCoroutines();
                StartCoroutine(ShakeCamera(2f, 2f));
			}
		}



        /* Fireeeeeee */
        if (Input.GetKey(KeyCode.F) && curr_mana >= 1)
        {
            Attack_Fire.SetActive(true);
	    curr_mana -= 1;
	    fireSound.Play();
        }
		else
		{
            Attack_Fire.SetActive(false);
		fireSound.Stop();
		}



        /*shooting*/
        if (!canShoot)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenShoot)
            {
                canShoot = true;
                timer = 0f;
            }
        }
        
        
        if (Input.GetMouseButtonDown(0) && canShoot && curr_mana >= 10)
        {
            canShoot = false;
            curr_mana -= 10;
            mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 rotation = mousePos - transform.position;
            float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            Instantiate(Attack_Prepare, Attack_Prepare_Tip.position, Quaternion.Euler(0, 0, rotZ - 90), player1);
            // shootingSound.Play();
        }


	}

	void IncreaseMana()
	{
		curr_mana = curr_mana + 2f;
		curr_mana = Mathf.Min(curr_mana, 100f);
	}
	
	
	
	
    void DestroyEnemyInRange()
    {
		Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(5,5), 0);
		foreach (Collider2D col in colliders)
	    {
			if(col.gameObject.CompareTag("Enemy") || col.gameObject.CompareTag("EnemyAttack"))
			{
			    Destroy(col.gameObject);
			}
		}
    }
    
    
    public IEnumerator ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cine = virCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cine.m_AmplitudeGain = intensity;
        
        yield return new WaitForSeconds(0.5f);
        
        cine.m_AmplitudeGain = 0f;
    }

    
    
}
