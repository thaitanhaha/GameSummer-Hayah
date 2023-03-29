using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeOrange : EnemyHealth
{
	[SerializeField] float speed;
	[SerializeField] float range;
	Transform player;

	Rigidbody2D rb;


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
		FollowPlayer();
	}
	else
	{
		StopFollowing();
	}


    }



	private void FollowPlayer()
	{
		if (transform.position.x < player.position.x)
		{
			rb.velocity = new Vector2(speed, 0);
			transform.eulerAngles = new Vector3(0, 0, 0);
			slider.transform.eulerAngles = new Vector3(0, 0, 0);
		}
		else 
		{
			rb.velocity = new Vector2(-speed, 0);
			transform.eulerAngles = new Vector3(0, -180, 0);
			slider.transform.eulerAngles = new Vector3(0, 0, 0);
		}
	}

	private void StopFollowing()
	{
		rb.velocity = new Vector2(0,0);
	}
}
