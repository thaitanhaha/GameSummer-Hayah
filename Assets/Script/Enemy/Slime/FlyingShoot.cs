using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingShoot : EnemyHealth
{
    [SerializeField] float speed;
	[SerializeField] float range;
	Transform player;
	private float timeShoot = 0.5f;
	[SerializeField] Transform tip;

	Rigidbody2D rb;

	[SerializeField] private GameObject[] way;
	[SerializeField] private GameObject bullet;

	int current = 0;



    void Start()
    {
        this.m_MaxHealth = 100;
    	this.m_Health = 100;
    	rb = GetComponent<Rigidbody2D>();
    	player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }


    void Update()
    {
	UpdateHealth();

    	float dist = Vector2.Distance(transform.position, player.position);
    	if (dist < range)
    	{
    	    LookAtPlayer();
            timeShoot -= Time.deltaTime;
            if (timeShoot <= 0)
            {
                ShootPlayer();
                timeShoot = 0.5f;
            }
    	}
    	else
    	{
    		MovingAround();
    	}


    }






	private void ShootPlayer()
	{
// 		if (timeShoot == 0)
		    Instantiate(bullet, tip.position, Quaternion.identity);
	}
	
	
	private void LookAtPlayer()
	{
	    if (transform.position.x < player.position.x)
		{
			transform.localScale = new Vector2(-1,1);
		}
		else 
		{
			transform.localScale = new Vector2(1,1);
		}
	}
	

	private void MovingAround()
	{
		if (Vector2.Distance(transform.position, way[current].transform.position) < 0.1f)
		{
			current++;
			if (current == 2) current = 0;
			transform.localScale *= new Vector2(-1,1);
		}

		//rb.velocity = (transform.position - way[current].transform.position) * Time.deltaTime * speed;
		transform.position = Vector2.MoveTowards(transform.position, way[current].transform.position, Time.deltaTime * speed);
	}


}
