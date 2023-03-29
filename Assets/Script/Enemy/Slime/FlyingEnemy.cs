using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : EnemyHealth
{
    private float speed = 10;

    private float timeFlip = 3f;
    private float timeIdle = 0.1f;
    private int tempIdle = 1;

    Transform player;
    
    
    

    void Start()
    {
        this.m_MaxHealth = 100;
        this.m_Health = 100;
        
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }


    void Update()
    {
        UpdateHealth();
        
        timeFlip -= Time.deltaTime;
        if (timeFlip <= 0)
        {
            flip();
            timeFlip = 3f;
        }
        
        
        timeIdle -= Time.deltaTime;
        this.transform.position += new Vector3(0, 0.05f, 0) * tempIdle;
        if (timeIdle <= 0f)
        {
            timeIdle = 0.1f;
            tempIdle = -tempIdle;
        }
        

       
    	
        float dist = Vector2.Distance(transform.position, player.position);
        if (dist < 10f)
        {
            LookAtPlayer();
            EnemyDash();
        }
    }
    
    
	private void EnemyDash()
    {
        // 	float originalGravity = myBody.gravityScale;
        // 	myBody.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        // 	yield return new WaitForSeconds(dashingTime);
        // 	myBody.gravityScale = originalGravity;
        
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
    
    
	private void LookAtPlayer()
	{
	    if (transform.position.x < player.position.x)
		{
			transform.localScale = new Vector2(-0.5f,0.5f);
		}
		else 
		{
			transform.localScale = new Vector2(0.5f,0.5f);
		}
	}
    
    
    
    
	void flip() 
	{
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
    
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
    
}
