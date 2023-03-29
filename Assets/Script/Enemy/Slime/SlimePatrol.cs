using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePatrol : EnemyHealth
{
	[SerializeField] private float speed;
	private float distance = 0.5f;

	private bool movingRight = true;
	
	[SerializeField] private Transform groundDetection;


	void Start()
	{
		this.m_MaxHealth = 100;
		this.m_Health = 100;
	}

	void Update()
	{
		UpdateHealth();

		transform.Translate(Vector2.right * speed * Time.deltaTime);

		RaycastHit2D ground = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
		if (!ground.collider)
		{
			if (movingRight)
			{
				transform.eulerAngles = new Vector3(0, -180, 0);
				slider.transform.eulerAngles = new Vector3(0, 0, 0);
				movingRight = false;
			}
			else
			{
				transform.eulerAngles = new Vector3(0, 0, 0);
				slider.transform.eulerAngles = new Vector3(0,0,0);
				movingRight = true;
			}
		}

	}

}


