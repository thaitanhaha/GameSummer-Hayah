using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
	private int appliedTimes = 0;
	private int appliedTimes1 = 0;


    void Start()
    {
        this.m_MaxHealth = 100;
        this.m_Health = 100;
    }


    void Update()
    {
        slider.value = 1f*m_Health/m_MaxHealth;
    }


	public void BeingPoison()
	{
		StartCoroutine(PoisonDamage(5));
	}




    IEnumerator PoisonDamage(int damage)
    {
        while(appliedTimes < 5)
        {
            TakeDame(damage);
            yield return new WaitForSeconds(0.5f);
            appliedTimes++;
        }
        appliedTimes = 0;
    }




	public void TakingHP()
	{
		if (this.m_Health < 100)
			StartCoroutine(HP_ing(10));
	}




	IEnumerator HP_ing(int hp)
    {
        while(appliedTimes1 < 5)
        {
            TakeHP(hp);
            yield return new WaitForSeconds(0.5f);
            appliedTimes1++;
        }
        appliedTimes1 = 0;
    }



	/*void OnTriggerEnter2D(Collider2D enemy_atk)
	{
		if (enemy_atk.gameObject.CompareTag("EnemyAttack"))
		{
			Debug.Log(2);
			TakeDame(10);
		}
	}*/


	void OnCollisionEnter2D(Collision2D enemy_atk)
	{
		if (enemy_atk.gameObject.CompareTag("EnemyAttack"))
		{
			TakeDame(10);
		}
	}

}
