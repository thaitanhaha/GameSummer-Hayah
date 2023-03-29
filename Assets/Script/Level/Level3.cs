using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3 : MonoBehaviour
{
    private PlayerMovement player;
    
    private float timer = 0f;
    
    
    void Start()
    {
	player = FindObjectOfType<PlayerMovement>().GetComponent<PlayerMovement>();
        player.isInLevel3 = true;
    }
    
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 10f)
        {
            StartCoroutine(RandomChange());
            timer = 0f;
        }
    }
    
    
    IEnumerator RandomChange()
    {
        player.StartCoroutine(player.FlashCoolDownChange());
        yield return null;
    }
}
