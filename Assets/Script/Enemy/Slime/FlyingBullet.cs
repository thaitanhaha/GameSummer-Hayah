using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBullet : MonoBehaviour
{
    private float speed = 3;

    Transform player;
    Vector3 targetDir;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	/*targetDir = player.position - transform.position;
        float zAngle = Vector3.Angle(targetDir, new Vector3(1,0,0));
	float yAngle = Vector3.Angle(targetDir, new Vector3(0,1,0));
	if (yAngle >= 90) zAngle = -zAngle;

	transform.Rotate(0, 0, zAngle, Space.World);*/

    }


    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
    
        
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
    
}
