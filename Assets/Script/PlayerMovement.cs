using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D myBody;

 	public Animator myAnim;

    [Header("Movement")]
    public float maxSpeed;
    public float jumpHeight = 5;
    public float FallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    public bool isTouchingGround;


	[SerializeField] ParticleSystem dustEffect;
	ParticleSystem.EmissionModule dustEmission;
	[SerializeField] ParticleSystem impactEffect;
	private bool wasOnGround;


	private bool canDash = true;
	private bool isDashing;
	private float dashingPower = 24f;
	private float dashingTime = 0.2f;
	private float dashingCooldown = 1f;
	
	bool facingRight;	

   



    [Header("Changing World")]
    Renderer rend;
    private bool isInAnotherWorld;
    [SerializeField] private GameObject world;
    private Transform[] worldThings;
    
    private float timeChange = 6f;
    private float timeTemp;
    
    [SerializeField] private Slider timeChanger;
    
    public bool isInLevel3 = false;
    
    private GameObject[] pushObjects;




    void Start()
    {
        
    	myBody = GetComponent<Rigidbody2D> ();
     	myAnim = GetComponent<Animator> ();
        facingRight = true;
        
        rend = this.gameObject.GetComponent<Renderer>();
        
        worldThings = world.transform.GetComponentsInChildren<Transform>();
        isInAnotherWorld = false;
        
        pushObjects = GameObject.FindGameObjectsWithTag("PushObject");
        OnOffRigid();

	    dustEmission = dustEffect.emission;
	    
    }




    private void Update()
    {
        float move = Input.GetAxis("Horizontal");

    	// show dust
    // 	{
    	if (move != 0 && isTouchingGround)
    	{
    		dustEmission.rateOverTime = 20f;
    	}
    	else 
    	{
    		dustEmission.rateOverTime = 0f;
    	}
    
    
    	// show impact effect
    	if (!wasOnGround && isTouchingGround)
    	{
    		impactEffect.gameObject.SetActive(true);
    		impactEffect.Stop();
    		impactEffect.transform.position = dustEffect.transform.position;
    		impactEffect.Play();
    	}	
        // }


        /* Move, Jump, Dash */
        // {
        wasOnGround = isTouchingGround;        


        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        
     	myAnim.SetFloat("speed", Mathf.Abs(move));
	    myAnim.SetFloat("isJumping", Mathf.Abs(myBody.velocity.y));

        if (isDashing)
        {
            return;
        }
    
    	myBody.velocity= new Vector2 (move*maxSpeed, myBody.velocity.y);
    	if ((move>0 && !facingRight) || (move < 0 && facingRight))
    	{
    		flip();
    	}


        
        
     	if (Input.GetKeyDown(KeyCode.Space) && isTouchingGround)
     	{
     		myBody.velocity = Vector2.up * jumpHeight;
     	}


        if (Input.GetKeyDown(KeyCode.E) && canDash)
        {
		        myAnim.SetBool("isDash", true);
            	StartCoroutine(Dash());
        }
     	


        if (myBody.velocity.y < 0)
            myBody.velocity += Vector2.up * Physics2D.gravity.y * (FallMultiplier - 1) * Time.deltaTime;
        else if (myBody.velocity.y > 0 && !Input.GetButton("Jump"))
            myBody.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            
        // }    



        /* Change World */
        // {
        if (Input.GetKeyDown(KeyCode.R) && isInAnotherWorld == false && this.GetComponent<PlayerMana>().curr_mana == 100 && isInLevel3 == false)
        {
            this.GetComponent<PlayerMana>().curr_mana -= 100;
            StartCoroutine(CoolDownChange());
            timeTemp = timeChange;
    	}
    	
    	if (timeTemp >= 0)
    	{
        	timeTemp -= Time.deltaTime;
        	timeChanger.value = 1.0f * timeTemp / timeChange;
    	}
        // }
    	
	    /* end of Update function */
    }



	void flip() 
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}



	private IEnumerator Dash()
    	{
        	canDash = false;
        	isDashing = true;
        	float originalGravity = myBody.gravityScale;
        	myBody.gravityScale = 0f;
        	myBody.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        	yield return new WaitForSeconds(dashingTime);
        	myBody.gravityScale = originalGravity;
        	isDashing = false;
        	yield return new WaitForSeconds(dashingCooldown);
        	canDash = true;
    	}




	public void endDash()
	{
		myAnim.SetBool("isDash", false);
	}



    public void AppearObjectInAnotherWorld()
    {
        OnOffRigid();
        
        if (isInAnotherWorld == false)
        {
            foreach (Transform other in worldThings)
            {
                other.gameObject.SetActive(false);
            }
        }
        else
        {
            foreach (Transform other in worldThings)
            {
                other.gameObject.SetActive(true);
            }
        }
    }




    public void InvertColor()
    {
        if (isInAnotherWorld == true)
        {
            this.gameObject.GetComponent<SpriteRenderer>().material.color = new Color(1.0000f, 1.0000f, 1.0000f, 0.4705882f);
        }
        
        else 
        {
            this.gameObject.GetComponent<SpriteRenderer>().material.color = new Color(1.0000f, 1.0000f, 1.0000f, 1.0000f);;
        }
    }

	
	
    public IEnumerator CoolDownChange()
    {
        isInAnotherWorld = !isInAnotherWorld;
        AppearObjectInAnotherWorld();
        InvertColor();
        
        yield return new WaitForSeconds(timeChange);
        
        timeTemp = 0f;
        
        isInAnotherWorld = !isInAnotherWorld;
        AppearObjectInAnotherWorld();
        InvertColor();
	}
	
	
	
    public IEnumerator FlashCoolDownChange()
    {
        /* Make Flash*/
        isInAnotherWorld = !isInAnotherWorld;
        InvertColor();
        yield return new WaitForSeconds(0.1f);
        isInAnotherWorld = !isInAnotherWorld;
        InvertColor();
        yield return new WaitForSeconds(0.1f);
        
        isInAnotherWorld = !isInAnotherWorld;
        InvertColor();
        yield return new WaitForSeconds(0.1f);
        isInAnotherWorld = !isInAnotherWorld;
        InvertColor();
        yield return new WaitForSeconds(0.1f);
        
        isInAnotherWorld = !isInAnotherWorld;
        InvertColor();
        yield return new WaitForSeconds(0.1f);
        isInAnotherWorld = !isInAnotherWorld;
        InvertColor();
        yield return new WaitForSeconds(0.1f);
        
        /* main */
        isInAnotherWorld = !isInAnotherWorld;
        AppearObjectInAnotherWorld();
        InvertColor();
        
        yield return new WaitForSeconds(timeChange);
        
        timeTemp = 0f;
        
        isInAnotherWorld = !isInAnotherWorld;
        AppearObjectInAnotherWorld();
        InvertColor();
	}
	
	
	void OnOffRigid()
	{
	    foreach (GameObject obj in pushObjects)
	    {
		if (obj.GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Dynamic)
	        	obj.GetComponent<Rigidbody2D>().bodyType  = RigidbodyType2D.Static;
		else 
			obj.GetComponent<Rigidbody2D>().bodyType  = RigidbodyType2D.Dynamic;
	    }
	}
	
	
}

