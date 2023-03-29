using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    
    void OnTriggerEnter2D(Collider2D my_atk)
	{
        if (my_atk.gameObject.CompareTag("MyAttack"))
        {
            TakeDame(1);
        }
    }

    void OnTriggerStay2D(Collider2D my_atk)
	{
		if (my_atk.gameObject.CompareTag("MyAttack"))
		{
			TakeDame(1f);
		}
	}


    void OnCollisionEnter2D(Collision2D my_atk)
	{
		if (my_atk.gameObject.CompareTag("MyAttack"))
		{
			TakeDame(1);
		}
	}
}
