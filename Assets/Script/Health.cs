using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] public float m_MaxHealth;
    public float m_Health;

	public Slider slider;
	public ParticleSystem beingHurt;



    public void UpdateHealth()
    {
        slider.value = 1f*m_Health/m_MaxHealth;
    }


    public void TakeDame(float dame)
    {
        m_Health -= dame;
        m_Health = Mathf.Max(0, m_Health);

        beingHurt.Play();

        if (m_Health <= 0)
        {
            Die();
        }
    }



    /* HP */

    public void TakeHP(float hp)
    {
        m_Health += hp;
        m_Health = Mathf.Min(m_Health, m_MaxHealth);
    }


    public void Die()
    {
	if (this.gameObject.CompareTag("Player"))
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Destroy(gameObject);
    }




    /* Damage */

	




}
