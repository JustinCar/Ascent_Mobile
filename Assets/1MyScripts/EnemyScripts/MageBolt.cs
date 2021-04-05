using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageBolt : MonoBehaviour
{
    public int damageLowerBound;
    public int damageUpperBound;
    public GameObject explosionPrefab;
    public Rigidbody2D thisRigidbody;
	public float speed;
	public int direction;
    public float initialCooldown;
    PlayerAudioManager audioManager;
	LevelManager levelManager;

    // Use this for initialization
    void Start()
    {
        thisRigidbody = GetComponent<Rigidbody2D>();
        audioManager = GameObject.Find("Player").GetComponent<PlayerAudioManager>();
        levelManager = GameObject.Find("Manager").GetComponent<LevelManager>();
        damageLowerBound =  (int)(damageLowerBound * (levelManager.floorNumber));
        damageUpperBound =  (int)(damageUpperBound * (levelManager.floorNumber));
    }

	void Update () 
	{	
        initialCooldown -= Time.deltaTime;

        if (initialCooldown <= 0)
        {
            thisRigidbody.velocity = new Vector2 (speed * direction, thisRigidbody.velocity.y);
        }
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        audioManager.voidHitAudio();

        if (collision.gameObject.tag == "Player") 
        {
            collision.gameObject.GetComponent<PlayerHealth>().takeDamage(Random.Range(damageLowerBound, damageUpperBound));
        }

        GameObject explosionInstance = Instantiate(explosionPrefab, new Vector3(collision.contacts[0].point.x, collision.contacts[0].point.y, 0),  explosionPrefab.transform.rotation);

        if (direction == 1)
        {
            Vector3 scale = explosionInstance.transform.localScale;
            scale.x *= -1;
            explosionInstance.transform.localScale = scale;  
        }
        
        Destroy(gameObject);
    }
}
